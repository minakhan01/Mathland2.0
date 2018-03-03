using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrobingHandler : Singleton<StrobingHandler> {

	public GameObject ball;
	private int updateCount = 0;
	public GameObject ballPrefab;
//	public Color colorOfStrobe;

    private List<GameObject> strobes = new List<GameObject>();

    // Use this for initialization
    void Start () {

   }

		

    // Update is called once per frame
    void Update () {
//		if (BallStateManager.Instance.currentBallState == BallStateManager.BallState.Launched) {
		if (true) {
			updateCount++;
			if (updateCount % 10 == 0)
			{
				//instantiate our strobe ball
				GameObject ballInstance = Instantiate(ballPrefab, ball.transform.position, ball.transform.rotation);
				MeshRenderer ballMesh = ballInstance.GetComponent<MeshRenderer>();

				//get our ballInstance color and set it to the same color as our TrailRenderer
				Color color = ballMesh.material.color;
//				color = colorOfStrobe;
				color = ball.GetComponent<TrailRenderer>().startColor;

				//make trail appropriately transparent, and measured via velocity
				float velocity = ball.GetComponent<Rigidbody>().velocity.magnitude;
				float transparency = velocity / 25 + 0.5f;
				if (transparency < 1)
				{
					color.a = transparency;
				}

				ballMesh.material.color = color;


				//set strobe ball velocity to 0 and not affected by gravity
				ballInstance.GetComponent<Rigidbody>().velocity = Vector3.zero;
				ballInstance.GetComponent<Rigidbody> ().useGravity = false;

				//once we create 'BallInformation' property
//				ballInstance.GetComponentInChildren<BallInformation>().setVelocity(velocity);


                strobes.Add(ballInstance); 

//				GameObject ballInstance2 = Instantiate(ballPrefab, ball2.transform.position, ball2.transform.rotation);
//				MeshRenderer ballMesh2 = ballInstance2.GetComponent<MeshRenderer>();
//				Color color2 = ballMesh2.material.color;
//				float velocity2 = ball2.GetComponent<Rigidbody>().velocity.magnitude;
//				float transparency2 = velocity / 25 + 0.3f;
//				color2 = ball2.GetComponent<TrailRenderer>().startColor;
//				if (transparency2 < 1)
//				{
//					color2.a = transparency2;
//				}
//				ballMesh2.material.color = color2;
//				ballInstance2.GetComponentInChildren<BallInformation>().setVelocity(velocity2);
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
