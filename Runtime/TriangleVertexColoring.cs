using UnityEngine;

public class TriangleVertexColoring {

    public static Color m_groundColor = Color.red;
    public static Color m_verticalColor = Color.blue;
    public static Color m_horizontalColor = Color.green;
    public static Color m_unidentifyColor = Color.yellow;
    public static void BasicVerticalHorizontalColor(ref Mesh mesh 
        ,float rgbPercent=0.1f
        ,float errorAllowedAngle = 10
        ,float errorAllowedGroundDistance = 0.1f) {

        Color color = new Color(1f, 1f, 1f, 1f);
        float colorPercent = (1f - rgbPercent);
        int triangleCount = mesh.vertices.Length / 3;
        Color[] colors = new Color[mesh.vertices.Length];
        ThreePointsTriangleDefault triangle = new ThreePointsTriangleDefault();


        for (int i = 0; i < triangleCount; i++)
        {
            triangle.SetThreePoints(mesh.vertices[i * 3], mesh.vertices[i * 3 + 1], mesh.vertices[i * 3 + 2]);
            if (ThreePointUtility.IsGround(triangle, Vector3.zero, errorAllowedGroundDistance))
            {
                color = m_groundColor * colorPercent;
            }
            else if (ThreePointUtility.IsVertical(triangle, errorAllowedAngle))
            {
                color = m_verticalColor * colorPercent;
            }
            else if (ThreePointUtility.IsHorizontal(triangle, errorAllowedAngle))
            {
                color = m_horizontalColor * colorPercent;
            }

            else
            {
                color = m_unidentifyColor * colorPercent;
            }

            colors[i * 3] = color + Color.green * rgbPercent;
            colors[i * 3 + 1] = color + Color.red * rgbPercent;
            colors[i * 3 + 2] = color + Color.blue * rgbPercent;

        }
        mesh.colors = colors;

    }

}

