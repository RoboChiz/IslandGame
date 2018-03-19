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

    //Water Drop
    public float waterDrop = 10.0f;
    private float lastwaterDrop = 0.0f;

    private MeshFilter meshFilter;
    private MeshCollider meshCollider;

    private int[,,] vertGridLayout;
    private int[,,] vertWallGridLayout;

    private Mesh generatedMesh, generatedWallMesh;

    //The Mesh
    Vector3[] finalVertices, finalNormals;
    Vector2[] finalUvs;
    int[] finalTris;

    //The Wall Mesh
    Vector3[] finalWallVertices, finalWallNormals;
    Vector2[] finalWallUvs;
    int[] finalWallTris;

    private bool meshGenerated = false;

    public float waveHeight = 0.5f;
    public float waveSpeed = 5f;

    public GameObject wallGameObject;

    private void Start()
    {
        RegenerateMesh();
    }

    // Update is called once per frame
    void Update ()
    {
		if(lastPlaneSize != planeSize || gridSize != lastGridSize || waterDrop != lastwaterDrop)
        {
            if (gridSize > 0.0f)
            {
                meshGenerated = false;
                RegenerateMesh();
                meshGenerated = true;
                lastPlaneSize = planeSize;
                lastGridSize = gridSize;
                lastwaterDrop = waterDrop;
            }
        }
	}

    void FixedUpdate()
    {
        if(meshGenerated)
        {
            int squares = Mathf.RoundToInt(planeSize / gridSize) + (planeSize % gridSize != 0f ? 1 : 0);

            //Do Spring Logic
            for (int x = 0; x <= squares; x++)
            {
                for (int y = 0; y <= squares; y++)
                {
                    //Plane
                    {
                        int id = vertGridLayout[x, 0, y];

                        float pos = Mathf.Sin((Time.time * waveSpeed) + x + y) * waveHeight;
                        float diff = pos - finalVertices[id].y;

                        finalVertices[id] += Vector3.up * diff;
                    }

                    //Wall
                    {
                        int id = vertWallGridLayout[x, 0, y];

                        float pos = Mathf.Sin((Time.time * waveSpeed) + x + y) * waveHeight;
                        float diff = pos - finalWallVertices[id].y;

                        finalWallVertices[id] += Vector3.up * diff;
                    }
                }
            }

            //Do Propogate Logic

            //Update Mesh
            generatedMesh.vertices = finalVertices;
            generatedMesh.normals = finalNormals;
            generatedMesh.uv = finalUvs;
            generatedMesh.triangles = finalTris;

            generatedWallMesh.vertices = finalWallVertices;
            generatedWallMesh.normals = finalWallNormals;
            generatedWallMesh.uv = finalWallUvs;
            generatedWallMesh.triangles = finalWallTris;
        }
    }

    /// <summary>
    /// Regenerates the entire mesh from scratch
    /// </summary>
    private void RegenerateMesh()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();

        Mesh finalMesh = new Mesh();
        Mesh finalWallMesh = new Mesh();

        //Make Lists for Mesh
        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<int> tris = new List<int>();

        List<Vector3> wallVertices = new List<Vector3>();
        List<Vector3> wallNormals = new List<Vector3>();
        List<Vector2> wallUvs = new List<Vector2>();
        List<int> wallTris = new List<int>();

        float halfSize = planeSize / 2f;
        int squares = Mathf.RoundToInt(planeSize / gridSize) + (planeSize% gridSize != 0f ? 1 : 0);

        vertGridLayout = new int[squares + 1, 1, squares + 1];
        vertWallGridLayout = new int[squares + 1, 2, squares + 1];

        //Setup Verts
        {
            for (int x = 0; x <= squares; x++)
            {
                for (int y = 0; y <= squares; y++)
                {
                    //Plane
                    vertGridLayout[x, 0, y] = vertices.Count;
                    vertices.Add(new Vector3(Mathf.Clamp(-halfSize + (x * gridSize), -halfSize, halfSize), 0f, Mathf.Clamp(-halfSize + (y * gridSize), -halfSize, halfSize)));

                    normals.Add(Vector3.up);

                    uvs.Add(new Vector2(x, y));

                    //Wall
                    if (x == 0 || y == 0 || x == squares || y == squares)
                    {
                        vertWallGridLayout[x, 0, y] = wallVertices.Count;
                        wallVertices.Add(new Vector3(Mathf.Clamp(-halfSize + (x * gridSize), -halfSize, halfSize), 0f, Mathf.Clamp(-halfSize + (y * gridSize), -halfSize, halfSize)));
                        vertWallGridLayout[x, 1, y] = wallVertices.Count;
                        wallVertices.Add(new Vector3(Mathf.Clamp(-halfSize + (x * gridSize), -halfSize, halfSize), -waterDrop, Mathf.Clamp(-halfSize + (y * gridSize), -halfSize, halfSize)));

                        wallNormals.Add(Vector3.up);
                        wallNormals.Add(Vector3.up);

                        wallUvs.Add(new Vector2(x, y));
                        wallUvs.Add(new Vector2(x, y));
                    }
                }
            }
        }

        //Setup Triangles
        for (int x = 0; x < squares; x++)
        {
            for (int y = 0; y < squares; y++)
            {
                //Tri 1
                tris.Add(vertGridLayout[x+1,0, y]);
                tris.Add(vertGridLayout[x,  0, y]);
                tris.Add(vertGridLayout[x,  0, y+1]);

                // Tri 2
                tris.Add(vertGridLayout[x + 1,  0, y + 1]);
                tris.Add(vertGridLayout[x + 1,  0, y]);
                tris.Add(vertGridLayout[x,      0, y + 1]);
            }
        }

        //Setup Walls
        for (int x = 0; x < squares; x++)
        {
            //Left Wall
            //Tri 1
            wallTris.Add(vertWallGridLayout[x,      0, 0]);
            wallTris.Add(vertWallGridLayout[x + 1,  0, 0]);
            wallTris.Add(vertWallGridLayout[x,      1, 0]);

            //Tri 2
            wallTris.Add(vertWallGridLayout[x + 1,  1, 0]);
            wallTris.Add(vertWallGridLayout[x,      1, 0]);
            wallTris.Add(vertWallGridLayout[x + 1,  0, 0]);

            //Right Wall
            //Tri 1
            wallTris.Add(vertWallGridLayout[x + 1,  0, squares]);
            wallTris.Add(vertWallGridLayout[x,      0, squares]);
            wallTris.Add(vertWallGridLayout[x,      1, squares]);

            //Tri 2
            wallTris.Add(vertWallGridLayout[x,      1, squares]);
            wallTris.Add(vertWallGridLayout[x + 1,  1, squares]);
            wallTris.Add(vertWallGridLayout[x + 1,  0, squares]);
        }

        for (int y = 0; y < squares; y++)
        {
            //Front Wall
            //Tri 1
            wallTris.Add(vertWallGridLayout[0, 0, y + 1]);
            wallTris.Add(vertWallGridLayout[0, 0, y]);
            wallTris.Add(vertWallGridLayout[0, 1, y]);

            //Tri 2
            wallTris.Add(vertWallGridLayout[0, 1, y]);
            wallTris.Add(vertWallGridLayout[0, 1, y + 1]);
            wallTris.Add(vertWallGridLayout[0, 0, y + 1]);

            //Back Wall
            //Tri 1
            wallTris.Add(vertWallGridLayout[squares, 0, y]);
            wallTris.Add(vertWallGridLayout[squares, 0, y + 1]);
            wallTris.Add(vertWallGridLayout[squares, 1, y]);

            //Tri 2
            wallTris.Add(vertWallGridLayout[squares, 1, y + 1]);
            wallTris.Add(vertWallGridLayout[squares, 1, y]);
            wallTris.Add(vertWallGridLayout[squares, 0, y + 1]);
        }

        //Set Mesh
        finalVertices = vertices.ToArray();
        finalMesh.vertices = finalVertices;

        finalNormals = normals.ToArray();
        finalMesh.normals = finalNormals;

        finalUvs = uvs.ToArray();
        finalMesh.uv = finalUvs;

        finalTris = tris.ToArray();
        finalMesh.triangles = finalTris;

        meshFilter.mesh = finalMesh;
        meshCollider.sharedMesh = finalMesh;

        generatedMesh = finalMesh;

        //Set Mesh
        finalWallVertices = wallVertices.ToArray();
        finalWallMesh.vertices = finalWallVertices;

        finalWallNormals = wallNormals.ToArray();
        finalWallMesh.normals = finalWallNormals;

        finalWallUvs = wallUvs.ToArray();
        finalWallMesh.uv = finalWallUvs;

        finalWallTris = wallTris.ToArray();
        finalWallMesh.triangles = finalWallTris;

        if(wallGameObject == null)
        {
            wallGameObject = new GameObject();
            wallGameObject.transform.parent = transform;
            wallGameObject.name = "Wall Mesh";
            wallGameObject.AddComponent<MeshFilter>();
            wallGameObject.AddComponent<MeshRenderer>();
            wallGameObject.AddComponent<MeshCollider>();
        }

        wallGameObject.GetComponent<MeshFilter>().mesh = finalWallMesh;
        wallGameObject.GetComponent<MeshCollider>().sharedMesh = finalWallMesh;
        wallGameObject.transform.localPosition = Vector3.zero;
        generatedWallMesh = finalWallMesh;

        //meshFilter.mesh = finalWallMesh;
        //meshCollider.sharedMesh = finalWallMesh;

    }
}
