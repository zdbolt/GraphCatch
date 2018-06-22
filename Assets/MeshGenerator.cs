using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour {
    // Use this for initialization
    [SerializeField]
    MeshFilter filter;
    public float size =1;
	void Start () {
        filter.mesh = GenerateMesh();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    Mesh GenerateMesh()
    {

        Mesh mesh = new Mesh();
        List<Vector3> vectorList = new List<Vector3>();
        for (int x = -5; x <= 5; x++)
            for (int y = -5; y <= 5; y++)
            {
                vectorList.Add(new Vector3(x, y, (x * x + y * y)));
            }

        mesh.SetVertices(vectorList);

        List<int> trianglesList = new List<int>();
        int columnSize = 11;
        for (int i = 0; i < 100; i++)
            {
                trianglesList.Add(i);
                trianglesList.Add(i + 1 + columnSize);
                trianglesList.Add(i + columnSize);

                trianglesList.Add(i);
                trianglesList.Add(i + 1);
                trianglesList.Add(i + 1 + columnSize);
            }

        mesh.SetTriangles(trianglesList, 0);

        List<Vector3> normalsList = new List<Vector3>();

        for (int i = 0; i < 100; i++)
        {
            normalsList.Add(Vector3.up);
        }

         mesh.SetNormals(normalsList);

            return mesh;
    }
}
