using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreePointsMono_RootToMoveBase : MonoBehaviour
{
    public Transform m_whatToMove;

    private void Reset()
    {
        m_whatToMove = transform;
    }
    public void GetWhatToMove(out Transform whatToMove)
    {
        whatToMove = m_whatToMove;
    }
}
