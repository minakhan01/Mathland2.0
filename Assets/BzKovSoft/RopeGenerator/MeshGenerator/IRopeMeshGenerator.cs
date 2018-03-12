using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BzKovSoft.RopeGenerator.MeshGenerator
{
	/// <summary>
	/// Mash generator for rope generator
	/// </summary>
	public interface IRopeMeshGenerator
	{
		/// <summary>
		/// Create mesh
		/// </summary>
		/// <param name="radius">Radius of rope</param>
		/// <param name="length">Length of rope</param>
		/// <param name="boneCount">Bones the mesh consist of</param>
		/// <param name="restrictFirstBone">If false, JointAngleLimit and skeletal weights will not be applied to first bone</param>
		Mesh Create(float radius, float length, int boneCount, bool restrictFirstBone);
	}
}
