using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using ChartAndGraph;


public class GraphManager : Singleton<GraphManager>
{
    private const float SAMPLE_FREQ = 0.15F;

    public const string VELOCITY = "Velocity";
    public const string FORCE = "Force";

    public GraphChart graphChart;
    public GameObject graph;
    //float initTime = 0f;

    float time;
    IEnumerator timer;
    float maxYValue = 0;

    // Use this for initialization
    void Start()
    {
        graph.SetActive(false);
        timer = addValuesToGraph();
    }

    // Update is called once per frame
    void Update()
    {

        //if (GameStateManager.Instance.currentPhysicsPlayState == GameStateManager.GamePlayPhysicsState.ON)
        //{

        //    //get time in seconds since start of game
        //    float time = Time.time;
        //    //check to see that 2 seconds have passed
        //    if (initTime + 2f < time)
        //    {
        //        initTime = time;
        //        //add new (x,y) points to graph--one for VELOCITY and one for FORCE
        //        //          Graph.DataSource.AddPointToCategoryRealtime(VELOCITY, System.DateTime.Now, BallPhysicsManager.Instance.updatedVelocity.magnitude); // each time we call AddPointToCategory 
        //        //          Graph.DataSource.AddPointToCategoryRealtime(FORCE, System.DateTime.Now, BallPhysicsManager.Instance.updatedForce.magnitude); // each time we call AddPointToCategory
        //        graphChart.DataSource.AddPointToCategoryRealtime(VELOCITY, time, BallPhysicsManager.Instance.updatedVelocity.magnitude); // each time we call AddPointToCategory 
        //        graphChart.DataSource.AddPointToCategoryRealtime(FORCE, time, BallPhysicsManager.Instance.updatedForce.magnitude); // each time we call AddPointToCategory
        //    }

        //}

    }

    IEnumerator addValuesToGraph()
    {
        while (GameStateManager.Instance.currentPhysicsPlayState == GameStateManager.GamePlayPhysicsState.ON)
        {
            yield return new WaitForSeconds(SAMPLE_FREQ);

            float velocity = BallPhysicsManager.Instance.ball.GetComponent<Rigidbody>().velocity.magnitude;
            if (velocity > maxYValue) maxYValue = velocity;

            time++;
            Debug.Log("time:" + time + "    *VELOCITY: " + BallPhysicsManager.Instance.ball.GetComponent<Rigidbody>().velocity.magnitude + "   *FORCE: " + BallPhysicsManager.Instance.updatedForce.magnitude);

            graphChart.DataSource.AddPointToCategoryRealtime(VELOCITY, time, velocity);
            graphChart.DataSource.AddPointToCategoryRealtime(FORCE, time, BallPhysicsManager.Instance.updatedForce.magnitude);

            graphChart.DataSource.VerticalViewSize = maxYValue * 1.5f;
        }

    }

    public void startGraph()
    {
        graphChart.DataSource.AddPointToCategoryRealtime(VELOCITY, time, BallPhysicsManager.Instance.ball.GetComponent<Rigidbody>().velocity.magnitude);
        graphChart.DataSource.AddPointToCategoryRealtime(FORCE, time, BallPhysicsManager.Instance.updatedForce.magnitude);
        StartCoroutine(timer);
    }

    public void stopGraph()
    {
        StopCoroutine(timer);
        time = 0.0f;
        maxYValue = 0.0f;
        Debug.Log("Stop coroutine: " + time);
        graphChart.DataSource.ClearAndMakeLinear(VELOCITY);
        graphChart.DataSource.ClearCategory(FORCE);
    }
}


