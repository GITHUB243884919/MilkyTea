using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildMesh : MonoBehaviour
{
    // Start is called before the first frame update
	public MeshFilter mf;
	public MeshRenderer mr;
	//public float x = 1f;
	//public float y = 1f;

	public Transform t0;
	public Transform t1;
	public Transform t2;
	public Transform t3;

	void Start()
    {
		Vector3[] vertes = new Vector3[]
		{
			new Vector3(t0.position.x, t0.position.y, 0), //0
			new Vector3(t1.position.x, t1.position.y, 0),  //1
			new Vector3(t2.position.x, t2.position.y, 0),   //2
			new Vector3(t3.position.x, t3.position.y, 0),  //3
		};
		mf.mesh.vertices = vertes;

		mf.mesh.triangles = new[]
		{
			0, 1, 2,
			0, 2, 3,

			//1      2
			// _____
			//|     |
			//|     |
			//|_____|
			//0     3
		};

		mf.mesh.uv = new Vector2[]
		{
			new Vector2(0, 0), //0
			new Vector2(0, 1), //1
			new Vector2(1, 1), //2
			new Vector2(1, 0), //3


		};
	}

    // Update is called once per frame
    void Update()
    {
		Vector3[] vertes = new Vector3[]
		{
			new Vector3(t0.position.x, t0.position.y, 0), //0
			new Vector3(t1.position.x, t1.position.y, 0),  //1
			new Vector3(t2.position.x, t2.position.y, 0),   //2
			new Vector3(t3.position.x, t3.position.y, 0),  //3
		};
		mf.mesh.vertices = vertes;
	}
}
