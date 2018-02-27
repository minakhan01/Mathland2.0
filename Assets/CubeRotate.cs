using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotate : MonoBehaviour, BTTransformReceiver {

	public GameObject cubeBTConnection;
    F8Sensor sensor;

    // Use this for initialization
    void Start()
    {
        Debug.Log("CubeRotate started");
		sensor = cubeBTConnection.GetComponent<BluetoothConnection> ().sensor;
    }

    // Update is called once per frame
    void Update()
    {
		if (sensor == null) {
			sensor = cubeBTConnection.GetComponent<BluetoothConnection> ().sensor;
        }
    }


	public void updateAcceleration(Vector3 acceleration) {
	}

	public void updateStretch(int stretch) {
        LegoControllerManager.Instance.updateStretch(stretch); 
	}

	public void updateRotation(Quaternion rotation) {
		LegoControllerManager.Instance.updateRotation(rotation);
	}
}
