using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThreePointsMono_ToDistanceAndAngle : MonoBehaviour
{
    public ThreePointsTriangleDefault m_triangle;

    public UnityEvent<I_ThreePointsDistanceAngleGet> m_onTriangleChanged;

    public bool m_useDrawLine = true;

    public void SetWithPoints(I_ThreePointsGet triangle)
    {
        triangle.GetThreePoints(out Vector3 start, out Vector3 middle, out Vector3 end);

        m_triangle.SetThreePoints(start, middle, end);
        m_onTriangleChanged.Invoke(m_triangle);
    }
    public void SetWithPoints(Vector3 startPoint, Vector3 middlePoint, Vector3 endPoint) { 
    
        m_triangle.SetThreePoints(startPoint, middlePoint, endPoint);
        m_onTriangleChanged.Invoke(m_triangle);
    }
    public void Update()
    {
        if (m_useDrawLine)
        {
            DrawLine();
        }
    }

    private void DrawLine()
    {
        Vector3 a = Vector3.one * 0.001f;
        m_triangle.GetPoints(out Vector3[] points);
        Debug.DrawLine(points[0]+ a, points[1] + a, Color.green);
        Debug.DrawLine(points[1] + a, points[2] + a, Color.red);
        Debug.DrawLine(points[0] + a, points[2] + a, Color.blue);
    }

}

public interface I_ThreePoints: I_ThreePointsGet, I_ThreePointsSet
{

}

public interface I_ThreePointsSet
{
    public void SetThreePoints(Vector3 start, Vector3 middle, Vector3 end);
    public void SetPoint(ThreePointCorner corner, Vector3 point);
}

public interface I_ThreePointsGet {
    public void GetPoints(out Vector3[] arrayOf3);
    public void GetPoint(ThreePointCorner corner, out Vector3 point);
    public void GetThreePoints(out Vector3 start, out Vector3 middle, out Vector3 end);
}

public interface I_ThreePointsDistanceAngleGet : I_ThreePointsGet
{
    
    public void GetCornerAngle(ThreePointCorner corner, out float angle);
    public void GetSegmentDistance(ThreePointSegment segment, out float distance);
}

[System.Serializable]
public class ThreePointsTriangleDefault : I_ThreePointsDistanceAngleGet, I_ThreePointsSet
{
    public STRUCT_ThreePointTriangle m_triangle;
    public STRUCT_ThreePointTriangleDistanceAndAngle m_distanceAndAngle;
    public void ComputerFromOrigine()
    {       
        m_distanceAndAngle.m_startMiddleDistance = Vector3.Distance(m_triangle.m_start, m_triangle.m_middle);
        m_distanceAndAngle.m_middleEndDistance = Vector3.Distance(m_triangle.m_middle, m_triangle.m_end);
        m_distanceAndAngle.m_startEndDistance = Vector3.Distance(m_triangle.m_start, m_triangle.m_end);

        m_distanceAndAngle.m_middlePointAngle = AngleBetween(m_triangle.m_start, m_triangle.m_middle, m_triangle.m_end);
        m_distanceAndAngle.m_endPointAngle = AngleBetween(m_triangle.m_start, m_triangle.m_end, m_triangle.m_middle);
        m_distanceAndAngle.m_startPointAngle = AngleBetween(m_triangle.m_middle, m_triangle.m_start, m_triangle.m_end);
    }

    private float AngleBetween(Vector3 start, Vector3 middle, Vector3 end)
    {
        Vector3 startToMiddle = middle - start;
        Vector3 endToMiddle = middle - end;

        float angle = Vector3.Angle(startToMiddle, endToMiddle);
        return angle;
    }

    public void GetPoints(out Vector3[] arrayOf3)
    {
        arrayOf3 = new Vector3[3];
        arrayOf3[0] = m_triangle.m_start;
        arrayOf3[1] = m_triangle.m_middle;
        arrayOf3[2] = m_triangle.m_end;
    }

   
    public void GetThreePoints(out Vector3 start, out Vector3 middle, out Vector3 end)
    {
        start = m_triangle.m_start;
        middle = m_triangle.m_middle;
        end = m_triangle.m_end;
    }

    public void SetThreePoints(Vector3 start, Vector3 middle, Vector3 end)
    {
        m_triangle.m_start = start;
        m_triangle.m_middle = middle;
        m_triangle.m_end = end;
        ComputerFromOrigine();
    }

    public void SetThreePoints(I_ThreePointsGet triangle)
    {
        triangle.GetThreePoints(out Vector3 start, out Vector3 middle, out Vector3 end);
        SetThreePoints(start, middle, end);
    }

    public void GetCornerAngle(ThreePointCorner corner, out float angle)
    {
        switch (corner)
        {
            case ThreePointCorner.Start:
                angle = m_distanceAndAngle.m_startPointAngle;
                break;
            case ThreePointCorner.Middle:
                angle = m_distanceAndAngle.m_middlePointAngle;
                break;
            case ThreePointCorner.End:
                angle = m_distanceAndAngle.m_endPointAngle;
                break;
            default:
                angle = 0;
                break;
        }
    }

    public void GetSegmentDistance(ThreePointSegment segment, out float distance)
    {
        switch(segment)
        {
            case ThreePointSegment.StartMiddle:
                distance = m_distanceAndAngle.m_startMiddleDistance;
                break;
            case ThreePointSegment.MiddleEnd:
                distance = m_distanceAndAngle.m_middleEndDistance;
                break;
            case ThreePointSegment.StartEnd:
                distance = m_distanceAndAngle.m_startEndDistance;
                break;
            default:
                distance = 0;
                break;
        }
    }

    public void GetPoint(ThreePointCorner corner, out Vector3 point)
    {
        switch (corner)
        {
            case ThreePointCorner.Start:
                point = m_triangle.m_start;
                break;
            case ThreePointCorner.Middle:
                point = m_triangle.m_middle;
                break;
            case ThreePointCorner.End:
                point = m_triangle.m_end;
                break;
            default:
                point = Vector3.zero;
                break;
        }
    }

    public void SetPoint(ThreePointCorner corner, Vector3 point)
    {
        switch (corner)
        {
            case ThreePointCorner.Start:
                m_triangle.m_start = point;
                break;
            case ThreePointCorner.Middle:
                m_triangle.m_middle = point;
                break;
            case ThreePointCorner.End:
                m_triangle.m_end = point;
                break;
            default:
                break;


        }
    }
}


[System.Serializable]
public struct STRUCT_ThreePointTriangle { 
    public Vector3 m_start;
    public Vector3 m_middle;
    public Vector3 m_end;
}
[System.Serializable]
public struct STRUCT_ThreePointTriangleDistanceAndAngle
{
    public float m_startMiddleDistance;
    public float m_middleEndDistance;
    public float m_startEndDistance;
    [Range(0, 180)]
    public float m_middlePointAngle;
    [Range(0, 180)]
    public float m_startPointAngle;
    [Range(0, 180)]
    public float m_endPointAngle;
}