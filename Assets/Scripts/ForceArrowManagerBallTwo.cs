﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class ForceArrowManagerBallTwo : Singleton<ForceArrowManagerBallTwo>
{

    public GameObject forceTail, forceHead;
    private Vector3 initialTailScale, initialHeadScale;

	public GameObject rewindUI;

    // Use this for initialization
    void Start()
    {
        Debug.Log("Start forcetail arrow: " + forceTail);
        initialTailScale = forceTail.transform.localScale;
        initialHeadScale = forceHead.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (rewindUI == null)
            rewindUI = UIManager.Instance.rewindUI;
		if (GameStateManager.Instance.currentPhysicsPlayState == GameStateManager.GamePlayPhysicsState.ON) {
			activeArrow (true);
//            BallPhysicsManager.Instance.updateVelocityandForce();
			updateForceArrowAngle ();
			updateForceArrowSize ();
			updateForceArrowPosition ();
		} else if (rewindUI.activeSelf && RewindManager.Instance.sliderValue > 0)
			activeArrow (true);
		else
        {
            activeArrow(false);
        }

    }

    public void updateForceArrowPosition()
    {
        transform.position = BallPhysicsManager.Instance.ballTwo.transform.position;
        Debug.Log("new arrow position: " + transform.position);
        //Vector3 vectorTailSize = velocityTail.GetComponent<Renderer>().bounds.size; 
        //velocityHead.transform.position = transform.position + (Vector3.Scale(velocityTail.transform.forward.normalized, vectorTailSize));
    }

    public void updateForceArrowSize()
    {
		float ballForceMagnitude = BallPhysicsManager.Instance.updatedForceBallTwo.magnitude;
        //float velocity = BallPhysicsManager.Instance.ball.GetComponent<Rigidbody>().velocity.magnitude;
        Debug.Log("ballForceMagnitude: " + ballForceMagnitude);
		if (ballForceMagnitude != 0) {
			forceTail.transform.localScale = new Vector3 (initialTailScale.x, initialTailScale.y, initialTailScale.z/2f);
			forceHead.transform.localScale = new Vector3 (initialHeadScale.x, initialHeadScale.y, initialHeadScale.z*1.7f);
		} else {
			forceTail.transform.localScale = new Vector3 (0, 0, 0);
			forceHead.transform.localScale = new Vector3 (0, 0, 0);

		}
    }


    public void updateForceArrowAngle()
    {
        //Quaternion toRotation = Quaternion.FromToRotation(transform.up, BallPhysicsManager.Instance.updatedVelocity);
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(BallPhysicsManager.Instance.updatedVelocity), 1.0f * Time.time);
        //transform.rotation = Quaternion.LookRotation (new Vector3(BallPhysicsManager.Instance.updatedVelocity.z, BallPhysicsManager.Instance.updatedVelocity.y, BallPhysicsManager.Instance.updatedVelocity.x));
		Vector3 direction = BallPhysicsManager.Instance.updatedForceBallTwo;
        Quaternion rot = Quaternion.LookRotation(direction) * Quaternion.Euler(90, 0, 0);
        Debug.Log("updateForceArrowAngle: - direction: " + direction);
        Debug.Log("updateForceArrowAngle: - rot: " + rot);
        transform.rotation = rot;
    }

    public void activeArrow(bool isActive)
    {
        Debug.Log("Force tail: " + forceTail);
        forceTail.SetActive(isActive);
        forceHead.SetActive(isActive);
    }
}



