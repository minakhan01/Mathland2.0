using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vectrosity;

public class VelocityAxisManager : MonoBehaviour {
    public int RESULT_POINT_IDX = 0;
    public int VELX_POINT_IDX = 1;
    public int COMMON_POINT_IDX = 2;
    public int VELY_POINT_IDX = 3;

    public VectorObject2D lines;

    Vector2 common;
    Vector2 result;
    Vector2 velY;
    Vector2 velX;

	// Use this for initialization
	void Start () {
        startPoints();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameStateManager.Instance.currentPhysicsPlayState == GameStateManager.GamePlayPhysicsState.ON) {
            updatePoints();
        }
	}

    void startPoints() {
        common = lines.vectorLine.points2[COMMON_POINT_IDX];
        result = lines.vectorLine.points2[RESULT_POINT_IDX];
        velY = lines.vectorLine.points2[VELY_POINT_IDX];
        velX = lines.vectorLine.points2[VELX_POINT_IDX];
    }

    void updatePoints() {
        //Vel X = common + velocity.y
        //Vel Y = common + velocity.x 
        //THE AXIS ARE CHANGED BECAUSE THE UI HAS TO BE ROTATED 90º SO THE REAL Y AXIS WILL SHOW THE X AXIS
        //resutl = vely
        //common is the same bc it doesn't move

        Vector3 currentBallVelocity = BallPhysicsManager.Instance.ball.GetComponent<Rigidbody>().velocity;

        velX.x = common.x + currentBallVelocity.y;
        velY.y = common.y + currentBallVelocity.x;
        result = velY;
    }
}
