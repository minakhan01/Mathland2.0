using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChartAndGraph;

public class GraphHandlerVerticalVelocityAcceleration : GraphHandler {

	public GraphChart graphChart;
	//float initTime = 0f;

	float time;
	IEnumerator timer;
	float maxYValue = 0;

	IEnumerator addValuesToGraph()
	{
		while (GameStateManager.Instance.currentPhysicsPlayState == GameStateManager.GamePlayPhysicsState.ON)
		{
			yield return new WaitForSeconds(SAMPLE_FREQ);

			float velocity = BallPhysicsManager.Instance.ball.GetComponent<Rigidbody>().velocity.magnitude;
			if (velocity > maxYValue) maxYValue = velocity;

			time++;

			graphChart.DataSource.AddPointToCategoryRealtime(VELOCITY_VERTICAL, time, BallPhysicsManager.Instance.updatedVelocity.y);
			graphChart.DataSource.AddPointToCategoryRealtime(ACCL_VERTICAL, time, BallPhysicsManager.Instance.updatedForce.y);

			graphChart.DataSource.VerticalViewSize = maxYValue * 1.5f;
		}

	}

	public void startGraph()
	{
		graphChart.DataSource.AddPointToCategoryRealtime(VELOCITY_VERTICAL, time, BallPhysicsManager.Instance.updatedVelocity.y);
		graphChart.DataSource.AddPointToCategoryRealtime(ACCL_VERTICAL, time, BallPhysicsManager.Instance.updatedForce.y);
		StartCoroutine(timer);
	}

	public void stopGraph()
	{
		StopCoroutine(timer);
		time = 0.0f;
		maxYValue = 0.0f;
		Debug.Log("Stop coroutine: " + time);
		graphChart.DataSource.ClearAndMakeLinear(VELOCITY_VERTICAL);
		graphChart.DataSource.ClearCategory(ACCL_VERTICAL);
	}

	public void stopGraphRecording()
	{
		StopCoroutine(timer);
	}

	// Use this for initialization
	void Start()
	{
		graphChart = GetComponent<GraphChart> ();
		timer = addValuesToGraph();
	}

	// Update is called once per frame
	void Update () {

	}
}
