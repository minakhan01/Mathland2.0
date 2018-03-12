using BzKovSoft.RopeGenerator.MeshGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BzKovSoft.RopeGenerator
{
	public class RopeGenerator
	{
		readonly IRopeMeshGenerator _meshGenerator;
		Material _material;
		GameObject _parrent;
		float _radius = 0.1f;
		float _length = 10f;
		float _massOfBone = 0.1f;
		int _boneCount = 10;
		int _jointAngleLimit = 90;
		bool _restrictFirstBone = false;


		public RopeGenerator(IRopeMeshGenerator meshGenerator, Material material, GameObject parrent)
		{
			_meshGenerator = meshGenerator;
			_material = material;
			_parrent = parrent;
		}
		
		public int JointAngleLimit
		{
			get { return _jointAngleLimit; }
			set { _jointAngleLimit = value; }
		}

		public int BoneCount
		{
			get { return _boneCount; }

			set
			{
				if (value < 2)
					throw new ArgumentException("'BoneCount' must be >= 2");

				_boneCount = value;
			}
		}

		public float MassOfBone
		{
			get { return _massOfBone; }

			set
			{
				if (value < 0)
					throw new ArgumentException("'MassOfBone' must be >= 0");

				_massOfBone = value;
			}
		}

		public float Length
		{
			get { return _length; }

			set
			{
				if (value <= 0)
					throw new ArgumentException("'Length' must be > 0");

				_length = value;
			}
		}

		/// <summary>
		/// Sets the radius of the rope.
		/// </summary>
		public float Radius
		{
			get { return _radius; }

			set
			{
				if (value <= 0)
					throw new ArgumentException("'Radius' must be > 0");

				_radius = value;
			}
		}

		public bool RestrictFirstBone
		{
			get
			{
				return _restrictFirstBone;
			}

			set
			{
				_restrictFirstBone = value;
			}
		}

		public GameObject[] MakeOne()
		{
			var transform = _parrent.transform;
			SkinnedMeshRenderer rend = _parrent.AddComponent<SkinnedMeshRenderer>();
			if (_material != null)
				rend.material = _material;
			rend.updateWhenOffscreen = true;
			
			var mesh = _meshGenerator.Create(_radius, _length, _boneCount, _restrictFirstBone);

			// Create Bone Transforms and Bind poses
			// One bone at the bottom and one at the top
			GameObject[] bones = new GameObject[_boneCount];
			Transform[] bonesT = new Transform[_boneCount];
			Matrix4x4[] bindPoses = new Matrix4x4[_boneCount];


			for (int i = 0; i < _boneCount; i++)
			{
				bones[i] = new GameObject("Bone_" + (i + 1).ToString());
			}


			for (int i = 0; i < _boneCount; i++)
			{
				float r = (float)i / (_boneCount - 1);

				var boneT = bones[i].transform;
				bonesT[i] = boneT;
				boneT.parent = transform;
				// Set the position relative to the parent
				boneT.localRotation = Quaternion.identity;
				boneT.localPosition = new Vector3(0, r * -_length, 0);

				// The bind pose is bone's inverse transformation matrix
				// In this case the matrix we also make this matrix relative to the root
				// So that we can move the root game object around freely
				bindPoses[i] = boneT.worldToLocalMatrix * transform.localToWorldMatrix;
			}





			for (int i = 0; i < _boneCount; i++)
			{
				var bone = bones[i];

				var rigid = bone.AddComponent<Rigidbody>();
				rigid.mass = _massOfBone;

				if (i == 0)
				{
					rigid.isKinematic = true;
				}
				else
				{
					float unitHeight = _length / (_boneCount - 1);

					var collider = bone.AddComponent<CapsuleCollider>();
					collider.radius = _radius;
					collider.height = unitHeight;
					collider.center = new Vector3(0, unitHeight / 2, 0);


					var joint = bone.AddComponent<ConfigurableJoint>();
					joint.connectedBody = bones[i - 1].GetComponent<Rigidbody>();

					joint.projectionMode = JointProjectionMode.PositionAndRotation;

					joint.xMotion = ConfigurableJointMotion.Locked;
					joint.yMotion = ConfigurableJointMotion.Locked;
					joint.zMotion = ConfigurableJointMotion.Locked;

					if (i != 1 | _restrictFirstBone)
					{
						joint.angularXMotion = ConfigurableJointMotion.Limited;
						joint.angularYMotion = ConfigurableJointMotion.Locked;
						joint.angularZMotion = ConfigurableJointMotion.Limited;
					}
					else
					{
						joint.angularXMotion = ConfigurableJointMotion.Free;
						joint.angularYMotion = ConfigurableJointMotion.Free;
						joint.angularZMotion = ConfigurableJointMotion.Free;
					}

					joint.autoConfigureConnectedAnchor = false;
					joint.connectedAnchor = new Vector3(0, 0, 0);
					joint.anchor = new Vector3(0, unitHeight, 0);

					var xLowLimits = joint.lowAngularXLimit;
					xLowLimits.limit = -_jointAngleLimit;
					joint.lowAngularXLimit = xLowLimits;

					var xHighLimits = joint.highAngularXLimit;
					xHighLimits.limit = _jointAngleLimit;
					joint.highAngularXLimit = xHighLimits;

					var zLimits = joint.angularZLimit;
					zLimits.limit = _jointAngleLimit;
					joint.angularZLimit = zLimits;
				}
			}




			// assign the bindPoses array to the bindposes array which is part of the mesh. 
			mesh.bindposes = bindPoses;

			// Assign bones and bind poses
			rend.bones = bonesT;
			rend.sharedMesh = mesh;

			return bones;
		}
	}
}
