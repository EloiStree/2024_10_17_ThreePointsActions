using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThreePointsMono_PointsQueue : MonoBehaviour
{
   
    public List<Vector3> m_newToOldPoints = new List<Vector3>();
    public int m_maxPoints = 10;
    public UnityEvent<Vector3, Vector3, Vector3> m_onNewThreePoints;
    public UnityEvent<Vector3> m_onNewPointAdded;
    public bool m_useDrawLine = true;
    public void AddPoint(Vector3 point)
    {
        m_newToOldPoints.Insert(0,point);
        if (m_newToOldPoints.Count > 10)
        {
            m_newToOldPoints.RemoveAt(m_newToOldPoints.Count-1);
        }
        if (m_newToOldPoints.Count >= 3)
        {
            m_onNewThreePoints.Invoke(m_newToOldPoints[0], m_newToOldPoints[1], m_newToOldPoints[2]);
        }
        m_onNewPointAdded.Invoke(point);
    }


    public void GetListOfPoints(out IEnumerable<Vector3> points) { 
    
        points = m_newToOldPoints;
    }
    public bool HasThreePoint() { return m_newToOldPoints.Count >=3; }
    public void GetThreeLastPoint(out Vector3[] lastPoint) { 
    
        lastPoint = new Vector3[3];
        lastPoint[0] = m_newToOldPoints[0];
        lastPoint[1] = m_newToOldPoints[1];
        lastPoint[2] = m_newToOldPoints[2];
    }
    public void GetThreeLastPoint(out Vector3 recent, out Vector3 previous, out Vector3 lastest) {

        recent = m_newToOldPoints[0];
        previous = m_newToOldPoints[1];
        lastest = m_newToOldPoints[2];
    }


    public void Update()
    {
        if(m_useDrawLine)
        {
            for (int i = 0; i < m_newToOldPoints.Count-1; i++)
            {
                Debug.DrawLine(m_newToOldPoints[i], m_newToOldPoints[i+1], i < 2 ?Color.green: Color.yellow );
            }
        }        
    }

}
