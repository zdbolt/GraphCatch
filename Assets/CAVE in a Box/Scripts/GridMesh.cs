
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class GridMesh
	: MonoBehaviour
{
	public int linesX = 10;
	public int linesY = 10;
	public float stepX = 0.5f;
	public float stepY = 0.5f;
	
	void Start() {
		float minX = -0.5f * stepX * linesX;
		float maxX =  0.5f * stepX * linesX;
		float minY = -0.5f * stepY * linesY;
		float maxY =  0.5f * stepY * linesY;
		
		List<Vector3> positions = new List<Vector3>();
		List<int>     indices   = new List<int>();
		
		for(int y = 0; y < linesY; ++y) {
			float yp = minY + y * stepY;
			positions.Add(new Vector3(minX, yp, 0.0f));
			positions.Add(new Vector3(maxX, yp, 0.0f));
			
			indices.Add(2 * y + 0);
			indices.Add(2 * y + 1);
		}
		
		for(int x = 0; x < linesX; ++x) {
			float xp = minX + x * stepX;
			positions.Add(new Vector3(xp, minY, 0.0f));
			positions.Add(new Vector3(xp, maxY, 0.0f));
			
			indices.Add(2 * linesY + 2 * x + 0);
			indices.Add(2 * linesY + 2 * x + 1);
		}
		
		Mesh mesh = new Mesh();
		mesh.vertices = positions.ToArray();
		mesh.SetIndices(indices.ToArray(), MeshTopology.Lines, 0);
		
		GetComponent<MeshFilter>().mesh = mesh;
	}
};