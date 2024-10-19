using UnityEngine;

public class ThreePointsMono_MeshColoring : MonoBehaviour { 

    public MeshFilter m_meshFilter;
    public float m_rgbPercent = 0.1f;
    public float m_errorAllowedAngle = 10;
    public float m_errorAllowedGroundDistance = 0.1f;

    [ContextMenu("Update Mesh Color with Basic")]
    public void UpdateMeshColorWithBasic()
    {
        Mesh mesh = m_meshFilter.sharedMesh;
        TriangleVertexColoring.BasicVerticalHorizontalColor(ref mesh, m_rgbPercent, m_errorAllowedAngle, m_errorAllowedGroundDistance);
        m_meshFilter.mesh = mesh;
    }
}

