using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class BallManagerScript : MonoBehaviour {

    public GameObject ball;
    public GameObject ball2;

    public GameObject VuforiaAttachedObject;
    Vector3 vuforiaObjectLockedPosition;
    Vector3 vuforiaObjectCurrentPosition;
    Vector3 ballDirectionVector;
    bool setDirection = false;
    VectorLine directionLine;
    public GameObject ballPositionText;
    public GameObject gameManager;

    public GameObject ballPrefab;

    private int updateCount = 0;

    // trajectory predictor
    public GameObject objToLaunch;
    public Transform launchPoint;
    public bool launch;
    float force = 8f;
    public float moveSpeed = 10f;
    //create a trajectory predictor in code
    TrajectoryPredictor tp;

//    Transform lastLaunchTransform;

    public bool directionLocked;

    bool ballActivated = false;

    bool ballLaunched = false;

    // Use this for initialization
    void Start () {
        directionLine = VectorLine.SetRay(Color.green, new Vector3(0, 0, 0), new Vector3(1f, 1f, 1f));
        directionLine.Draw3D();
        Physics.IgnoreCollision(ball.GetComponent<Collider>(), ball2.GetComponent<Collider>());
        tp = GetComponent< TrajectoryPredictor > ();
    }



    // Update is called once per frame
    void Update () {
        if (setDirection)
        {
            // ball.transform.Find("3DTextPrefab").GetComponent<TextMesh>().text = "set direction";
            VectorLine.Destroy(ref directionLine);
            // Debug.Log("setDirection");
            vuforiaObjectCurrentPosition =
                VuforiaAttachedObject.transform.position;
            ballDirectionVector = vuforiaObjectCurrentPosition
                - vuforiaObjectLockedPosition;
            // Debug.Log("vuforiaObjectCurrentPosition: " + vuforiaObjectCurrentPosition);
            // Debug.Log("vuforiaObjectLockedPosition: " + vuforiaObjectLockedPosition);
            // Debug.Log("ballDirectionVector: " + ballDirectionVector);
            directionLine = VectorLine.SetRay(Color.green, vuforiaObjectLockedPosition, ballDirectionVector);
            directionLine.SetWidth(4.0f);
            directionLine.Draw3D();

            launchPoint.rotation = Quaternion.LookRotation(ballDirectionVector);

            if (directionLocked) {
                setDirection = false;
            }
            // ball.transform.Find("3DTextPrefab").gameObject.GetComponent<TextMesh>().text  = ballDirectionVector+"";
        }

        if (ballLaunched) {
            updateCount++;
            if (updateCount % 10 == 0)
            {
                Debug.Log("creating an instance of ball");
                GameObject ballInstance = Instantiate(ballPrefab, ball.transform.position, ball.transform.rotation);
                MeshRenderer ballMesh = ballInstance.GetComponent<MeshRenderer>();
                Color color = ballMesh.material.color;
                color = ball.GetComponent<TrailRenderer>().startColor;
                float velocity = ball.GetComponent<Rigidbody>().velocity.magnitude;
                float transparency = velocity / 25 + 0.5f;
                if (transparency < 1)
                {
                    color.a = transparency;
                }
                ballMesh.material.color = color;
                ballInstance.GetComponentInChildren<BallInformation>().setVelocity(velocity);

                GameObject ballInstance2 = Instantiate(ballPrefab, ball2.transform.position, ball2.transform.rotation);
                MeshRenderer ballMesh2 = ballInstance2.GetComponent<MeshRenderer>();
                Color color2 = ballMesh2.material.color;
                float velocity2 = ball2.GetComponent<Rigidbody>().velocity.magnitude;
                float transparency2 = velocity / 25 + 0.3f;
                color2 = ball2.GetComponent<TrailRenderer>().startColor;
                if (transparency2 < 1)
                {
                    color2.a = transparency2;
                }
                ballMesh2.material.color = color2;
                ballInstance2.GetComponentInChildren<BallInformation>().setVelocity(velocity2);
            }
        }
    }

    public void activateBall(Vector3 position) {
        Debug.Log("activate ball position: "+position);
        //ball.transform.position = position;
        ball.SetActive(true);
        ball.transform.position = position;
        vuforiaObjectLockedPosition = position;
        ball.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        setDirection = true;
        ball2.SetActive(true);
        ball2.transform.position = position;
        ball2.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);

        // ball.transform.Find("3DTextPrefab").gameObject.transform.position = position;
        // ball.transform.Find("3DTextPrefab").GetComponent<TextMesh>().text = position + "";
        //ballPositionText.GetComponent<TextMesh>().text = ""+position;

        Vector3 inputPosition = position;
        launchPoint.position = inputPosition;
        launchPoint.rotation = Quaternion.LookRotation(new Vector3(0.5f, 0.5f, 0.5f));
        
        ballActivated = true;
    }

    void LateUpdate()
    {
        if (ballActivated && setDirection)
        {
            //set line duration to delta time so that it only lasts the length of a frame
            tp.debugLineDuration = Time.unscaledDeltaTime;
            //tell the predictor to predict a 3d line. this will also cause it to draw a prediction line
            //because drawDebugOnPredict is set to true
            tp.Predict3D(launchPoint.position, launchPoint.forward * force, Physics.gravity);
//            lastLaunchTransform = launchPoint;

            //Debug.Log("Hit Object: " + tp.hitInfo3D.collider.gameObject.name);

            //this static method can be used as well to get line info without needing to have a component and such
            //TrajectoryPredictor.GetPoints3D(launchPoint.position, launchPoint.forward * force, Physics.gravity);

            ////info text stuff
            //if (infoText)
            //{
            //    //this will check if the predictor has a hitinfo and then if it does will update the onscreen text
            //    //to say the name of the object the line hit;
            //    if (tp.hitInfo3D.collider)
            //        infoText.text = "Hit Object: " + tp.hitInfo3D.collider.gameObject.name;
            //}
        }
        if (directionLocked)
        {
//            tp..donKillLine = true;
            // Debug.Log("launch point: " + launchPoint.position);
            //tp.Predict3D(launchPoint.position, launchPoint.forward * force, Physics.gravity);
            tp.Predict3D(launchPoint.position, launchPoint.forward * force, Physics.gravity);
        }
    }

    public void LaunchBall() {
        Rigidbody rbi = ball.GetComponent<Rigidbody>();
        //ball.transform.position = launchPoint.position;
        rbi.isKinematic = false;
        ball.transform.rotation = launchPoint.rotation;
        rbi.velocity = launchPoint.forward * force;

        Rigidbody rbi2 = ball2.GetComponent<Rigidbody>();
        rbi2.isKinematic = false;
        ball2.transform.rotation = launchPoint.rotation;
        rbi2.velocity = launchPoint.forward * force;

        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        ballLaunched = true;
    }

    public void stopBall() {
        Time.timeScale = 0.0f;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
    }

    public void playBall()
    {
        Time.timeScale = 0.05f;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
    }

    public void resetGame()
    {
    }

    public void SetDirection() {
        // draw vectrosity line
    }

    public void LockDirection()
    {
        Debug.Log("LockDirection in BallManagerScript");
        directionLocked = true;
    }
}
