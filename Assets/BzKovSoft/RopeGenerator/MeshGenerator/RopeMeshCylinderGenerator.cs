using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BzKovSoft.RopeGenerator.MeshGenerator
{
	/// <summary>
	/// Rope mesh generator that produces a cylinder
	/// </summary>
	public class RopeMeshCylinderGenerator : IRopeMeshGenerator
	{
		int _segmentsPerBone = 4;
		int _sides = 6;

		/// <summary>
		/// Sets the number of sides around the cylinder.
		/// </summary>
		public int Sides
		{
			get { return _sides; }
			set { _sides = value; }
		}

		/// <summary>
		/// Sets the number of divisions along the bone's major axis.
		/// </summary>
		public int SegmentsPerBone
		{
			get { return _segmentsPerBone; }
			set
			{
				if (value < 1)
					throw new ArgumentException("'SegPerBone' must be >= 2");

				_segmentsPerBone = value;
			}
		}

		public Mesh Create(float radius, float length, int boneCount, bool restrictFirstBone)
		{
			int totalSegments = (boneCount - 1) * _segmentsPerBone + 1;

			var mesh = CreateMesh(radius, -length, _sides, totalSegments);
			CalculateWeights(mesh, boneCount, totalSegments, restrictFirstBone);

			return mesh;
		}

		private static Mesh CreateMesh(float radius, float length, int sides, int segments)
		{
			Mesh mesh = new Mesh();
			int sidesP = sides + 1;

			#region Vertices
			Vector3[] vertices = new Vector3[sidesP * (segments) + 2];
			vertices[0] = Vector3.zero;
			vertices[vertices.Length - 1] = new Vector3(0f, length, 0f);

			const float _2pi = Mathf.PI * 2f;

			for (int segment = 0; segment < segments; segment++)
			{
				float a1 = segment / (segments - 1f) * length;

				for (int side = 0; side < sidesP; side++)
				{
					float a2 = _2pi * side / (sidesP - 1);
					float sin2 = Mathf.Sin(a2);
					float cos2 = Mathf.Cos(a2);

					vertices[
						1 +					//
						segment * sidesP +  // lat shift
						side             // lon shift
						] = new Vector3(cos2 * radius, a1, sin2 * radius);
				}
			}






			#endregion

			#region Normales
			Vector3[] normales = new Vector3[vertices.Length];
			normales[0] = Vector3.up;
			normales[normales.Length - 1] = Vector3.down;

			for (int n = 1; n < vertices.Length - 1; n++)
			{
				var v = vertices[n];
				v.y = 0;
				normales[n] = v.normalized;
			}
			#endregion

			#region UVs

			Vector2[] uvs = new Vector2[vertices.Length];
			uvs[0] = new Vector2(0.5f, -0.8f);
			uvs[uvs.Length - 1] = new Vector2(0.5f, 0.2f);

			for (int segment = 0; segment < segments; segment++)
			{
				float a1 = 1f - segment / (segments - 1f);

				for (int side = 0; side < sidesP; side++)
				{
					float a2 = side / (float)sides;

					uvs[
						1 +
						segment * sidesP +  // lat shift
						side             // lon shift
						] = new Vector2(a2, a1);
				}
			}
			#endregion





			#region Triangles

			int countOfCapTrs = sides;
			int[] triangles = new int[((segments - 1) * sides * 2 + countOfCapTrs * 2) * 3];
			int i = 0;



			// opening cap
			for (int side = 0; side < sides; side++)
			{
				int current = 0 * sidesP + side + 1;
				int currentP1 = current + 1;
				
				triangles[i++] = 0;

				if (length > 0)
				{
					triangles[i++] = current;
					triangles[i++] = currentP1;
				}
				else
				{
					triangles[i++] = currentP1;
					triangles[i++] = current;
				}
			}

			// body
			for (int segment = 0; segment < segments - 1; segment++)
			{
				for (int side = 0; side < sides; side++)
				{
					int current = segment * sidesP + side + 1;
					int next = current + sidesP;

					int currentP1 = current + 1;
					int nextP1 = next + 1;

					triangles[i++] = current;

					if (length > 0)
					{
						triangles[i++] = next;
						triangles[i++] = currentP1;
						triangles[i++] = currentP1;
						triangles[i++] = next;
						triangles[i++] = nextP1;
					}
					else
					{
						triangles[i++] = currentP1;
						triangles[i++] = next;
						triangles[i++] = currentP1;
						triangles[i++] = nextP1;
						triangles[i++] = next;
					}
				}
			}

			// closing cap
			for (int side = 0; side < sides; side++)
			{
				int current = (segments - 1) * sidesP + side + 1;
				int currentP1 = current + 1;

				triangles[i++] = vertices.Length - 1;

				if (length > 0)
				{
					triangles[i++] = currentP1;
					triangles[i++] = current;
				}
				else
				{
					triangles[i++] = current;
					triangles[i++] = currentP1;
				}
			}

			#endregion

			mesh.vertices = vertices;
			mesh.normals = normales;
			mesh.uv = uvs;
			mesh.triangles = triangles;

			mesh.RecalculateBounds();

			return mesh;
		}

		private void CalculateWeights(Mesh mesh, int boneCount, int totalSegments, bool restrictFirstBone)
		{
			int sidesP = _sides + 1;
			BoneWeight[] weights = new BoneWeight[totalSegments * sidesP + 2];

			for (int bi = 0; bi < boneCount - 1; bi++)
			{
				int boneIndex0 = bi;
				int boneIndex1 = bi + 1;

				for (int segment = 0; segment < _segmentsPerBone; segment++)
				{
					float r = (float)segment / _segmentsPerBone;
					r = Mathf.SmoothStep(0, 1, r);

					for (int i = 0; i < sidesP; i++)
					{
						int index = bi * _segmentsPerBone * sidesP + segment * sidesP + i + 1;

						var weight = weights[index];


						if (bi != 0 | restrictFirstBone)
						{
							weight.boneIndex0 = boneIndex0;
							weight.weight0 = 1f - r;
							weight.boneIndex1 = boneIndex1;
							weight.weight1 = r;

							if (weight.weight0 > 1 | weight.weight1 > 1)
								throw new InvalidOperationException();
						}
						else
						{
							weight.boneIndex0 = 1;
							weight.weight0 = 1f;
						}

						weights[index] = weight;
					}
				}
			}

			for (int i = 0; i < sidesP; i++)
			{
				int index = (boneCount - 1) * _segmentsPerBone * sidesP + i + 1;

				var weight = weights[index];

				weight.boneIndex0 = boneCount - 2;
				weight.weight0 = 0f;
				weight.boneIndex1 = boneCount - 1;
				weight.weight1 = 1f;

				weights[index] = weight;
			}
			
			// opening cap
			var weightLast = weights[0];
			weightLast.boneIndex0 = 0;
			weightLast.weight0 = 1f;
			weights[0] = weightLast;

			// closing cap
			var weightFirst = weights[weights.Length - 1];
			weightFirst.boneIndex0 = boneCount - 2;
			weightFirst.weight0 = 0f;
			weightFirst.boneIndex1 = boneCount - 1;
			weightFirst.weight1 = 1f;
			weights[weights.Length - 1] = weightFirst;

			// A BoneWeights array (weights) was just created and the boneIndex and weight assigned.
			// The weights array will now be assigned to the boneWeights array in the Mesh.
			mesh.boneWeights = weights;
		}
	}
}
