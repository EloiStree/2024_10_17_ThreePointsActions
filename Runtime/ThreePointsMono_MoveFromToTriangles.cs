using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Eloi.ThreePoints
{

    [ExecuteInEditMode]
    public class ThreePointsMono_MoveFromToTriangles : MonoBehaviour
    {

        public ThreePointsMono_RootToMoveBase m_whatToMove;
        public ThreePointsMono_Transform3 m_origineAnchor;
        public ThreePointsMono_Transform3 m_whereToGoAnchor;

        public bool m_useDebug;

        [ContextMenu("Move and rotate")]
        public void MoveAndRotate()
        {
            RelocateTriangleRootFromTo.MoveTo(
                m_whatToMove.transform,
                m_origineAnchor,
                m_whereToGoAnchor);


        }





        [ContextMenu("Move to centroid")]
        public void MoveToCentroid()
        {

            RefreshPosition();
            m_origineAnchor.GetCentroid(out Vector3 from);
            m_whereToGoAnchor.GetCentroid(out Vector3 to);
            Vector3 move = to - from;
            m_whatToMove.transform.position += move;
        }

        private void RefreshPosition()
        {
            m_origineAnchor.RefreshDataWithoutNotification();
            m_whereToGoAnchor.RefreshDataWithoutNotification();
        }




        void Update()
        {
            if (m_useDebug)
            {
                if (m_whatToMove == null || m_origineAnchor == null || m_whereToGoAnchor == null)
                {
                    return;
                }

                m_origineAnchor.GetCentroid(out Vector3 start);
                m_origineAnchor.GetCentroid(out Vector3 end);
                Debug.DrawLine(start, end, Color.red);

                m_whereToGoAnchor.GetCentroid(out Vector3 start2);
                m_whereToGoAnchor.GetCrossProductAtMiddle(out Vector3 cross);
                Debug.DrawLine(start2, start2 + cross, Color.cyan);

                m_origineAnchor.GetCentroid(out Vector3 start3);
                m_origineAnchor.GetCrossProductAtMiddle(out Vector3 cross3);
                Debug.DrawLine(start3, start3 + cross3, Color.cyan);
            }
        }
    }
}