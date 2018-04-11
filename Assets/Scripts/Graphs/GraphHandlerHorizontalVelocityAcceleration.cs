using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChartAndGraph;

public class GraphHandlerHorizontalVelocityAcceleration : GraphHandler {

	public GraphChart graphChart;
	public GameObject graph;
	//float initTime = 0f;

	public string categoryOneName;
	public string categoryTwoName;

	float maxYValue = 0;

    IEnumerator AddValuesToGraph()
	{
		while (GameStateManager.Instance.currentPhysicsPlayState == GameStateManager.GamePlayPhysicsState.ON)
		{
			yield return new WaitForSeconds(SAMPLE_FREQ);

			float velocity = BallPhysicsManager.Instance.ball.GetComponent<Rigidbody>().velocity.magnitude;
			if (velocity > maxYValue) maxYValue = velocity;


			Debug.Log("time:" + time + "    *VELOCITY: " + BallPhysicsManager.Instance.ball.GetComponent<Rigidbody>().velocity.magnitude + "   *FORCE: " + BallPhysicsManager.Instance.updatedForce.magnitude);

			graphChart.DataSource.AddPointToCategoryRealtime(VELOCITY_HORIZONTAL, time, BallPhysicsManager.Instance.ball.GetComponent<Rigidbody>().velocity.x);
			graphChart.DataSource.AddPointToCategoryRealtime(ACCL_HORIZONTAL, time, BallPhysicsManager.Instance.updatedForce.x);
			time++;
			graphChart.DataSource.VerticalViewSize = maxYValue * 1.5f;
		}

	}

	public override string getCategoryOne() {
		return categoryOneName;
	}


    public void StopGraph()
	{
        StopRecordingGraph();
		time = 0.0f;
		maxYValue = 0.0f;
		Debug.Log("Stop coroutine: " + time);
		graphChart.DataSource.ClearAndMakeLinear(VELOCITY_HORIZONTAL);
		graphChart.DataSource.ClearCategory(ACCL_HORIZONTAL);
	}

	// Use this for initialization
	void Start()
	{
		categoryOneName = VELOCITY_HORIZONTAL;
		categoryTwoName = ACCL_HORIZONTAL;

		timer = AddValuesToGraph();

        //categoryNames.Add(VELOCITY_HORIZONTAL);
        //categoryNames.Add(ACCL_HORIZONTAL);
	}

	// Update is called once per frame
	void Update () {

	}
}
