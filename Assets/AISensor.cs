using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class AISensor : MonoBehaviour
{

    public float distance = 10;
    public float angle = 30;
    public float height = 1.0f;
    public Color meshColor = Color.red;
    public int scanFrequnecy = 30;
    public LayerMask layers;


    Collider[] colliders=new Collider[50];
    Mesh mesh;
    int count;
    float scanInteral;
    float scanTimer;

    // Start is called before the first frame update
    void Start()
    {
        scanInteral = 1.0f / scanFrequnecy;
    }

    // Update is called once per frame
    void Update()
    {
        scanTimer-=Time.deltaTime;
        if(scanTimer < 0)
        {
            scanTimer += scanInteral;
            Scan();
        }
    }

    private void Scan()
    {
        count = Physics.OverlapSphereNonAlloc(transform.position, distance, colliders, layers, QueryTriggerInteraction.Collide);
    }

    Mesh CreateWedgeMesh()
    {
        Mesh mesh = new Mesh();

        int segments = 10; // Number of segments for the wedge
        int numTriangles = (segments * 4) + 2 + 2; // Total triangles for the wedge
        int numVertices = numTriangles * 3; // Total vertices for the wedge

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];
        Vector3[] normals = new Vector3[numVertices]; // Array to store normals

        Vector3 bottomCenter = Vector3.zero;
        Vector3 topCenter = bottomCenter + Vector3.up * height;

        int vert = 0;
        float currentAngle = -angle;
        float deltaAngle = (angle * 2) / segments;

        // Generate wedge segments
        for (int i = 0; i < segments; ++i)
        {
            Vector3 bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * distance;
            Vector3 bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * distance;
            Vector3 topLeft = bottomLeft + Vector3.up * height;
            Vector3 topRight = bottomRight + Vector3.up * height;

            // Create front face (bottom left, bottom right, top right)
            vertices[vert] = bottomLeft;
            normals[vert++] = Vector3.up;
            vertices[vert] = bottomRight;
            normals[vert++] = Vector3.up;
            vertices[vert] = topRight;
            normals[vert++] = Vector3.up;

            // Create front face (top right, top left, bottom left)
            vertices[vert] = topRight;
            normals[vert++] = Vector3.up;
            vertices[vert] = topLeft;
            normals[vert++] = Vector3.up;
            vertices[vert] = bottomLeft;
            normals[vert++] = Vector3.up;

            // Create side face (bottom center, bottom left, top left)
            vertices[vert] = bottomCenter;
            normals[vert++] = Vector3.forward;
            vertices[vert] = bottomLeft;
            normals[vert++] = Vector3.forward;
            vertices[vert] = topLeft;
            normals[vert++] = Vector3.forward;

            // Create side face (bottom center, top left, top center)
            vertices[vert] = bottomCenter;
            normals[vert++] = Vector3.forward;
            vertices[vert] = topLeft;
            normals[vert++] = Vector3.forward;
            vertices[vert] = topCenter;
            normals[vert++] = Vector3.forward;

            currentAngle += deltaAngle;
        }

        // Top face
        vertices[vert] = topCenter;
        normals[vert++] = Vector3.up;
        vertices[vert] = topCenter + Vector3.right * distance;
        normals[vert++] = Vector3.up;
        vertices[vert] = topCenter - Vector3.right * distance;
        normals[vert++] = Vector3.up;

        // Bottom face
        vertices[vert] = bottomCenter;
        normals[vert++] = Vector3.down;
        vertices[vert] = bottomCenter + Vector3.right * distance;
        normals[vert++] = Vector3.down;
        vertices[vert] = bottomCenter - Vector3.right * distance;
        normals[vert++] = Vector3.down;

        // Assign the triangles
        for (int i = 0; i < numVertices; ++i)
        {
            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals; // Assign normals to the mesh
        mesh.RecalculateBounds();

        return mesh;
    }



    private void OnValidate()
    {
        mesh = CreateWedgeMesh();
        scanInteral = 1.0f / scanFrequnecy;
    }
    private void OnDrawGizmos()
    {
        if (mesh)
        {
            Gizmos.color = meshColor;
            Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
        }
        Gizmos.DrawWireSphere(transform.position, distance);
        for(int i = 0;i<count; ++i)
        {
            Gizmos.DrawSphere(colliders[i].transform.position, 0.2f);
        }
    }
}
