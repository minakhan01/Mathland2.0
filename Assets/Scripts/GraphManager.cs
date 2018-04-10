using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using ChartAndGraph;


public class GraphManager : Singleton<GraphManager>
{

    public List<GraphHandler> graphs;
    List<GraphHandler> activeGraphs = new List<GraphHandler>();

    // Use this for initialization
    void Start()
    {
        initGraph();
        for (int i = 0; i < graphs.Count; i++)
        {
            if (graphs[i].gameObject.activeSelf)
            {
                Debug.Log("Graph - Adding new active graph");
                activeGraphs.Add(graphs[i]);
            }
        }
        Debug.Log("Graph - active graphs count: " + activeGraphs.Count);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GraphChartBase getFirstGraph()
    {
        return activeGraphs[0].gameObject.GetComponent<GraphChart>();
    }

    public void initGraph()
    {
        switch (GameStateManager.Instance.scene)
        {
            case 1:
                graphs[1].gameObject.SetActive(true);
                break;
            case 2:
                graphs[1].gameObject.SetActive(true);
                break;
            case 3:
                graphs[0].gameObject.SetActive(true);
                break;
            case 4:
                graphs[0].gameObject.SetActive(true);
                break;
            case 5:
                graphs[4].gameObject.SetActive(true);
                break;
            case 6:
                graphs[4].gameObject.SetActive(true);
                break;
            case 7:
                graphs[4].gameObject.SetActive(true);
                break;
            case 8:
                graphs[0].gameObject.SetActive(true);
                break;
            case 9:
                graphs[4].gameObject.SetActive(true);
                break;
            case 10:
                graphs[0].gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void startGraph()
    {
        for (int i = 0; i < activeGraphs.Count; i++)
        {
            GraphHandler graphHandler = activeGraphs[i];

            Debug.Log("Graph - type of graph: " + graphHandler.GetType());
            Debug.Log("Graph - is equals to speed and acc: " + (graphHandler.GetType() == typeof(GraphHandlerSpeedAndAcceleration)));

            if (graphHandler.GetType() == typeof(GraphHandlerSpeedBallOneAndTwo)) ((GraphHandlerSpeedBallOneAndTwo)graphHandler).StartGraph();
            if (graphHandler.GetType() == typeof(GraphHandlerSpeedAndAcceleration)) ((GraphHandlerSpeedAndAcceleration)graphHandler).StartGraph();
            if (graphHandler.GetType() == typeof(GraphHandlerAccelerationBallOneandTwo)) ((GraphHandlerAccelerationBallOneandTwo)graphHandler).StartGraph();
            if (graphHandler.GetType() == typeof(GraphHandlerVerticalVelocityAcceleration)) ((GraphHandlerVerticalVelocityAcceleration)graphHandler).StartGraph();
            if (graphHandler.GetType() == typeof(GraphHandlerHorizontalVelocityAcceleration)) ((GraphHandlerHorizontalVelocityAcceleration)graphHandler).StartGraph();
            if (graphHandler.GetType() == typeof(GraphHandlerHorizontalVerticalCombinedSpeed)) ((GraphHandlerHorizontalVerticalCombinedSpeed)graphHandler).StartGraph();
        }
    }

    public void pauseGraph()
    {
        stopGraphRecording();
    }

    public void stopGraph()
    {
        for (int i = 0; i < activeGraphs.Count; i++)
        {
            GraphHandler graphHandler = activeGraphs[i];
            Debug.Log("Graph stopGraph");

            if (graphHandler.GetType() == typeof(GraphHandlerSpeedBallOneAndTwo)) ((GraphHandlerSpeedBallOneAndTwo)graphHandler).StopGraph();
            if (graphHandler.GetType() == typeof(GraphHandlerSpeedAndAcceleration)) ((GraphHandlerSpeedAndAcceleration)graphHandler).StopGraph();
            if (graphHandler.GetType() == typeof(GraphHandlerAccelerationBallOneandTwo)) ((GraphHandlerAccelerationBallOneandTwo)graphHandler).StopGraph();
            if (graphHandler.GetType() == typeof(GraphHandlerVerticalVelocityAcceleration)) ((GraphHandlerVerticalVelocityAcceleration)graphHandler).StopGraph();
            if (graphHandler.GetType() == typeof(GraphHandlerHorizontalVelocityAcceleration)) ((GraphHandlerHorizontalVelocityAcceleration)graphHandler).StopGraph();
            if (graphHandler.GetType() == typeof(GraphHandlerHorizontalVerticalCombinedSpeed)) ((GraphHandlerHorizontalVerticalCombinedSpeed)graphHandler).StopGraph();
        }
    }

    public void stopGraphRecording()
    {
        for (int i = 0; i < activeGraphs.Count; i++)
        {
            GraphHandler graphHandler = activeGraphs[i];
            Debug.Log("Graph stopGraphRecording");

            if (graphHandler.GetType() == typeof(GraphHandlerSpeedBallOneAndTwo)) ((GraphHandlerSpeedBallOneAndTwo)graphHandler).StopRecordingGraph();
            if (graphHandler.GetType() == typeof(GraphHandlerSpeedAndAcceleration)) ((GraphHandlerSpeedAndAcceleration)graphHandler).StopRecordingGraph();
            if (graphHandler.GetType() == typeof(GraphHandlerAccelerationBallOneandTwo)) ((GraphHandlerAccelerationBallOneandTwo)graphHandler).StopRecordingGraph();
            if (graphHandler.GetType() == typeof(GraphHandlerVerticalVelocityAcceleration)) ((GraphHandlerVerticalVelocityAcceleration)graphHandler).StopRecordingGraph();
            if (graphHandler.GetType() == typeof(GraphHandlerHorizontalVelocityAcceleration)) ((GraphHandlerHorizontalVelocityAcceleration)graphHandler).StopRecordingGraph();
            if (graphHandler.GetType() == typeof(GraphHandlerHorizontalVerticalCombinedSpeed)) ((GraphHandlerHorizontalVerticalCombinedSpeed)graphHandler).StopRecordingGraph();
        }
    }

}