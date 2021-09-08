using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMesh : MonoBehaviour
{
    // Start is called before the first frame update
	public MeshFilter mf;
	public MeshRenderer mr;
	public float x = 1f;
	public float y = 1f;
	
	void Start()
    {
		Vector3[] vertes = new Vector3[]
		{
			new Vector3(-x, -y, 0), //0
			new Vector3(-x, y, 0),  //1
			new Vector3(x, y, 0),   //2
			new Vector3(x, -y, 0),  //3
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

	}
}
