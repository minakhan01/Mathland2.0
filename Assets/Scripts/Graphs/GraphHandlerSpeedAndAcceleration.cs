﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChartAndGraph;

public class GraphHandlerSpeedAndAcceleration : GraphHandler {

	public GraphChart graphChart;
	public GameObject graph;
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
			Debug.Log("time:" + time + "    *VELOCITY: " + BallPhysicsManager.Instance.ball.GetComponent<Rigidbody>().velocity.magnitude + "   *FORCE: " + BallPhysicsManager.Instance.updatedForce.magnitude);

			graphChart.DataSource.AddPointToCategoryRealtime(VELOCITY_BALL_ONE, time, velocity);
			graphChart.DataSource.AddPointToCategoryRealtime(ACCL_BALL_ONE, time, BallPhysicsManager.Instance.updatedForce.magnitude);

			graphChart.DataSource.VerticalViewSize = maxYValue * 1.5f;
		}

	}

	public void startGraph()
	{
		graphChart.DataSource.AddPointToCategoryRealtime(VELOCITY_BALL_ONE, time, BallPhysicsManager.Instance.ball.GetComponent<Rigidbody>().velocity.magnitude);
		graphChart.DataSource.AddPointToCategoryRealtime(ACCL_BALL_ONE, time, BallPhysicsManager.Instance.updatedForce.magnitude);
		StartCoroutine(timer);
	}

	public void stopGraph()
	{
		StopCoroutine(timer);
		time = 0.0f;
		maxYValue = 0.0f;
		Debug.Log("Stop coroutine: " + time);
		graphChart.DataSource.ClearAndMakeLinear(VELOCITY_BALL_ONE);
		graphChart.DataSource.ClearCategory(ACCL_BALL_ONE);
	}

	public void stopGraphRecording()
	{
		StopCoroutine(timer);
	}

	// Use this for initialization
	void Start()
	{
		graph.SetActive(false);
		timer = addValuesToGraph();
	}

	// Update is called once per frame
	void Update () {

	}
}
