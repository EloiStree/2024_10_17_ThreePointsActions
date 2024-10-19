using UnityEngine;
using UnityEngine.Events;

public class ThreePointsMono_AwakeDestroyEvents : MonoBehaviour{

    public UnityEvent m_awake;
    public UnityEvent m_start;
    public UnityEvent m_onApplicationQuit;
    public UnityEvent m_destroy;

    public void Awake() { 
        m_awake.Invoke();
    }
    public void Start() { 
        m_start.Invoke();
    }
    public void OnApplicationQuit() { 
        m_onApplicationQuit.Invoke();
    }
    public void OnDestroy() { 
        m_destroy.Invoke();
    }
}
