using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChartAndGraph;

public class GraphHandlerSpeedBallOneAndTwo : GraphHandler {

	public GraphChart graphChart;
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

			graphChart.DataSource.AddPointToCategoryRealtime(VELOCITY_BALL_ONE, time, BallPhysicsManager.Instance.ball.GetComponent<Rigidbody>().velocity.magnitude);
			graphChart.DataSource.AddPointToCategoryRealtime(VELOCITY_BALL_TWO, time, BallPhysicsManager.Instance.ballTwo.GetComponent<Rigidbody>().velocity.magnitude);

			graphChart.DataSource.VerticalViewSize = maxYValue * 1.5f;
			time++;
		}

	}

    public void StopGraph()
	{
        StopRecordingGraph();
		time = 0.0f;
		maxYValue = 0.0f;
		Debug.Log("Stop coroutine: " + time);
		graphChart.DataSource.ClearAndMakeLinear(VELOCITY_BALL_ONE);
		graphChart.DataSource.ClearCategory(VELOCITY_BALL_TWO);
	}

	// Use this for initialization
	void Start()
	{
		categoryOneName = VELOCITY_BALL_ONE;
		categoryTwoName = VELOCITY_BALL_ONE;

		graphChart = GetComponent<GraphChart> ();
		timer = AddValuesToGraph();
	}

	public override string getCategoryOne() {
		return categoryOneName;
	}

	// Update is called once per frame
	void Update () {

	}
}
