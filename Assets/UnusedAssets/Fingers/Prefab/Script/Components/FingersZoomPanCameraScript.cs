using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalRubyShared
{
    [RequireComponent(typeof(Camera))]
    public class FingersZoomPanCameraScript : MonoBehaviour
    {
        [Tooltip("Require this area to be visible at all times")]
        public Collider VisibleArea;

        private ScaleGestureRecognizer scaleGesture;
        private PanGestureRecognizer panGesture;
        private TapGestureRecognizer tapGesture;
        private Vector3 cameraAnimationTargetPosition;

        private Camera _camera;
        private Rigidbody _rigidBody;

        private IEnumerator AnimationCoRoutine()
        {
            Vector3 start = _rigidBody.position;

            // animate over 1/2 second
            for (float accumTime = Time.deltaTime; accumTime <= 0.5f; accumTime += Time.deltaTime)
            {
                _rigidBody.position = Vector3.Lerp(start, cameraAnimationTargetPosition, accumTime / 0.5f);
                yield return null;
            }
        }

        private void Start()
        {
            _camera = GetComponent<Camera>();
            _rigidBody = GetComponent<Rigidbody>();
            if (_rigidBody == null)
            {
                _rigidBody = gameObject.AddComponent<Rigidbody>();
            }
            _rigidBody.isKinematic = true;
            if (GetComponent<UnityEngine.EventSystems.PhysicsRaycaster>() == null)
            {
                gameObject.AddComponent<UnityEngine.EventSystems.PhysicsRaycaster>();
            }
            if (GetComponent<UnityEngine.EventSystems.Physics2DRaycaster>() == null)
            {
                gameObject.AddComponent<UnityEngine.EventSystems.Physics2DRaycaster>();
            }

            scaleGesture = new ScaleGestureRecognizer
            {
                ZoomSpeed = 6.0f // for a touch screen you'd probably not do this, but if you are using ctrl + mouse wheel then this helps zoom faster
            };
            scaleGesture.StateUpdated += Gesture_Updated;
            FingersScript.Instance.AddGesture(scaleGesture);

            panGesture = new PanGestureRecognizer();
            panGesture.StateUpdated += PanGesture_Updated;
            FingersScript.Instance.AddGesture(panGesture);

            // the scale and pan can happen together
            scaleGesture.AllowSimultaneousExecution(panGesture);

            tapGesture = new TapGestureRecognizer();
            tapGesture.StateUpdated += TapGesture_Updated;
            FingersScript.Instance.AddGesture(tapGesture);
        }

        private void LateUpdate()
        {
            if (VisibleArea == null)
            {
                return;
            }

            Bounds b = VisibleArea.bounds;
            Vector3 world1 = _camera.ViewportToWorldPoint(Vector3.zero);
            Vector3 world2 = _camera.ViewportToWorldPoint(Vector3.one);
            Vector3 pos = _rigidBody.position;

            // move the camera so that the visible area is visible, if necessary

            // x axis
            if (world1.x > b.max.x)
            {
                pos.x -= (world1.x - b.max.x);
            }
            else if (world2.x < b.min.x)
            {
                pos.x += (b.min.x - world2.x);
            }

            // y axis
            if (world1.y > b.max.y)
            {
                pos.y -= (world1.y - b.max.y);
            }
            else if (world2.y < b.min.y)
            {
                pos.y += (b.min.y - world2.y);
            }

            _rigidBody.position = pos;
        }

        private void TapGesture_Updated(GestureRecognizer gesture)
        {
            if (tapGesture.State != GestureRecognizerState.Ended)
            {
                return;
            }

            Ray ray = _camera.ScreenPointToRay(new Vector3(tapGesture.FocusX, tapGesture.FocusY, 0.0f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // adjust camera x, y to look at the tapped / clicked sphere
                cameraAnimationTargetPosition = new Vector3(hit.transform.position.x, hit.transform.position.y, _camera.transform.position.z);
                StopAllCoroutines();
                StartCoroutine(AnimationCoRoutine());
            }
        }

        private void PanGesture_Updated(GestureRecognizer gesture)
        {
            if (panGesture.State == GestureRecognizerState.Executing)
            {
                StopAllCoroutines();

                // convert pan coordinates to world coordinates
                // get z position, orthographic this is 0, otherwise it's the z coordinate of all the spheres
                float z = (_camera.orthographic ? 0.0f : 10.0f);
                Vector3 pan = new Vector3(panGesture.DeltaX, panGesture.DeltaY, z);
                Vector3 zero = _camera.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, z));
                Vector3 panFromZero = _camera.ScreenToWorldPoint(pan);
                Vector3 panInWorldSpace = zero - panFromZero;
                _camera.transform.Translate(panInWorldSpace);
            }
            else if (panGesture.State == GestureRecognizerState.Ended)
            {
                // apply velocity to camera to give it a little extra smooth end to the pan
                _camera.GetComponent<Rigidbody>().velocity = new Vector3(panGesture.VelocityX * -0.002f, panGesture.VelocityY * -0.002f, 0.0f);
            }
        }

        private void Gesture_Updated(GestureRecognizer gesture)
        {
            if (scaleGesture.State != GestureRecognizerState.Executing || scaleGesture.ScaleMultiplier == 1.0f)
            {
                return;
            }

            // invert the scale so that smaller scales actually zoom out and larger scales zoom in
            float scale = 1.0f + (1.0f - scaleGesture.ScaleMultiplier);

            if (_camera.orthographic)
            {
                float newOrthographicSize = Mathf.Clamp(_camera.orthographicSize * scale, 1.0f, 100.0f);
                _camera.orthographicSize = newOrthographicSize;
            }
            else
            {
                // get camera look vector
                Vector3 forward = _camera.transform.forward;

                // set the target to the camera x,y and 0 z position
                Vector3 target = _rigidBody.position;
                target.z = 0.0f;

                // get distance between camera target and camera position
                float distance = Vector3.Distance(target, _rigidBody.position);

                // come up with a new distance based on the scale gesture
                float newDistance = Mathf.Clamp(distance * scale, 1.0f, 100.0f);

                // set the camera position at the new distance
                _rigidBody.position = target - (forward * newDistance);
            }
        }
    }
}
