using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChartAndGraph;

public class GraphHandlerHorizontalVerticalCombinedSpeed : GraphHandler {

	public GraphChart graphChart;
	public GameObject graph;
	//float initTime = 0f;

	public string categoryOneName;
	public string categoryTwoName;
    public string categoryThreeName;

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

			graphChart.DataSource.AddPointToCategoryRealtime(VELOCITY_HORIZONTAL, time, BallPhysicsManager.Instance.ball.GetComponent<Rigidbody>().velocity.x);
			graphChart.DataSource.AddPointToCategoryRealtime(VELOCITY_VERTICAL, time, Mathf.Abs(BallPhysicsManager.Instance.ball.GetComponent<Rigidbody>().velocity.y));
			graphChart.DataSource.AddPointToCategoryRealtime(VELOCITY_BALL_ONE, time, BallPhysicsManager.Instance.ball.GetComponent<Rigidbody>().velocity.magnitude);
			time++;
			graphChart.DataSource.VerticalViewSize = maxYValue * 1.5f;

		}

	}

	public override string getCategoryOne() {
		return categoryOneName;
	}

    public void resetGraph()
	{
        StopRecordingGraph();
		time = 0.0f;
		maxYValue = 0.0f;
		Debug.Log("Stop coroutine: " + time);


        foreach(string category in categoryNames) {
            graphChart.DataSource.ClearAndMakeLinear(category);    
        }

	}

	// Use this for initialization
	void Start()
	{
		categoryOneName = VELOCITY_HORIZONTAL;
		categoryTwoName = VELOCITY_VERTICAL;
		categoryThreeName = VELOCITY_BALL_ONE;

        categoryNames.Add(VELOCITY_HORIZONTAL);
        categoryNames.Add(VELOCITY_VERTICAL);
        categoryNames.Add(VELOCITY_BALL_ONE);

		timer = AddValuesToGraph();
	}

	// Update is called once per frame
	void Update () {

	}
}
