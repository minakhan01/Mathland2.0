using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class StrobingHandler : Singleton<StrobingHandler> {

	public GameObject ball;
	private int updateCount = 0;
	public GameObject ballPrefab;
	//	public Color colorOfStrobe;

	private List<GameObject> strobes = new List<GameObject>();

	// arrows
	public GameObject realArrowVelocity;
	public GameObject realArrowForce;
	public GameObject arrowVelocity;
	public GameObject arrowForce;
	private Vector3 initialVelocityScale, initialForceScale;
	public float VelocityConst, ForceConst;

	public int countInterval = 6;

	// Use this for initialization
	void Start () {
		initialVelocityScale = arrowVelocity.transform.localScale;
		initialForceScale = arrowForce.transform.localScale;
	}



	// Update is called once per frame
	void Update () {
		if (GameStateManager.Instance.currentPhysicsPlayState == GameStateManager.GamePlayPhysicsState.ON) {
			updateCount++;
			if (updateCount % countInterval == 0)
			{
				//instantiate our strobe ball
				GameObject ballInstance = (GameObject) Instantiate(ballPrefab, ball.transform.position, ball.transform.rotation);
				MeshRenderer ballMesh = ballInstance.GetComponent<MeshRenderer>();
				Debug.Log ("Strobe after MeshRenderer");

				//instantiate arrows

				BallPhysicsManager.Instance.updateVelocityandForce();
				GameObject arrowVelocityInstance = (GameObject) Instantiate(arrowVelocity, realArrowVelocity.transform.position, realArrowVelocity.transform.rotation);
				GameObject arrowForceInstance = (GameObject) Instantiate(arrowForce, realArrowForce.transform.position, realArrowForce.transform.rotation);
				float ballVelocityMagnitude = 0;
				float ballForceMagnitude = 0;
				if (transform.root.gameObject.name == BallPhysicsManager.Instance.ball.name) {
					//resize strobe arrow
					ballVelocityMagnitude = BallPhysicsManager.Instance.ball.GetComponent<Rigidbody> ().velocity.magnitude;
					ballForceMagnitude = BallPhysicsManager.Instance.updatedForce.magnitude;
				} else if (transform.root.gameObject.name == BallPhysicsManager.Instance.ballTwo.name) {
					//resize strobe arrow
					ballVelocityMagnitude = BallPhysicsManager.Instance.ballTwo.GetComponent<Rigidbody> ().velocity.magnitude;
					ballForceMagnitude = BallPhysicsManager.Instance.updatedForceBallTwo.magnitude;
				}

				Debug.Log ("velocity of ball " + BallPhysicsManager.Instance.updatedVelocity);
				if (ballVelocityMagnitude == 0.0)
					arrowVelocityInstance.transform.localScale = new Vector3 (0f, 0f, 0f);
				else
					arrowVelocityInstance.transform.localScale = new Vector3 (initialVelocityScale.x, initialVelocityScale.y, initialVelocityScale.z*ballVelocityMagnitude*VelocityConst);
				if (GameStateManager.Instance.sceneHasRope) {
					arrowForceInstance.transform.localScale = new Vector3 (initialForceScale.x, initialForceScale.y, initialForceScale.z*0.5f);
				}
				else if (ballForceMagnitude == 0.0)
					arrowForceInstance.transform.localScale = new Vector3 (0f, 0f, 0f);
				else
					arrowForceInstance.transform.localScale = new Vector3 (initialForceScale.x, initialForceScale.y, initialForceScale.z*ballForceMagnitude*ForceConst);
				Debug.Log ("arrow velocity localscale " + arrowVelocityInstance .transform.localScale);
				Debug.Log ("arrow force localscale " + arrowForceInstance.transform.localScale);

				//get our ballInstance color and set it to the same color as our TrailRenderer
				Color color = ballMesh.material.color;
				color = ball.GetComponent<TrailRenderer>().startColor;
				Debug.Log ("Strobe after color");

				//make trail appropriately transparent, and measured via velocity
				float velocity = ball.GetComponent<Rigidbody>().velocity.magnitude;
				float transparency = velocity / 25 + 0.5f;
				if (transparency < 1)
				{
					color.a = transparency;
				}
				Debug.Log ("Strobe after velocity");
				ballMesh.material.color = color;

				Debug.Log ("Strobe after material");

				Debug.Log ("Strobe after rigidbody");
				//once we create 'BallInformation' property
				//				ballInstance.GetComponentInChildren<BallInformation>().setVelocity(velocity);


				strobes.Add(ballInstance); 
				strobes.Add (arrowVelocityInstance);
				strobes.Add (arrowForceInstance);
			}
		}
	}

	public void clearStrobes()
	{
		Debug.Log("Clearing all strobes");

		//add this back in once we get strobes working on their own
		//        ball.GetComponent<TrailRenderer>().Clear(); 

		foreach (GameObject strobe in strobes)  {
			Destroy(strobe); 
		}
	}
}