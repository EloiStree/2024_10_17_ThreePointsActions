using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TriangleGeneratorMono : MonoBehaviour
{
    public string m_spaceName = "TriangleGenerator";
    public MeshFilter m_meshFilter;
    public Mesh m_mesh;
    [Range(0, 1)]
    public float m_rgbPercent = 0.1f;
     public bool m_clearAtAwake = true;

    [Header("Debug")]
    public List<STRUCT_ThreePointTriangle > m_triangles = new List<STRUCT_ThreePointTriangle>();
    public ThreePointsTriangleDefault[] m_trianglesWithInfo = new ThreePointsTriangleDefault [0]; 
     Vector3[] m_pointMesh;
     int[] m_triangleMesh;

    public Color m_groundColor = Color.red ;
    public Color m_verticalColor = Color.green ;
    public Color m_horizontalColor = Color.blue;
    public Color m_unidentifyColor = Color.yellow;

    public float m_errorAllowedAngle = 10;
    public float m_errorAllowedGroundDistance = 0.1f;

    [ContextMenu("Clear")]
    public void Clear()
    {
        m_triangles.Clear();
        m_trianglesWithInfo= new ThreePointsTriangleDefault[0];
        UpdateMesh();
    }
 
    [ContextMenu("Add Triangle for testing")]
    public void AddRandomTriangleForTesting() { 
    
        ThreePointsTriangleDefault triangle = new ThreePointsTriangleDefault();
        triangle.SetThreePoints(
            UnityEngine.Random.insideUnitSphere * 5
            , UnityEngine.Random.insideUnitSphere * 5
            , UnityEngine.Random.insideUnitSphere*5);
        AddTriangle(triangle);
    
    }

    private void Awake()
    {
        if (m_clearAtAwake)
        {
            Clear();
        }
    }


    public void AddTriangle(I_ThreePointsGet triangle) { 
    
        triangle.GetThreePoints(out Vector3 start, out Vector3 middle, out Vector3 end);
        m_triangles.Add(new STRUCT_ThreePointTriangle() {
            m_start = start,
            m_middle = middle,
            m_end = end
        });
        UpdateMesh();
    }

[ContextMenu("Update Mesh")]
public void UpdateMesh()
    {

        // Check if m_meshFilter is assigned
        if (m_meshFilter == null)
        {
            Debug.LogError("MeshFilter is not assigned.");
            return;
        }

        if(m_triangles.Count> m_trianglesWithInfo.Length)
        {
            m_trianglesWithInfo = new ThreePointsTriangleDefault[m_triangles.Count];
            for (int i = 0; i < m_triangles.Count; i++)
            {
                m_trianglesWithInfo[i] = new ThreePointsTriangleDefault();
            }
        }
        // Initialize mesh and set properties
        m_mesh = new Mesh()
        {
            name = m_spaceName,
            vertices = new Vector3[m_triangles.Count * 3],
            triangles = new int[m_triangles.Count * 3],
            normals = new Vector3[m_triangles.Count * 3],
            colors = new Color[m_triangles.Count * 3]
        };
        int []triangles= new int [m_triangles.Count * 3];
        Vector3[] vertices = new Vector3[m_triangles.Count * 3];
        Color[] colors = new Color[m_triangles.Count * 3];
        Vector3[] normals=  new Vector3[m_triangles.Count * 3];

        
        

        // Populate vertices, triangles, normals, and colors
        for (int i = 0; i < m_triangles.Count; i++)
        {
            // Set vertices
            vertices[i * 3] = m_triangles[i].m_start;
            vertices[i * 3 + 1] = m_triangles[i].m_middle;
            vertices[i * 3 + 2] = m_triangles[i].m_end;

            // Set triangles
            triangles[i * 3] = i * 3;
            triangles[i * 3 + 1] = i * 3 + 1;
            triangles[i * 3 + 2] = i * 3 + 2;

            // Calculate and set normals
            Vector3 normal = Vector3.Cross(
                m_triangles[i].m_middle - m_triangles[i].m_start,
                m_triangles[i].m_end - m_triangles[i].m_start).normalized;

            normals[i * 3] = normal;
            normals[i * 3 + 1] = normal;
            normals[i * 3 + 2] = normal;

            // Assign colors (example: different colors per vertex)


            m_trianglesWithInfo[i].SetThreePoints(m_triangles[i].m_start, m_triangles[i].m_middle, m_triangles[i].m_end);

            I_ThreePointsDistanceAngleGet t = m_trianglesWithInfo[i];
            Color color = new Color(1f,1f,1f,1f);
            float colorPercent = (1f - m_rgbPercent);
           
            if (ThreePointUtility.IsGround(t, Vector3.zero, m_errorAllowedGroundDistance))
            {
                color = m_groundColor * colorPercent;
            }
            else if (ThreePointUtility.IsVertical(t, m_errorAllowedAngle))
            {
                color = m_verticalColor * colorPercent;
            }
            else if (ThreePointUtility.IsHorizontal(t, m_errorAllowedAngle))
            {
                color = m_horizontalColor * colorPercent;
            }
            else
            {
                color = m_unidentifyColor * colorPercent;
            }

                colors[i * 3] = color+ Color.green * m_rgbPercent;
                colors[i * 3 + 1]= color + Color.red * m_rgbPercent;
                colors[i * 3 + 2]= color + Color.blue * m_rgbPercent;
            
            
        }

        // Recalculate bounds and normals
        m_mesh.vertices = vertices;
        m_mesh.triangles = triangles;
        m_pointMesh= m_mesh.vertices;
        m_triangleMesh = m_mesh.triangles;
        m_mesh.normals = normals;
        m_mesh.colors = colors;
        
        //m_mesh.bounds = new Bounds(Vector3.zero, new Vector3(65, 65, 65));
        m_mesh.RecalculateNormals();
        m_mesh.RecalculateBounds();

        // Assign mesh to the mesh filter component
        m_meshFilter.mesh = m_mesh;

        // Draw debug lines to visualize triangles in the editor
        for (int i = 0; i < m_triangles.Count; i++)
        {
            //Draw lines from the mesh vertices
            Debug.DrawLine(m_mesh.vertices[i * 3], m_mesh.vertices[i * 3 + 1], Color.green);
            Debug.DrawLine(m_mesh.vertices[i * 3 + 1], m_mesh.vertices[i * 3 + 2], Color.red);
            Debug.DrawLine(m_mesh.vertices[i * 3 + 2], m_mesh.vertices[i * 3], Color.blue);

        }
    }


    
    
    [ContextMenu("Save in Persistant path STL")]
    public void SavePersistantSTL() { 
        SaveStl(Application.persistentDataPath + "/" + m_spaceName + ".stl");
    }
    public void SaveStl(string path)
    {
        ThreePointsMeshExporter.SaveMeshAsSTL(m_spaceName, m_mesh, path);
        Debug.Log("Saved to: " + path);
    }
    [ContextMenu("Save in Persistant path OBJ")]
    public void SavePersistantOBJ() { 
        SaveObj(Application.persistentDataPath + "/" + m_spaceName + ".obj");
    }

    [ContextMenu("Save in Download path OBJ")]
    public void SaveInDownloadObj()
    {

        string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/Downloads/" + m_spaceName + ".obj";
        SaveObj(path);
    }
    [ContextMenu("Save in Download path OBJ")]
    public void SaveInDownloadObjWithDate()
    {

        string date = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
        string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/Downloads/" + m_spaceName + "_"+date+".obj";
        SaveObj(path);
    }
    [ContextMenu("Save in Project path OBJ")]
    public void SaveProjectPathOBJ() {     
        SaveObj(Application.dataPath + "/" + m_spaceName + ".obj");
    }
    
    public void SaveObj(string path)
    {
        ThreePointsMeshExporter.SaveMeshAsOBJ( m_mesh, path);
        Debug.Log("Saved to: " + path);
    }

    [ContextMenu("Load from Data Path STL")]
    public void Load()
    {
        string path = Application.persistentDataPath + "/" + m_spaceName + ".stl";
        ThreePointsMeshExporter.SaveMeshAsSTL(m_spaceName, m_mesh, path);
        Debug.Log("Loaded from: " + path);
    }

    
}

public class ThreePointsMeshExporter : MonoBehaviour
{
    public static void SaveMeshAsOBJ(Mesh mesh, string filePath)
    {
        StreamWriter writer = new StreamWriter(filePath);

        writer.WriteLine("# Unity3D Mesh Exporter");

        // Write vertices
        foreach (Vector3 v in mesh.vertices)
        {
            Vector3 worldV = (v); // To keep mesh in world position
            writer.WriteLine("v " + worldV.x + " " + worldV.y + " " + worldV.z);
        }

        // Write normals
        foreach (Vector3 n in mesh.normals)
        {
            Vector3 worldN = (n); // Transform normals
            writer.WriteLine("vn " + worldN.x + " " + worldN.y + " " + worldN.z);
        }

        // Write UVs
        foreach (Vector2 uv in mesh.uv)
        {
            writer.WriteLine("vt " + uv.x + " " + uv.y);
        }

        // Write faces
        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            int idx0 = mesh.triangles[i] + 1;
            int idx1 = mesh.triangles[i + 1] + 1;
            int idx2 = mesh.triangles[i + 2] + 1;
            writer.WriteLine("f " + idx0 + "/" + idx0 + "/" + idx0 + " " + idx1 + "/" + idx1 + "/" + idx1 + " " + idx2 + "/" + idx2 + "/" + idx2);
        }

        writer.Close();
        Debug.Log("Mesh exported to " + filePath);
    }


    
    public static void SaveMeshAsSTL(string name, Mesh mesh, string filePath)
    {
        using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
        {
            string headerText = $"{name}|" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
            byte[] headerBytes = new byte[80];
            System.Text.Encoding.ASCII.GetBytes(headerText).CopyTo(headerBytes, 0);
            writer.Write(headerBytes);
            // Number of triangles
            writer.Write(mesh.triangles.Length / 3);

            // Write each triangle
            for (int i = 0; i < mesh.triangles.Length; i += 3)
            {
                // Triangle vertices
                Vector3 v0 = mesh.vertices[mesh.triangles[i]];
                Vector3 v1 = mesh.vertices[mesh.triangles[i + 1]];
                Vector3 v2 = mesh.vertices[mesh.triangles[i + 2]];

                // Calculate face normal
                Vector3 normal = Vector3.Cross(v1 - v0, v2 - v0).normalized;

                // Write the normal vector
                writer.Write(normal.x);
                writer.Write(normal.y);
                writer.Write(normal.z);

                // Write vertices
                WriteVector(writer, v0);
                WriteVector(writer, v1);
                WriteVector(writer, v2);

                // Attribute byte count (unused)
                writer.Write((ushort)0);
            }
        }

    }

    private static void WriteVector(BinaryWriter writer, Vector3 vector)
    {
        writer.Write(vector.x);
        writer.Write(vector.y);
        writer.Write(vector.z);
    }

}
