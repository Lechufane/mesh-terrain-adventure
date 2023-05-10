using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    public int xSize = 250;
    public int zSize = 250;

    public float noiseScale = 0.05f;
    public float noiseAmplitude = 20f;


    void Start()
        {
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;

        }

    void Update()
        {
            createShape();
            updateMesh();
        }

    void createShape()
        {
            vertices = new Vector3[(xSize + 1) * (zSize + 1)];

            for (int i = 0, z = 0; z <= zSize; z++)
            {
            for (int x = 0; x <= xSize; x++)
                {
                    float y = Mathf.PerlinNoise(x * noiseScale, z * noiseScale) * noiseAmplitude;
                    vertices[i] = new Vector3(x, y, z);
                    i++;
                }
            }

            triangles = new int[xSize * zSize * 6];
            int vert = 0;
            int tris = 0;

            for(int z = 0; z < zSize; z++)
            {
                for(int x = 0; x < xSize; x++)
                {
                    triangles[tris + 0] = vert + 0;
                    triangles[tris + 1] = vert + xSize + 1;
                    triangles[tris + 2] = vert + 1;
                    triangles[tris + 3] = vert + 1;
                    triangles[tris + 4] = vert + xSize + 1;
                    triangles[tris + 5] = vert + xSize + 2;

                    vert++;
                    tris += 6;
                }
                vert++;
            }

        }

    void updateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}