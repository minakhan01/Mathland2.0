using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeMe : MonoBehaviour {

    public bool isResizing = false;
    float MinScale = 0.5f;
    float MaxScale = 2f;
    float ResizeSpeedFactor = 1.5f;
    float ResizeScaleFactor = 1.5f;
    Vector3 lastScale;

    // Use this for initialization
    void Start () {
		
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void resizingStarted() {
        lastScale = gameObject.transform.localScale;
    }

    // Called by GestureManager
	public void resize(Vector3 normalizedOffset) {
//        Debug.Log("Resize called");
//        float resizeX, resizeY, resizeZ;
//
//        resizeX = resizeY = resizeZ = normalizedOffset.z * ResizeScaleFactor;
//
//        resizeX = Mathf.Clamp(lastScale.x + resizeX, MinScale, MaxScale);
//        resizeY = Mathf.Clamp(lastScale.y + resizeY, MinScale, MaxScale);
//        resizeZ = Mathf.Clamp(lastScale.z + resizeZ, MinScale, MaxScale);
//
//		Vector3 newLocalScale = Vector3.Lerp(transform.localScale,
//			new Vector3(resizeX, resizeY, resizeZ),
//			ResizeSpeedFactor);

		// LegoControllerManager.Instance.updateSize (normalizedOffset.z); //Disabled for now
    }

}
