using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalRubyShared
{
    public class DemoScriptMultiDrag : MonoBehaviour
    {
        [Tooltip("Asteroid prefab")]
        public GameObject AsteroidPrefab;

        [Tooltip("Number of asteroids to spawn")]
        [Range(1, 50)]
        public int SpawnCount = 20;

        private class DragState
        {
            public GestureRecognizer Gesture;
            public GameObject GameObject;
            public Vector2 Offset;
        }

        // lookup existing dragging objects by game object or by gesture
        private readonly Dictionary<GameObject, DragState> draggingObjects = new Dictionary<GameObject, DragState>();
        private readonly Dictionary<GestureRecognizer, DragState> draggingObjectsGesture = new Dictionary<GestureRecognizer, DragState>();

        private bool CanDrag(GameObject obj)
        {
            return obj.name.StartsWith("Asteroid") && !draggingObjects.ContainsKey(obj);
        }

        private void Start()
        {
            // randomly position and scale the asteroids in the field of view
            Bounds bounds = new Bounds
            {
                min = Camera.main.ViewportToWorldPoint(Vector3.zero),
                max = Camera.main.ViewportToWorldPoint(Vector3.one)
            };
            for (int i = 0; i < SpawnCount; i++)
            {
                GameObject obj = GameObject.Instantiate(AsteroidPrefab);
                float scale = Random.Range(0.2f, 0.5f);
                obj.transform.localScale = new Vector3(scale, scale, 1.0f);
                obj.transform.position = new Vector3(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y), 0.0f);

                PanGestureRecognizer pan = new PanGestureRecognizer();
                pan.StateUpdated += Pan_StateUpdated;

                // allow the pan to start immediately with no minimum distance movement required
                pan.ThresholdUnits = 0.0f;

                // attach the pan gesture to the asteroid
                pan.PlatformSpecificView = obj;

                FingersScript.Instance.AddGesture(pan);
            }
        }

        private void Update()
        {
        }

        private void Pan_StateUpdated(GestureRecognizer gesture)
        {
            switch (gesture.State)
            {
                case GestureRecognizerState.Began:
                {
                    // check if we hit something, if so start dragging it otherwise reset the gesture
                    Vector2 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(gesture.FocusX, gesture.FocusY, 0.0f));
                    Collider2D[] hits = Physics2D.OverlapCircleAll(worldPos, 0.5f);
                    bool foundOne = false;
                    foreach (Collider2D hit in hits)
                    {
                        if (CanDrag(hit.gameObject))
                        {
                            // store the drag state along with an offset to reposition relative to the touch point and object center
                            DragState state = new DragState
                            {
                                Gesture = gesture,
                                GameObject = hit.gameObject,
                                Offset = (Vector2)hit.transform.position - worldPos
                            };
                            draggingObjects[hit.gameObject] = state;
                            draggingObjectsGesture[gesture] = state;
                            foundOne = true;
                            break;
                        }
                    }
                    if (!foundOne)
                    {
                        gesture.Reset();
                    }
                } break;

                case GestureRecognizerState.Executing:
                {
                    DragState state;
                    if (draggingObjectsGesture.TryGetValue(gesture, out state))
                    {
                        // position the object relative to the touch location and offset
                        Vector2 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(gesture.FocusX, gesture.FocusY, 0.0f));
                        state.GameObject.transform.position = (Vector3)(worldPos + state.Offset);
                    }
                } break;

                case GestureRecognizerState.Ended:
                {
                    // finish dragging, remove state
                    DragState state;
                    if (draggingObjectsGesture.TryGetValue(gesture, out state))
                    {
                        draggingObjectsGesture.Remove(gesture);
                        draggingObjects.Remove(state.GameObject);
                    }
                } break;
            }
        }
    }
}
