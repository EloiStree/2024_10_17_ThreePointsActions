using UnityEngine;
using UnityEngine.Events;

public class ThreePointsMono_RelayNextConstructTriangle : MonoBehaviour
{

    public ThreePointsTriangleDefault m_triangle;
    public UnityEvent<I_ThreePointsDistanceAngleGet> m_onTriangleReceived;
    public int m_trianglePointReceived=0;

    [ContextMenu("Start Listening to construction")]
    public void StartListeningToTriangleBuilding() { 
        m_trianglePointReceived = 0;
    }
    public void CheckIfThirdTriangleReceived() { 
        if(m_trianglePointReceived==3)
        {
            m_onTriangleReceived.Invoke(m_triangle);
        }
    }
    public void NotifyNewPoint(Vector3 start, Vector3 middle, Vector3 end)
    {
        m_triangle.SetThreePoints(start, middle, end);
        m_trianglePointReceived++;
        CheckIfThirdTriangleReceived();
    }
    public void NotifyNewPoint(I_ThreePointsGet triangle)
    {
        if (triangle == null)
            return;
        triangle.GetThreePoints(out Vector3 start, out Vector3 middle, out Vector3 end);
        m_triangle.SetThreePoints(start, middle, end);
        m_trianglePointReceived++;
        CheckIfThirdTriangleReceived();
    }
}
