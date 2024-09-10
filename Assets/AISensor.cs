using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISensor : MonoBehaviour
{

    public float distance = 10;

    public float angle = 30;

    public float height = 1.0f;

    public Color meshColor=Color.red;

    Mesh mesh;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    Mesh CreateWedgeMesh()
    {
        Mesh mesh = new Mesh();
        int numTriangles = 8;
        int numVertices = numTriangles * 3;


        Vector3[] vertices = new Vector3[numVertices];

        int[] triangles=new int[numVertices];

        Vector3 bottomCenter=Vector3.zero;
            return mesh;
    }
}
