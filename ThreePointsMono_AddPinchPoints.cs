using UnityEngine;
using UnityEngine.Events;

public class ThreePointsMono_AddPinchPoints : MonoBehaviour
{

    public Transform m_thumbTip;
    public Transform m_indexTip;
    public UnityEvent<Vector3> m_onPointDiffused;

   


    public bool IsTransformValide()
    {
        return m_thumbTip != null && m_indexTip != null;
    }

    public bool IsPointsActive()
    {
        return m_thumbTip.gameObject.activeInHierarchy && m_indexTip.gameObject.activeInHierarchy;
    }

    [ContextMenu("Push Tracked As Vector3")]
    public void PushTrackedAsVector3()
    {
        if (IsTransformValide() )
            m_onPointDiffused.Invoke(m_thumbTip.position);
    }

    [ContextMenu("Push Tracked As Vector3 if active")]
    public void PushTrackedAsVector3IfActive()
    {
        if (IsTransformValide()  && IsPointsActive())
            m_onPointDiffused.Invoke(GetBetweenFingerPosition());
    }

    private Vector3 GetBetweenFingerPosition()
    {
        return (m_thumbTip.position + m_indexTip.position) / 2;
    }

}
