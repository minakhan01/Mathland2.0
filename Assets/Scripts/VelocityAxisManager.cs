using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vectrosity;
using ChartAndGraph;

public class VelocityAxisManager : MonoBehaviour
{
    public int RESULT_POINT_IDX = 5;
    public int VELX_POINT_IDX = 3;
    public int COMMON_POINT_IDX = 0;
    public int VELY_POINT_IDX = 1;


    public float VelocityXMagnitude = 1f;
    public float VelocityYMagnitude = 1f;

    public VectorObject2D lines;

    Vector2 common;
    Vector2 result;
    Vector2 velY;
    Vector2 velX;

    public GraphChartBase chart;

    // Use this for initialization
    void Start()
    {
        startPoints();
    }

    // Update is called once per frame
    void Update()
    {
        if (chart == null)
            chart = GraphManager.Instance.getFirstGraph();
        if (RewindManager.Instance.isRecording)
        {
            updatePoints();
            lines.vectorLine.Draw();
        }
        else
        {
            Debug.Log("VELOCITY AXIS MANAGER - is rewinding");
            updatePointsOnRewind();
            lines.vectorLine.Draw();
        }
    }

    void startPoints()
    {
        common = lines.vectorLine.points2[COMMON_POINT_IDX];
        result = lines.vectorLine.points2[RESULT_POINT_IDX];
        velY = lines.vectorLine.points2[VELY_POINT_IDX];
        velX = lines.vectorLine.points2[VELX_POINT_IDX];
    }

    void updatePoints()
    {
        //Vel X = common + velocity.y
        //Vel Y = common + velocity.x 
        //THE AXIS ARE CHANGED BECAUSE THE UI HAS TO BE ROTATED 90º SO THE REAL Y AXIS WILL SHOW THE X AXIS
        //resutl = vely
        //common is the same bc it doesn't move

        Vector3 currentBallVelocity = BallPhysicsManager.Instance.ball.GetComponent<Rigidbody>().velocity;

        velX.y = common.y - 30 * Mathf.Abs(currentBallVelocity.x) * VelocityYMagnitude;
        velY.x = common.x + 30 * Mathf.Abs(currentBallVelocity.y) * VelocityXMagnitude;
        result.y = velX.y;
        result.x = velY.x;
        lines.vectorLine.points2[COMMON_POINT_IDX] = common;
        lines.vectorLine.points2[2] = common;
        lines.vectorLine.points2[4] = common;
        lines.vectorLine.points2[VELY_POINT_IDX] = velY;
        lines.vectorLine.points2[VELX_POINT_IDX] = velX;
        lines.vectorLine.points2[RESULT_POINT_IDX] = result;
    }

    void updatePointsOnRewind()
    {
        DoubleVector3 last;

        chart.DataSource.GetLastPoint(chart.GetComponent<GraphHandler>().categoryNames[0], out last);
        //int currentX = (int)(RewindManager.Instance.sliderValue * (float)last.x);

        Vector3 currentBallVelocity = BallPhysicsManager.Instance.ball.GetComponent<Rigidbody>().velocity;

        Debug.Log("VELOCITY AXIS MANAGER - currentBallVelocity " + currentBallVelocity);

        double ratio = RewindManager.Instance.sliderValue;
        ratio = RewindManager.Instance.rewindRatio;
        int currentX = (int)(ratio * last.x);

        Debug.Log("VELOCITY AXIS MANAGER - ratio " + ratio);

        float newVelocityHorizontal = (float)chart.DataSource.GetPoint(chart.GetComponent<GraphHandler>().categoryNames[0], currentX).y;
        float newVelocityVertical = (float)chart.DataSource.GetPoint(chart.GetComponent<GraphHandler>().categoryNames[1], currentX).y;

        Debug.Log("VELOCITY AXIS MANAGER - currentX " + currentX);
        Debug.Log("VELOCITY AXIS MANAGER - new vel horizontal " + newVelocityHorizontal);
        Debug.Log("VELOCITY AXIS MANAGER - new vel vertical " + newVelocityVertical);

        velX.y = common.y - 30 * Mathf.Abs(newVelocityHorizontal) * VelocityYMagnitude;
        velY.x = common.x + 30 * Mathf.Abs(newVelocityVertical) * VelocityXMagnitude;
        result.y = velX.y;
        result.x = velY.x;
        lines.vectorLine.points2[COMMON_POINT_IDX] = common;
        lines.vectorLine.points2[2] = common;
        lines.vectorLine.points2[4] = common;
        lines.vectorLine.points2[VELY_POINT_IDX] = velY;
        lines.vectorLine.points2[VELX_POINT_IDX] = velX;
        lines.vectorLine.points2[RESULT_POINT_IDX] = result;
    }



    //	public int RESULT_POINT_IDX = 0;
    //    public int VELX_POINT_IDX = 1;
    //    public int COMMON_POINT_IDX = 2;
    //    public int VELY_POINT_IDX = 3;
    //
    //    public VectorObject2D lines;
    //
    //    Vector2 common;
    //    Vector2 result;
    //    Vector2 velY;
    //    Vector2 velX;
    //
    //	// Use this for initialization
    //	void Start () {
    //        startPoints();
    //	}
    //	
    //	// Update is called once per frame
    //	void Update () {
    //        if (GameStateManager.Instance.currentPhysicsPlayState == GameStateManager.GamePlayPhysicsState.ON) {
    //            updatePoints();
    //			lines.vectorLine.Draw ();
    //        }
    //	}
    //
    //    void startPoints() {
    //        common = lines.vectorLine.points2[COMMON_POINT_IDX];
    //        result = lines.vectorLine.points2[RESULT_POINT_IDX];
    //        velY = lines.vectorLine.points2[VELY_POINT_IDX];
    //        velX = lines.vectorLine.points2[VELX_POINT_IDX];
    //    }
    //
    //    void updatePoints() {
    //        //Vel X = common + velocity.y
    //        //Vel Y = common + velocity.x 
    //        //THE AXIS ARE CHANGED BECAUSE THE UI HAS TO BE ROTATED 90º SO THE REAL Y AXIS WILL SHOW THE X AXIS
    //        //resutl = vely
    //        //common is the same bc it doesn't move
    //
    //        Vector3 currentBallVelocity = BallPhysicsManager.Instance.ball.GetComponent<Rigidbody>().velocity;
    //
    //        velX.x = common.x + currentBallVelocity.y;
    //        velY.y = common.y + currentBallVelocity.x;
    //        result = velY;
    //    }
}
