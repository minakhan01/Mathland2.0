using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class BallStateManager : Singleton<BallStateManager> {

	public BallState currentBallState = BallState.Released;
	public GameObject ball;
	Rigidbody rbi;
	public GameObject ballPositionText;
    public bool playingCatch; 
    private Vector3 ballPosition;

    private GameObject ballJointConnectedBody = null;
//    private Quaternion ballJointConnectedBodyRotation;
//    private Vector3 ballJointConnectedBodyPosition;


	public enum BallState
	{
		Released = 0,
		Grabbed,
		Launched
	}

    // Use this for initialization
    void Start () {
		rbi = ball.GetComponent<Rigidbody>();
        playingCatch = false; 
    }


    private void FixedUpdate()
    {
        if (currentBallState == BallState.Launched)
        {
            if (ball.GetComponent<VelocityReactor>() != null)
            {
                //rbi.AddForce(ball.GetComponent<VelocityReactor>().experiencedforce);
            }
        }
    }


//	/// <summary>
//	/// When the keyword recognizer hears a command this will be called.
//	/// In this case we only have one keyword, which will re-enable moving the
//	/// target.
//	/// </summary>
//	/// <param name="args">information to help route the voice command.</param>
//	private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
//	{
//		string stringDetected = args.text.ToLower ();
//		if (stringDetected.Equals(resetBallCommand.ToLower())) {
//            Reset();
//		}
//		else if (stringDetected.Equals(releaseBallCommand.ToLower()))  {
//			Release ();
//		}
//		else if (stringDetected.Equals(grabBallCommand.ToLower()))
//		{
//			Grab ();
//		}
//		else if (stringDetected.Equals(launchBallCommand.ToLower()))
//		{
//			Launch ();
//		}
//	}


	public void Release() {
		if (currentBallState != BallState.Released) {
			Debug.Log("ball state changed from "+currentBallState+" to released");
			currentBallState = BallState.Released;
		}
	}

	public void Grab() {
		if (currentBallState != BallState.Grabbed) {
			Debug.Log("ball state changed from "+currentBallState+" to grabbed");
			currentBallState = BallState.Grabbed;
		}
	}

    public void PlayCatch()
    {
        playingCatch = true;
        Launch();
        playingCatch = false; 
    }

	public void Launch() {
        //playingCatch = true;  // TODO Remove this when not playing catch!
		if (currentBallState != BallState.Launched) {
			Debug.Log("ball state changed from "+currentBallState+" to launched");
			currentBallState = BallState.Launched;
//            GameToolManager.Instance.freezePrefabs(false); 
			LaunchBall ();
		}
	}

	private void GrabBall()
	{
		Debug.Log ("Grab ball");
        rbi.useGravity = false;
    }

	private void ReleaseBall()
	{
		Debug.Log ("Release ball");
        rbi.useGravity = false;
    }

	private void LaunchBall()
	{
		Debug.Log ("Launch ball");
        if (playingCatch)
        {
            rbi.isKinematic = false;
            rbi.velocity = new Vector3(1, 1, 1);
            return; 
        }

        ballPosition = ball.transform.position;
        rbi.isKinematic = false;
        rbi.velocity = ball.GetComponent<VelocityReactor>().updateInitVelocity;
        //rbi.AddForce(ball.GetComponent<VelocityReactor>().experiencedforce);
        //Debug.Log("Ball should have moved as vel is " + rbi.velocity + " and force is " + ball.GetComponent<VelocityReactor>().experiencedforce);
        //rbi.isKinematic = false;
        //rbi.useGravity = true;
        //rbi.velocity = ball.transform.forward * PhysicsManager.Instance.force;
        //Time.timeScale = 0.5f;
        //Time.fixedDeltaTime = 0.02F * Time.timeScale;
        if (ball.GetComponent<Joint>() != null)
        {
            // Ball is attached to something. Save properties of that object 
            Debug.Log("Ball had a joint when launched, saving connected body");
            saveConnectedBody();
        }
        else
        {
            Debug.Log("Setting ballJointConnectedBody to null"); 
            ballJointConnectedBody = null;
        }
    }

	public void Reset() {
        
//		rbi.useGravity = false;
		Debug.Log("ball state changed from "+currentBallState+" to reset");
        GameObject.Find("TrajectoryPredictor").GetComponent<TrajectoryPredictorScript>().resetBall();
        ball.GetComponent<VelocityReactor>().velocities = new List<GameObject>();
        ball.GetComponent<VelocityReactor>().forces = new List<GameObject>();
        rbi.isKinematic = true;
        
        if (ballJointConnectedBody)
        {
            // Remove the joint before changing ball position
            Destroy(ball.GetComponent<Joint>());
            Debug.Log("Temporarily removed joint from ball");
            ball.transform.position = ballPosition;
            restoreConnectedBody(); 

        } else
        {
            // If the ball was not connected to anything, we can animate it back to its original position
            AnimateBallPosition(ballPosition);
        }

        
        ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        currentBallState = BallState.Released;
//        GameToolManager.Instance.freezePrefabs(true); //unfreeze
//        LevelLoader.Instance.resetLevel(); 

    }



    float animationCompletion = 1;
    float animationSpeed = .5f;
    Vector3 ballStartPosition;
    Vector3 ballEndPosition;

    private void saveConnectedBody()
    {
        ballJointConnectedBody = ball.GetComponent<Joint>().connectedBody.gameObject; //Should be the last bone of the rope
//        ballJointConnectedBodyRotation = ballJointConnectedBody.transform.parent.rotation;
//        ballJointConnectedBodyPosition = ballJointConnectedBody.transform.parent.position;
        Debug.Log("Saved connectedBody to: " + ballJointConnectedBody.name); 
    }

    private void restoreConnectedBody()
    {
        GameObject oldRope = ballJointConnectedBody.transform.parent.gameObject;
        string oldRopeName = oldRope.name; 
        Destroy(oldRope);

        // Create new rope
//        GameObject newRope = Instantiate(GameToolManager.Instance.GameToolPrefabsDictionary["Rope"].GetComponent<MenuItemRepresentation>().Itemtobeinstantiated, ballJointConnectedBodyPosition, ballJointConnectedBodyRotation);
//        GameToolManager.Instance.setupGameTool(newRope);
//        newRope.name = oldRopeName;
//        GameToolManager.Instance.InstantiatedGameToolPrefabs[oldRopeName] = newRope; 
//
//        ball.GetComponent<GameTool>().addJointTo(newRope.transform.GetChild(newRope.transform.childCount-1).gameObject); // Re-add joint to last bone in newRope

        Debug.Log("Reattached joint to ball");
    }


    public void AnimateBallPosition(Vector3 destination)
    {
        ballStartPosition = ball.transform.position;
        ballEndPosition = destination;

        animationCompletion = 0; // This starts the animation in Update()
    }


    void Update()
    {

        if (animationCompletion < 1)
        {
            animationCompletion += Time.deltaTime * animationSpeed;
            ball.transform.position = Vector3.Lerp(ballStartPosition, ballEndPosition, animationCompletion); 
        }
        
    }

}
