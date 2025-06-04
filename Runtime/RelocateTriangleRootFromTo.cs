using Eloi;
using UnityEngine;

using Eloi.ThreePoints;
public class RelocateTriangleRootFromTo {


    public static void MoveTo(
        Transform rootToMove,
        ThreePointsMono_Transform3 fromTransform,
        ThreePointsMono_Transform3 toTransform

        )
    {
        I_ThreePointsGet from = (I_ThreePointsGet) fromTransform.m_triangle;
        I_ThreePointsGet to= (I_ThreePointsGet) toTransform.m_triangle;

        fromTransform.RefreshDataWithoutNotification();
        toTransform.RefreshDataWithoutNotification();

        MoveToCentroid(rootToMove, from, to);

        fromTransform.RefreshDataWithoutNotification();
        toTransform.RefreshDataWithoutNotification();

        GetCentroidAndClosestPoint(
            from,to,
            out Vector3 centroide,
            out Vector3 cfromPoint,
            out Vector3 ctoPoint);
        GetCentroidAndFarestPoint(
            from, to, 
            out Vector3 fcentroide,
            out Vector3 ffromPoint,
            out Vector3 ftoPoint);
        Vector3 forwardFrom = Vector3.Cross(cfromPoint - centroide, ffromPoint - centroide);
        Vector3 forwardTo = Vector3.Cross(ctoPoint - centroide, ftoPoint - centroide);

        //Debug.DrawLine(centroide, centroide + forwardFrom, Color.magenta, 10);
        //Debug.DrawLine(centroide, centroide + forwardTo , Color.yellow, 10);

        GetRotationFromTwoDirection(forwardFrom, cfromPoint - centroide, out Quaternion rotationFrom);
        GetRotationFromTwoDirection(forwardTo, ctoPoint - centroide, out Quaternion rotationTo);

        RelocationUtility.RotateTargetAroundPointMath(rootToMove, centroide, rotationFrom, rotationTo);



    }
    public static void MoveToCentroid(Transform whatToMove, I_ThreePointsGet origineTriangle, I_ThreePointsGet toTriangle)
    {

        ThreePointUtility.GetCentroid(origineTriangle,out Vector3 from);
        ThreePointUtility.GetCentroid(toTriangle, out Vector3 to);
        Vector3 move = to - from;
        whatToMove.transform.position += move;
    }
    public static void GetRotationFromTwoDirection(Vector3 forward, Vector3 upward, out Quaternion orientation)
    {
        orientation = Quaternion.LookRotation(forward, upward);
    }

    public static void GetCentroidAndClosestPoint(
        I_ThreePointsGet origine,
        I_ThreePointsGet whereToGo,
        out Vector3 centroide,
        out Vector3 fromPoint, 
        out Vector3 toPoint)
    {
        ThreePointUtility.GetCentroid( whereToGo, out centroide);
        ThreePointUtility.GetClosestPoint(origine, centroide, out ThreePointCorner _, out fromPoint, out _);
        ThreePointUtility.GetClosestPoint(whereToGo, centroide, out ThreePointCorner _, out toPoint, out _);
       }
    public static void GetCentroidAndFarestPoint(
        I_ThreePointsGet origine, 
        I_ThreePointsGet whereToGo,
        out Vector3 centroide,
        out Vector3 fromPoint, out Vector3 toPoint)
    {
        ThreePointUtility.GetCentroid(whereToGo,out centroide);
        ThreePointUtility.GetFarestPoint(origine,centroide, out ThreePointCorner _, out fromPoint, out _);
        ThreePointUtility.GetFarestPoint(whereToGo,centroide, out ThreePointCorner _, out toPoint, out _);
    }
}
