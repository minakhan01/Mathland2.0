using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class ColorManager : Singleton<ColorManager> {

	public Material BallOneVelocityMaterial;
	public Material BallTwoVelocityMaterial;
	public Material BallOneAccelerationMaterial;
	public Material BallTwoAccelerationMaterial;
	public Material BallHorizontalVelocityMaterial;
	public Material BallVerticalVelocityMaterial;
	public Material BallHorizontalAccelerationMaterial;
	public Material BallVerticalAccelerationMaterial;

	public Color BallOneVelocityColor;
	public Color BallTwoVelocityColor;
	public Color BallOneAccelerationColor;
	public Color BallTwoAccelerationColor;
	public Color BallHorizontalVelocityColor;
	public Color BallVerticalVelocityColor;
	public Color BallHorizontalAccelerationColor;
	public Color BallVerticalAccelerationColor;
	 
	// Use this for initialization
	void Start () {
		Debug.Log ("BallOneVelocityColor");
		BallOneVelocityColor = getColorFromMaterial(BallOneVelocityMaterial);

		Debug.Log ("BallTwoVelocityColor");
		BallTwoVelocityColor = getColorFromMaterial(BallTwoVelocityMaterial);

		Debug.Log ("BallOneAccelerationColor");
		BallOneAccelerationColor = getColorFromMaterial(BallOneAccelerationMaterial);

		Debug.Log ("BallTwoAccelerationColor");
		BallTwoAccelerationColor = getColorFromMaterial(BallTwoAccelerationMaterial);

		Debug.Log ("BallHorizontalVelocityColor");
		BallHorizontalVelocityColor = getColorFromMaterial(BallHorizontalVelocityMaterial);

		Debug.Log ("BallVerticalVelocityColor");
		BallVerticalVelocityColor = getColorFromMaterial(BallVerticalVelocityMaterial);

		Debug.Log ("BallHorizontalAccelerationColor");
		BallHorizontalAccelerationColor = getColorFromMaterial(BallHorizontalAccelerationMaterial);

		Debug.Log ("BallVerticalAccelerationColor");
		BallVerticalAccelerationColor = getColorFromMaterial(BallVerticalAccelerationMaterial);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	Color getColorFromMaterial (Material material) {
		Debug.Log ("ColorManager: "+ material.color.ToString());
		return material.color;
	}
}
