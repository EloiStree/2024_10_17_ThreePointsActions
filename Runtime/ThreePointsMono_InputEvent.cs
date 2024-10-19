using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ThreePointsMono_InputEvent : MonoBehaviour
{

    public UnityEvent<bool> m_onIsPressChanged;
    public UnityEvent m_onPressed;
    public UnityEvent m_onReleased;
    public InputActionReference m_input;
    public float m_isTrueLimit = 0.5f;
    public float m_floatIsPressingvalue;
    public bool m_isPressedValue;

    public void OnEnable()
    {
        m_input.action.Enable();
        m_input.action.performed += OnInput;
        m_input.action.canceled += OnInput;
    }

    public void OnDisable()
    {
        m_input.action.performed -= OnInput;
        m_input.action.canceled -= OnInput;
    }

    private void OnInput(InputAction.CallbackContext context)
    {
        m_floatIsPressingvalue = context.ReadValue<float>();
        bool isPressed = m_floatIsPressingvalue > m_isTrueLimit;
        if (isPressed != m_isPressedValue)
        {
            m_isPressedValue = isPressed;
          
            m_onIsPressChanged.Invoke(m_isPressedValue);
            if (m_isPressedValue)
            {
                m_onPressed.Invoke();
            }
            else
            {
                m_onReleased.Invoke();
            }
        }
    }
}
