using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChartAndGraph;

public class GraphHandlerSpeedAndAcceleration : GraphHandler {

	public GraphChart graphChart;
	public GameObject graph;
	//float initTime = 0f;

	public string categoryOneName;
	public string categoryTwoName;

    //float time;
    //IEnumerator timer;
	float maxYValue = 0;

    IEnumerator AddValuesToGraph()
	{
		while (GameStateManager.Instance.currentPhysicsPlayState == GameStateManager.GamePlayPhysicsState.ON)
		{
			yield return new WaitForSeconds(SAMPLE_FREQ);

			float velocity = BallPhysicsManager.Instance.ball.GetComponent<Rigidbody>().velocity.magnitude;
			if (velocity > maxYValue) maxYValue = velocity;
			float accl = BallPhysicsManager.Instance.updatedForce.magnitude;
			if (accl < 0.1f) {
				accl = 0.1f;
			}


			if (GameStateManager.Instance.sceneHasRope) {
				graphChart.DataSource.AddPointToCategoryRealtime (VELOCITY_BALL_ONE, time, 1.0f);
				graphChart.DataSource.AddPointToCategoryRealtime (ACCL_BALL_ONE, time, 1.0f);
			} else {
				graphChart.DataSource.AddPointToCategoryRealtime (VELOCITY_BALL_ONE, time, velocity);
				graphChart.DataSource.AddPointToCategoryRealtime (ACCL_BALL_ONE, time, accl);
			}
			time++;
			graphChart.DataSource.VerticalViewSize = maxYValue * 1.5f;
		}

	}

    public void StopGraph()
	{
        StopRecordingGraph();
		time = 0.0f;
		maxYValue = 0.0f;
		graphChart.DataSource.ClearAndMakeLinear(VELOCITY_BALL_ONE);
		graphChart.DataSource.ClearCategory(ACCL_BALL_ONE);
	}

 //   public void StopRecordingGraph()
	//{
		
	//}

	// Use this for initialization
	void Start()
	{
		categoryOneName = VELOCITY_BALL_ONE;
		categoryTwoName = ACCL_BALL_ONE;
		timer = AddValuesToGraph();
	}

	public override string getCategoryOne() {
		return categoryOneName;
	}

	// Update is called once per frame
	void Update () {

	}
}
