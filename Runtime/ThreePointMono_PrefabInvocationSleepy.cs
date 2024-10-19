using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreePointMono_PrefabInvocationSleepy : MonoBehaviour
{
    public GameObject m_prefab;
    public ThreePointsTriangleDefault m_triangle;
    public bool m_invokeOnStart = false;
    public void InvokeWith(I_ThreePointsDistanceAngleGet info) {

        m_triangle.SetThreePoints(info);

    }
    [ContextMenu("Invoke")]
    public void Invoke() {
        m_triangle.GetThreePoints(out Vector3 start, out Vector3 middle, out Vector3 end);
        Vector3 centroid = new Vector3(
    (start.x + middle.x + end.x) / 3,
    (start.y + middle.y + end.y) / 3,
    (start.z + middle.z + end.z) / 3
);
        ThreePointUtility.GetCrossDirection(m_triangle, out Vector3 crossDirection, m_invokeOnStart);
        GameObject go = Instantiate(m_prefab, centroid, Quaternion.identity);
        go.transform.up = crossDirection;


    }
}

