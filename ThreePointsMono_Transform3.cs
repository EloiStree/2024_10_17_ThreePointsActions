using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class ThreePointsMono_Transform3 : MonoBehaviour
{
    public Transform m_startPoint;
    public Transform m_middlePoint;
    public Transform m_endPoint;
    public ThreePointsTriangleDefault m_triangle;

    public UnityEvent<I_ThreePointsGet> m_onPushed;
    public bool m_useUpdateDebug =true;
    public bool m_pushAtStart = true;

    private void Start()
    {
        if (m_pushAtStart)
        {
            Push();
        }
    }


    [ContextMenu("Push")]
    public void Push()
    {
        m_triangle.SetThreePoints(m_startPoint.position, m_middlePoint.position, m_endPoint.position);
        m_onPushed.Invoke(m_triangle);
    }

    public void Update()
    {
        if (m_useUpdateDebug)
        {
            m_triangle.SetThreePoints(m_startPoint.position, m_middlePoint.position, m_endPoint.position);
            Debug.DrawLine(m_startPoint.position, m_middlePoint.position, Color.green);
            Debug.DrawLine(m_middlePoint.position, m_endPoint.position, Color.green);
            Debug.DrawLine(m_endPoint.position, m_startPoint.position, Color.green);
            Vector3 directionForward = Vector3.Cross(
                m_middlePoint.position - m_startPoint.position
                , m_endPoint.position - m_startPoint.position);
            Debug.DrawRay(m_startPoint.position, directionForward, Color.red);
        }
        
    }
}
