using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Water2DMesh : MonoBehaviour
{
	public MeshFilter mf;
	public MeshRenderer mr;
	public float x = 1f;
	public float y = 1f;

	public bool isRotate = false;
	bool isRotating = false;
	Vector3[] vertices;
	Tweener tweener;

	void Start()
	{
		vertices = new Vector3[]
		{
			new Vector3(0, 0, 0),   //0
			new Vector3(0, y, 0),   //1
			new Vector3(x, y, 0),   //2
			new Vector3(x, 0, 0),   //3
		};

		mf.mesh.vertices = vertices;

		mf.mesh.triangles = new[]
		{
			0, 1, 2,
			0, 2, 3,
		};

		mf.mesh.uv = new Vector2[]
		{
			new Vector2(0, 0), //0
			new Vector2(0, 1), //1
			new Vector2(1, 1), //2
			new Vector2(1, 0), //3

			//1      2
			// _____
			//|     |
			//|     |
			//|_____|
			//0     3
		};


		
	}

	void RotateMeshVertices()
	{
		tweener = DOTween.To(DoTweenCB, 0f, 45f, 3f);
	}

	void DoTweenCB(float angle)
	{
		Vector3 p1 = RotatePoint(new Vector3(0, 0, 0), new Vector3(0, y, 0), angle);
		Vector3 p2 = RotatePoint(new Vector3(x, 0, 0), new Vector3(x, y, 0), angle);
		vertices[1] = p1;
		vertices[2] = p2;
		mf.mesh.vertices = vertices;
	}

	Vector3 RotatePoint(Vector3 org, Vector3 target, float angle)
	{
		return Quaternion.AngleAxis(angle, new Vector3(0, 0, 1)) * (target - org) + org;
	}

	// Update is called once per frame
	void Update()
	{
		if (isRotate && !isRotating)
		{
			isRotating = true;
			RotateMeshVertices();
		}
	}

}
