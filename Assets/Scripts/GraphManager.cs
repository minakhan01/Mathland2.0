using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using ChartAndGraph;


public class GraphManager : Singleton<GraphManager>
{

	public List<GameObject> graphs;
	//float initTime = 0f;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}		

	public void startGraph()
	{
		for (int i = 0; i < graphs.Count; i++) {
			graphs[i].GetComponent<GraphHandler> ().startGraph ();
		}
	}

	public void pauseGraph()
	{
		stopGraphRecording ();
	}

	public void stopGraph()
	{
		for (int i = 0; i < graphs.Count; i++) {
			graphs[i].GetComponent<GraphHandler> ().stopGraph ();
		}
	}

	public void stopGraphRecording()
	{
		for (int i = 0; i < graphs.Count; i++) {
			graphs[i].GetComponent<GraphHandler> ().stopGraphRecording ();
		}
	}
}