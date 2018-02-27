using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePosition : MonoBehaviour {

    public GameObject imageTarget;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (imageTarget != null)
        {
            Vector3 v3 = new Vector3(imageTarget.transform.position.x, imageTarget.transform.position.y, imageTarget.transform.position.z);
            transform.position = v3;
            Vector3 eulerAngles = imageTarget.transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(-eulerAngles.x, eulerAngles.y, -eulerAngles.z);
        }
    }
}
