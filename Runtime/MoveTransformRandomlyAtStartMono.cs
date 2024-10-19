using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTransformRandomlyAtStartMono : MonoBehaviour
{

    public float m_maxDistance = 10;
    
    void Start()
    {
        MoveRandomlyAroundZero();

    }

    [ContextMenu("MoveRandomlyAroundZero")]
    private void MoveRandomlyAroundZero()
    {
        Vector3 randomPosition = Random.insideUnitSphere * m_maxDistance;
        transform.position = randomPosition;
        transform.rotation = Random.rotation;
    }
}
