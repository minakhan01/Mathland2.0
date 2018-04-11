using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChartAndGraph;

public class GraphHandlerAccelerationBallOneandTwo : GraphHandler {

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

            VelocityAxisManager.Instance.velocitiesOverTime.Add(BallPhysicsManager.Instance.ball.GetComponent<Rigidbody>().velocity);

			float velocity = BallPhysicsManager.Instance.ball.GetComponent<Rigidbody>().velocity.magnitude;
			if (velocity > maxYValue) maxYValue = velocity;


			Debug.Log("time:" + time + "    *VELOCITY: " + BallPhysicsManager.Instance.ball.GetComponent<Rigidbody>().velocity.magnitude + "   *FORCE: " + BallPhysicsManager.Instance.updatedForce.magnitude);

			graphChart.DataSource.AddPointToCategoryRealtime(ACCL_BALL_ONE, time, BallPhysicsManager.Instance.updatedForce.magnitude);
			graphChart.DataSource.AddPointToCategoryRealtime(ACCL_BALL_TWO, time, BallPhysicsManager.Instance.updatedForceBallTwo.magnitude);
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
		graphChart.DataSource.ClearAndMakeLinear(ACCL_BALL_ONE);
		graphChart.DataSource.ClearCategory(ACCL_BALL_TWO);
	}

	// Use this for initialization
	void Start()
	{
		categoryOneName = ACCL_BALL_ONE;
		categoryTwoName = ACCL_BALL_TWO;

		graphChart = GetComponent<GraphChart> ();
		timer = AddValuesToGraph();

        //categoryNames.Add(ACCL_BALL_ONE);
        //categoryNames.Add(ACCL_BALL_TWO);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
