using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using Obi;

public class RopeResponse : Singleton<RopeResponse> {

	public ObiRope obiRope;
	public ObiCollider ObiColliderBall;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void removeBallRopeConnnection() {
		ObiDistanceConstraintBatch batch = obiRope.DistanceConstraints.GetFirstBatch(); // or GetBatches()[0] for pre-3.3
		int firstParticle = batch.springIndices[0];
		int lastParticle = batch.springIndices[batch.springIndices.Count-1];
		obiRope.PinConstraints.RemoveFromSolver(null);
		batch.RemoveConstraint(lastParticle);
		obiRope.PinConstraints.PushDataToSolver();
	}

	public void addBallRopeConnnection() {
		ObiDistanceConstraintBatch batch = obiRope.DistanceConstraints.GetFirstBatch(); // or GetBatches()[0] for pre-3.3
		int firstParticle = batch.springIndices[0];
		int lastParticle = batch.springIndices[batch.springIndices.Count-1];
		obiRope.PinConstraints.GetFirstBatch().AddConstraint(lastParticle, ObiColliderBall, new Vector3(0.8f, 0, 0), 1);
		obiRope.PinConstraints.AddToSolver(null);
		obiRope.PinConstraints.PushDataToSolver();
	}
}
