using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class ColorManager : Singleton<ColorManager> {

	public static Color BallOneVelocityColor = new Color32(0x55, 0xC7, 0xDD, 0xFF);
	public static Color BallTwoVelocityColor = new Color32(0xFF, 0xFF, 0x4C, 0xFF);
	public static Color BallOneAccelerationColor = new Color32(0x06, 0xFF, 0x64, 0xFF);
	public static Color BallTwoAccelerationColor = new Color32(0xB2, 0x4C, 0xFF, 0xFF);
	public static Color BallHorizontalVelocityColor = new Color32(0xED, 0x49, 0xFF, 0xFF);
	public static Color BallVerticalVelocityColor = new Color32(0xFF, 0x92, 0x36, 0xFD);
	public static Color BallHorizontalAccelerationColor = new Color32(0x61, 0xED, 0x00, 0xFF);
	public static Color BallVerticalAccelerationColor = new Color32(0xFF, 0xB9, 0x00, 0xFD);
	 
	// Use this for initialization
	void Start () {
//		Debug.Log ("BallOneVelocityColor");
//		BallOneVelocityColor = getColorFromMaterial(BallOneVelocityMaterial);
//
//		Debug.Log ("BallTwoVelocityColor");
//		BallTwoVelocityColor = getColorFromMaterial(BallTwoVelocityMaterial);
//
//		Debug.Log ("BallOneAccelerationColor");
//		BallOneAccelerationColor = getColorFromMaterial(BallOneAccelerationMaterial);
//
//		Debug.Log ("BallTwoAccelerationColor");
//		BallTwoAccelerationColor = getColorFromMaterial(BallTwoAccelerationMaterial);
//
//		Debug.Log ("BallHorizontalVelocityColor");
//		BallHorizontalVelocityColor = getColorFromMaterial(BallHorizontalVelocityMaterial);
//
//		Debug.Log ("BallVerticalVelocityColor");
//		BallVerticalVelocityColor = getColorFromMaterial(BallVerticalVelocityMaterial);
//
//		Debug.Log ("BallHorizontalAccelerationColor");
//		BallHorizontalAccelerationColor = getColorFromMaterial(BallHorizontalAccelerationMaterial);
//
//		Debug.Log ("BallVerticalAccelerationColor");
//		BallVerticalAccelerationColor = getColorFromMaterial(BallVerticalAccelerationMaterial);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	Color getColorFromMaterial (Material material) {
		Debug.Log ("ColorManager: "+ material.color.ToString());
		return material.color;
	}
}
