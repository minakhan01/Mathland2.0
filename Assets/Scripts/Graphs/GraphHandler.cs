using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphHandler : MonoBehaviour
{

    public const float SAMPLE_FREQ = 0.15F;

    public const string VELOCITY_BALL_ONE = "VelocityBallOne";
    public const string VELOCITY_BALL_TWO = "VelocityBallTwo";
    public const string ACCL_BALL_ONE = "AccelerationBallOne";
    public const string ACCL_BALL_TWO = "AccelerationBallTwo";
    public const string VELOCITY_HORIZONTAL = "VelocityHorizontal";
    public const string VELOCITY_VERTICAL = "VelocityVertical";
    public const string ACCL_HORIZONTAL = "AccelerationHorizontal";
    public const string ACCL_VERTICAL = "AccelerationVertical";

    //	public GraphChart graphChart;

    public int numCategories;
    public List<string> categoryNames;

    protected float time;
    protected IEnumerator timer;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGraph()
    {
        StartCoroutine(timer);
    }

    public void StopGraph()
    {

    }

    public void StopRecordingGraph()
    {
		Debug.Log ("StopRecordingGraph");
        StopCoroutine(timer);
    }
}
