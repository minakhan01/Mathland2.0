using BzKovSoft.RopeGenerator.MeshGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace BzKovSoft.RopeGenerator
{
	public sealed class RopeGenDialog : EditorWindow
	{
		IRopeMeshGenerator _meshGenerator;
		int _genCounter = 0;

		GameObject _parrent;
		float _radius;
		float _length;
		float _massOfBone;
		int _boneCount;
		int _jointAngleLimit;
		bool _restrictFirstBone;
		MeshGeneratorType _meshGeneratorType;
		Material _material;

		[MenuItem("Window/BzSoft/Rope Generator")]
		private static void ShowWindow()
		{
			EditorWindow.GetWindow(typeof(RopeGenDialog), false, "Ragdoll helper");
		}

		void OnEnable()
		{
			LoadDefaultValues();
		}

		void LoadDefaultValues()
		{
			var tmpRope = new RopeGenerator(null, null, null);

			_radius = tmpRope.Radius;
			_length = tmpRope.Length;
			_massOfBone = tmpRope.MassOfBone;
			_boneCount = tmpRope.BoneCount;
			_jointAngleLimit = tmpRope.JointAngleLimit;
			_restrictFirstBone = tmpRope.RestrictFirstBone;

			_meshGeneratorType = MeshGeneratorType.Cylinder;
		}

		void OnGUI()
		{
			Handles.BeginGUI();
			GUILayout.BeginVertical("box");

			_parrent = (GameObject)EditorGUILayout.ObjectField("Parrent", _parrent, typeof(GameObject), true);
			_material = (Material)EditorGUILayout.ObjectField("Material", _material, typeof(Material), true);

			_radius = EditorGUILayout.FloatField("Radius", _radius);
			_length = EditorGUILayout.FloatField("Length", _length);
			_massOfBone = EditorGUILayout.FloatField("Mass of bone", _massOfBone);
			_boneCount = EditorGUILayout.IntField("Bone Count", _boneCount);
			_jointAngleLimit = EditorGUILayout.IntField("Joint angle limit", _jointAngleLimit);
			_restrictFirstBone = EditorGUILayout.Toggle("Restrict first bone", _restrictFirstBone);
			
			GUILayout.Label("Mesh type:");
			_meshGeneratorType = (MeshGeneratorType)EditorGUILayout.EnumPopup(_meshGeneratorType);

			++EditorGUI.indentLevel;
			switch (_meshGeneratorType)
			{
				case MeshGeneratorType.Cylinder:
					DrawPanelMeshGeneratorCylinder();
					break;

				default: throw new NotSupportedException();
			}
			--EditorGUI.indentLevel;

			if (GUILayout.Button("Make Rope"))
			{
				MakeRope();
			}

			GUILayout.EndVertical();
			Handles.EndGUI();
		}

		private void DrawPanelMeshGeneratorCylinder()
		{
			RopeMeshCylinderGenerator meshGenerator = _meshGenerator as RopeMeshCylinderGenerator;
			if (meshGenerator == null)
			{
				meshGenerator = new RopeMeshCylinderGenerator();
				_meshGenerator = meshGenerator;
			}
			meshGenerator.Sides = EditorGUILayout.IntField("Sides", meshGenerator.Sides);
			meshGenerator.SegmentsPerBone = EditorGUILayout.IntField("Segments per bone", meshGenerator.SegmentsPerBone);
		}

		private void MakeRope()
		{
			if (_meshGenerator == null)
			{
				Debug.LogError("MeshGenerator is not specified");
				return;
			}

			var go = new GameObject("New Rope (" + (++_genCounter).ToString() + ")");
			go.transform.localPosition = new Vector3(0, 0, 0);

			if (_parrent != null)
				go.transform.parent = _parrent.transform;

			var tmpRope = new RopeGenerator(_meshGenerator, _material, go);
			ApplayParams(tmpRope);
			tmpRope.MakeOne();
		}

		private void ApplayParams(RopeGenerator tmpRope)
		{
			tmpRope.Radius = _radius;
			tmpRope.Length = _length;
			tmpRope.MassOfBone = _massOfBone;
			tmpRope.BoneCount = _boneCount;
			tmpRope.JointAngleLimit = _jointAngleLimit;
			tmpRope.RestrictFirstBone = _restrictFirstBone;
		}

		enum MeshGeneratorType
		{
			Cylinder,
		}
	}
}
