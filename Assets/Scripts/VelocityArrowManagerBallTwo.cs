using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class VelocityArrowManagerBallTwo : Singleton<VelocityArrowManagerBallTwo>
{

    public GameObject velocityTail, velocityHead;
    private Vector3 initialTailScale, initialHeadScale;

	public GameObject rewindUI;

    // Use this for initialization
    void Start()
    {
        if (rewindUI == null)
            rewindUI = UIManager.Instance.rewindUI;
        
        initialTailScale = velocityTail.transform.localScale;
        initialHeadScale = velocityHead.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
		if (GameStateManager.Instance.currentPhysicsPlayState == GameStateManager.GamePlayPhysicsState.ON) {
			activeArrow (true);
//            BallPhysicsManager.Instance.updateVelocityandForce();
			updateVelocityArrowAngle ();
			updateVelocityArrowSize ();
			updateVelocityArrowPosition ();
		} else if (rewindUI.activeSelf && RewindManager.Instance.sliderValue > 0)
			activeArrow (true);
		else {
            activeArrow(false);
        }

    }

    public void updateVelocityArrowPosition()
    {
        transform.position = BallPhysicsManager.Instance.ballTwo.transform.position;
        Debug.Log("new arrow position: " + transform.position);
        //Vector3 vectorTailSize = velocityTail.GetComponent<Renderer>().bounds.size; 
        //velocityHead.transform.position = transform.position + (Vector3.Scale(velocityTail.transform.forward.normalized, vectorTailSize));
    }

    public void updateVelocityArrowSize()
    {
		float ballVelocityMagnitude = BallPhysicsManager.Instance.ballTwo.GetComponent<Rigidbody>().velocity.magnitude;
        //float velocity = BallPhysicsManager.Instance.ball.GetComponent<Rigidbody>().velocity.magnitude;
        Debug.Log("ballVelocityMagnitude: " + ballVelocityMagnitude);
		if (ballVelocityMagnitude != 0f) {
			velocityTail.transform.localScale = new Vector3 (initialTailScale.x, initialTailScale.y, initialTailScale.z * ballVelocityMagnitude/2.4f);
			velocityHead.transform.localScale = new Vector3 (initialHeadScale.x, initialHeadScale.y, initialHeadScale.z * (1 / ballVelocityMagnitude));
		} else {
			velocityTail.transform.localScale = new Vector3 (0, 0, 0);
			velocityHead.transform.localScale = new Vector3 (0, 0, 0);

		}		
		Debug.Log ("velocity arrow manager velocity arrow localscale" + transform.localScale);
    }


    public void updateVelocityArrowAngle()
    {
        //Quaternion toRotation = Quaternion.FromToRotation(transform.up, BallPhysicsManager.Instance.updatedVelocity);
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(BallPhysicsManager.Instance.updatedVelocity), 1.0f * Time.time);
        //transform.rotation = Quaternion.LookRotation (new Vector3(BallPhysicsManager.Instance.updatedVelocity.z, BallPhysicsManager.Instance.updatedVelocity.y, BallPhysicsManager.Instance.updatedVelocity.x));
		Vector3 direction = BallPhysicsManager.Instance.ballTwo.GetComponent<Rigidbody>().velocity;
        Quaternion rot = Quaternion.LookRotation(direction) * Quaternion.Euler(90, 0, 0);
        Debug.Log("updateVelocityArrowAngle: - direction: " + direction);
        Debug.Log("updateVelocityArrowAngle: - rot: " + rot);
        transform.rotation = rot;
    }

    public void activeArrow(bool isActive) {
        velocityTail.SetActive(isActive);
        velocityHead.SetActive(isActive);
    }
}
