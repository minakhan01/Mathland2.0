using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public int id;
    private CheckpointManager checkpointManager;
    private TextMesh textDisplay; 

	// Use this for initialization
	void Start () {
        Debug.Log("Checkpoint init");
		checkpointManager = CheckpointManager.Instance;
        textDisplay = GetComponentInChildren<TextMesh>();
        textDisplay.text = (id + 1).ToString(); 

        //TODO a check to set isBackwards to true of the text cameraFacing component in Spectator View if it happens consistently
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Checkpoint triggered");
        bool success = checkpointManager.checkpointTriggered(id, other);

        if (success) setColor(Color.green); 

    }

    private void setColor(Color color)
    {
        this.transform.GetChild(0).GetComponent<Renderer>().material.color = color;
    }
}
