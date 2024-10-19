using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreePointsMono_UtilityDemo : MonoBehaviour
{
    public ThreePointsTriangleDefault m_triangle;
    public string m_distanceAsString;
    public string m_angleAsString;
    public bool m_isPerpendicularMiddle;

    public Colors m_colors;
    [System.Serializable]
    public class Colors { 
        public Color m_groundColor = Color.white;
        public Color m_wallColor = Color.cyan;
        public Color m_horizontalGroundColor = Color.green;
        public Color m_verticalGroundColor = Color.red;
        public Color m_otherColor= Color.yellow;
    }

    public void SetWith(I_ThreePointsGet triangle)
    {
        m_triangle.SetThreePoints(triangle);
        Refresh();
    }


    public void SetWith(I_ThreePointsDistanceAngleGet triangle) { 
    
        m_triangle.SetThreePoints(triangle);
        Refresh();
    
    }

    private void Refresh()
    {
        m_angleAsString = ThreePointUtility.GetStringOfAnglesInDegree(m_triangle);
        m_distanceAsString = ThreePointUtility.GetStringOfDistancesInCm(m_triangle);
        m_isPerpendicularMiddle = ThreePointUtility.IsPerpendicularAtMiddlePoint(m_triangle, 10);
    }
}

public enum ThreePointSegment { StartMiddle, MiddleEnd, StartEnd }
public enum ThreePointCorner { Start, Middle, End }
