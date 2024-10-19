using UnityEngine;

public class ThreePointsMono_QuickFollowIt : MonoBehaviour
{
    public Transform m_whatToMove;
    public Transform m_whatToFollow;
    private void Reset()
    {
        m_whatToMove = transform;
    }
    void Update()
    {
        
        if(m_whatToMove ==null || m_whatToFollow == null)
            return;
        m_whatToMove.position = m_whatToFollow.position;
        m_whatToMove.rotation = m_whatToFollow.rotation;
    }
}
