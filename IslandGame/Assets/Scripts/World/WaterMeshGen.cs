using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class WaterMeshGen : MonoBehaviour
{
    //The size of the water plane
    public float planeSize = 10f;
    private float lastPlaneSize = 0.0f;

    //Grid Resolution
    public float gridSize = 1.0f;
    private float lastGridSize = 0.0f;

    private MeshFilter meshFilter;

	// Update is called once per frame
	void Update ()
    {
		if(lastPlaneSize != planeSize || gridSize != lastGridSize)
        {
            if (gridSize > 0.0f)
            {
                RegenerateMesh();
                lastPlaneSize = planeSize;
                lastGridSize = gridSize;
            }
        }
	}

    /// <summary>
    /// Regenerates the entire mesh from scratch
    /// </summary>
    private void RegenerateMesh()
    {
        meshFilter = GetComponent<MeshFilter>();

        Mesh finalMesh = new Mesh();

        //Make Lists for Mesh
        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<int> tris = new List<int>();

        float halfSize = planeSize / 2f;
        int squares = Mathf.RoundToInt((planeSize / gridSize) + 0.5f);

        int[,] gridLayout = new int[squares, squares];

        //Setup Verts
        {
            for (int x = 0; x < squares; x++)
            {
                for (int y = 0; y < squares; y++)
                {
                    gridLayout[x, y] = vertices.Count;
                    vertices.Add(new Vector3(-halfSize + (x * gridSize), 0f, -halfSize + (y * gridSize)));
                    normals.Add(Vector3.up);
                    uvs.Add(new Vector2(x, y));
                }
            }
        }

        //Setup Triangles
        for (int x = 0; x < squares - 1; x++)
        {
            for (int y = 0; y < squares - 1; y++)
            {
                //Tri 1
                tris.Add(gridLayout[x+1, y]);
                tris.Add(gridLayout[x, y]);
                tris.Add(gridLayout[x, y+1]);

                // Tri 2
                tris.Add(gridLayout[x + 1, y + 1]);
                tris.Add(gridLayout[x + 1, y]);
                tris.Add(gridLayout[x, y + 1]);
            }
        }

        //Set Mesh
        finalMesh.vertices = vertices.ToArray();
        finalMesh.normals = normals.ToArray();
        finalMesh.uv = uvs.ToArray();
        finalMesh.triangles = tris.ToArray();

        meshFilter.mesh = finalMesh;

    }
}
