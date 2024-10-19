using System;
using UnityEngine;

public static class ThreePointUtility {

    public static string GetStringOfAnglesInDegree(I_ThreePointsDistanceAngleGet triangle)
    {
        triangle.GetCornerAngle(ThreePointCorner.Start, out float angle);
        triangle.GetCornerAngle(ThreePointCorner.Middle, out float angle2);
        triangle.GetCornerAngle(ThreePointCorner.End, out float angle3);



        return string.Format("A(°): {0:0.0} {1:0.0} {2:0.0}", angle, angle2, angle3);

    }
    public static string GetStringOfDistancesInCm(I_ThreePointsDistanceAngleGet triangle)
    {
        triangle.GetSegmentDistance(ThreePointSegment.StartMiddle, out float distance0);
        triangle.GetSegmentDistance(ThreePointSegment.MiddleEnd, out float distance1);
        triangle.GetSegmentDistance(ThreePointSegment.StartEnd, out float distance2);

        return string.Format("D(cm): {0:0.0} {1:0.0} {2:0.0}",
            distance0 * 100f,
            distance1 * 100f,
            distance2 * 100f);
    }

    public static bool IsPerpendicularAtMiddlePoint(I_ThreePointsDistanceAngleGet triangle, int errorAllowed = 5)
    {
        return IsPerpendicular(triangle, ThreePointCorner.Middle, errorAllowed);
    }
    public static bool IsPerpendicular(I_ThreePointsDistanceAngleGet triangle, ThreePointCorner corner, int errorAllowed = 5)
    {
        triangle.GetCornerAngle(corner, out float angle);
        return Math.Abs(angle - 90f) < errorAllowed;
    }

    public static bool IsDistanceAlmostEqualsTo(
        I_ThreePointsDistanceAngleGet triangle,
        ThreePointSegment segment,
        float distance,
        int errorAllowed = 5)
    {
        triangle.GetSegmentDistance(segment, out float segmentDistance);
        return Math.Abs(distance - segmentDistance) < errorAllowed;





    }


    public static bool IsGround(I_ThreePointsGet triangle,
        Vector3 groundPosition, float errorAllowed = 0.1f)
    {
       triangle.GetThreePoints(out Vector3 start, out Vector3 middle, out Vector3 end);
       return 
            IsAtPosition(groundPosition.y, start.y, errorAllowed)
            && IsAtPosition(groundPosition.y, middle.y, errorAllowed)
           && IsAtPosition(groundPosition.y, end.y, errorAllowed);
    }

    private static bool IsAtPosition(float wantedPositionAxis, float positionAxis, float errorAllowed)
    {
        return Math.Abs(wantedPositionAxis - positionAxis) < errorAllowed;
    }

    public static bool IsVertical(I_ThreePointsGet t, float angleError=5)
    {
        GetCrossDirection(t, out Vector3 directionForward,false);
        float angle = Vector3.Angle(directionForward, Vector3.up);
        if(angle > 90)
        {
            angle = 180 - angle;
        }

        return angle < angleError;

    }


    public static bool IsHorizontal(I_ThreePointsGet t,float angleError=5)
    {

        GetCrossDirection(t, out Vector3 directionForward, false);
        float angle = Vector3.Angle(directionForward, Vector3.up)-90;
        if (angle < 0)
        {
            angle = -angle;
        }
        return angle < angleError;
    }

    public static void GetCrossDirection(I_ThreePointsGet t, out Vector3 directionForward, bool inverse)
    {
        t.GetThreePoints(out Vector3 start, out Vector3 middle, out Vector3 end);
        directionForward = Vector3.Cross(
            middle - start
            , middle - end);
        if(inverse)
        {
            directionForward = -directionForward;
        }
    }

    public static void GetCentroid(I_ThreePointsGet triangle, out Vector3 centroid)
    {
        triangle.GetThreePoints(out Vector3 start, out Vector3 middle, out Vector3 end);
        centroid = new Vector3(
    (start.x + middle.x + end.x) / 3,
    (start.y + middle.y + end.y) / 3,
    (start.z + middle.z + end.z) / 3
);
    }

    public static void GetClosestPoint(
        I_ThreePointsGet triangle,
        Vector3 toPoint,
        out ThreePointCorner closestCorner,
        out Vector3 closestPosition,
        out float distance)
    {
        distance = float.MaxValue;
        triangle.GetThreePoints(out Vector3 start, out Vector3 middle, out Vector3 end);
        closestCorner = ThreePointCorner.Start;
        closestPosition = start;
        float distanceStart = Vector3.Distance(start, toPoint);
        float distanceMiddle = Vector3.Distance(middle, toPoint);
        float distanceEnd = Vector3.Distance(end, toPoint);
        if (distanceStart < distance)
        { closestCorner = ThreePointCorner.Start; distance = distanceStart; closestPosition = start; }
        if (distanceMiddle < distance)
        { closestCorner = ThreePointCorner.Middle; distance = distanceMiddle; closestPosition = middle; }
        if (distanceEnd < distance)
        { closestCorner = ThreePointCorner.End; distance = distanceEnd; closestPosition = end; }

    }

    public static void GetFarestPoint(
        I_ThreePointsGet triangle,
        Vector3 toPoint, 
        out ThreePointCorner closestCorner,
        out Vector3 closestPosition, 
        out float distance)
    {
        
        distance = 0;
        triangle.GetThreePoints(out Vector3 start, out Vector3 middle, out Vector3 end);
        closestCorner = ThreePointCorner.Start;
        closestPosition = start;
        float distanceStart = Vector3.Distance(start, toPoint);
        float distanceMiddle = Vector3.Distance(middle, toPoint);
        float distanceEnd = Vector3.Distance(end, toPoint);
        if (distanceStart > distance)
        { closestCorner = ThreePointCorner.Start; distance = distanceStart; closestPosition = start; }
        if (distanceMiddle > distance)
        { closestCorner = ThreePointCorner.Middle; distance = distanceMiddle; closestPosition = middle; }
        if (distanceEnd > distance)
        { closestCorner = ThreePointCorner.End; distance = distanceEnd; closestPosition = end; }

    }



    public static  void GetCrossProductMiddle(I_ThreePointsGet triangle,  out Vector3 cross)
    {
        triangle.GetThreePoints(out Vector3 start, out Vector3 middle, out Vector3 end);
        cross = Vector3.Cross(middle - start, middle - end);
    }





}
