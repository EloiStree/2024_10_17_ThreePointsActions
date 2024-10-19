using System;
using UnityEngine;

[ExecuteInEditMode]
public class TriangleSingleAsMeshMono : MonoBehaviour
{
    public MeshFilter m_meshFilter;
    [Range(0, 1)]
    public float m_rgbPercent = 0.1f;
    public bool m_useDrawLine = true;
    public float m_errorAllowedAngle = 10;
    public float m_errorAllowedGroundDistance = 0.1f;

    [Header("Debug")]
    public ThreePointsTriangleDefault m_triangle;
    
    public Vector3[] m_pointMesh;
    public Color[] m_colors;
    int[] m_triangleMesh;

    
    [ContextMenu("Clear")]
    public void Clear()
    {
        m_triangle.Clear();
        UpdateMesh();
    }

    [ContextMenu("Set Triangle for testing")]
    public void SetRandomTriangleForTesting()
    {

        ThreePointsTriangleDefault triangle = new ThreePointsTriangleDefault();
        triangle.SetThreePoints(
            UnityEngine.Random.insideUnitSphere * 5
            , UnityEngine.Random.insideUnitSphere * 5
            , UnityEngine.Random.insideUnitSphere * 5);
        SetTriangle(triangle);

    }



    public void SetTriangle(I_ThreePointsGet triangle)
    {

        triangle.GetThreePoints(out Vector3 start, out Vector3 middle, out Vector3 end);
        m_triangle.SetThreePoints(start, middle, end);
        UpdateMesh();
    }
    public void Update()
    {
        UpdateMesh();
    }

    //public Vector3 [] m_wtfVector3;
    //public int[] m_wtfInt;
    //public Color[] m_wtfColor;

    public int m_test;
    [ContextMenu("Update Mesh")]
    public void UpdateMesh()
    {
        // Check if m_meshFilter is assigned
        if (m_meshFilter == null)
        {
            Debug.LogError("MeshFilter is not assigned.");
            return;
        }
        if(m_meshFilter.sharedMesh == null)
        {
            m_meshFilter.sharedMesh = new Mesh();
        }

        if(m_pointMesh == null || m_pointMesh.Length != 3)
        {
            m_pointMesh = new Vector3[3];
        }
        if(m_triangleMesh == null || m_triangleMesh.Length != 3)
        {
            m_triangleMesh = new int[3];
        }
        if(m_colors == null || m_colors.Length != 3)
        {
            m_colors = new Color[3];
            m_colors[0] = Color.red;
            m_colors[1] = Color.green;
            m_colors[2] = Color.blue;
        }
        m_triangleMesh[0] = 0;
        m_triangleMesh[1] = 1;
        m_triangleMesh[2] = 2;

        m_triangle.GetThreePoints(out m_pointMesh[0], out m_pointMesh[1], out m_pointMesh[2]);





        m_meshFilter.sharedMesh.SetVertices(m_pointMesh,0,3);
        m_meshFilter.sharedMesh.SetTriangles(m_triangleMesh, 0, true);

        Mesh m = m_meshFilter.sharedMesh;
        TriangleVertexColoring.BasicVerticalHorizontalColor(ref m, m_rgbPercent, m_errorAllowedAngle,m_errorAllowedGroundDistance);
        m_meshFilter.sharedMesh = m;
        //m_wtfVector3 = m_meshFilter.sharedMesh.vertices;
        //m_wtfInt = m_meshFilter.sharedMesh.triangles;
        //m_wtfColor = m_meshFilter.sharedMesh.colors;

        // Optional: Draw debug lines to visualize the triangle
        if (m_useDrawLine)
        {
            Debug.DrawLine(m_pointMesh[0], m_pointMesh[1], Color.green);
            Debug.DrawLine(m_pointMesh[1], m_pointMesh[2], Color.red);
            Debug.DrawLine(m_pointMesh[2], m_pointMesh[0], Color.blue);
        }
    }

}
