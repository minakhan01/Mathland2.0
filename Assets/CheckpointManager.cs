using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : Singleton<CheckpointManager> {

    private List<Checkpoint> checkpoints = new List<Checkpoint>();
    private int currentCheckpoint = 0; 

    void initCheckpoints()
    {
        Debug.Log("Initializing " + checkpoints.Count + " checkpoints"); 

        for (int i = 0; i < checkpoints.Count; i++)
        {
            checkpoints[i].id = i;
        }
    }

	// Use this for initialization
	void Start () {
        Debug.Log("Checkpoint manager init");
        initCheckpoints(); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Called when a checkpoint is triggered with its corresponding id
    // Returns true if checkpoitn successfully hit
    public bool checkpointTriggered(int id, Collider other)
    {
        if (other.gameObject.name != "Ball" ||
            BallStateManager.Instance.currentBallState != BallStateManager.BallState.Launched ||
            id != currentCheckpoint) return false; 
        else
        {
            currentCheckpoint++;
            if (currentCheckpoint == checkpoints.Count) checkpointsCompleted();
            return true;
        }
    }

    // Called when all of the checkpoints were successfully completed
    private void checkpointsCompleted()
    {
        Debug.Log("You win!");
        LevelLoader.Instance.NextLevel(); 
    }

    public void newCheckpointAdded()
    {
        initCheckpoints(); 
    }

    public void addCheckpoint(Checkpoint checkpoint)
    {

        checkpoints.Add(checkpoint);
        initCheckpoints(); 
    }

	public void resetCheckpoints() {
		if (checkpoints != null) { 
			foreach (Checkpoint checkpoint in checkpoints) {
                Debug.Log("Destroying Checkpoint"); 
				Destroy (checkpoint.gameObject); //might not actually remove the gameobject until next call to update
			}
		}
        checkpoints.Clear();

        currentCheckpoint = 0; 

        Debug.Log("Checkpoints reset: # checkpoints: " + checkpoints.Count); 
	}
}
