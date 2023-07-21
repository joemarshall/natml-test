using UnityEngine.XR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class HeadsetMotionGetter : MonoBehaviour
{

    void Start()
    {
        if(m_PositionInput.action!=null)
        {
            m_PositionInput.action.Enable();
        }
        if(m_VelocityInput.action!=null)
        {
            m_VelocityInput.action.Enable();
        }
        if(m_AccelInput.action!=null)
        {
            m_AccelInput.action.Enable();
        }
        if(m_RotationInput.action!=null)
        {
            m_RotationInput.action.Enable();
        }
        if(m_RotationalVelocityInput.action!=null)
        {
            m_RotationalVelocityInput.action.Enable();
        }


    }


    public InputActionProperty m_PositionInput;
    public InputActionProperty m_RotationInput;

    public InputActionProperty m_VelocityInput;
    public InputActionProperty m_AccelInput;

    public InputActionProperty m_RotationalVelocityInput;


    public Vector3 pos;
    public Vector3 vel;
    public Vector3 accel;
    public Quaternion rotation;
    public Vector3 rotationVelocity;
    


    public void Update()
    {
        float dtMult=1.0f/Time.deltaTime;
        Vector3 oldPos=pos;
        Vector3 oldVel=vel;

        if(m_PositionInput.action!=null)
        {
            pos=m_PositionInput.action.ReadValue<Vector3>();
        }

        if(m_VelocityInput.action!=null)
        {
            vel=m_VelocityInput.action.ReadValue<Vector3>();
        }else{
            vel=(pos-oldPos)*dtMult;
        }

        if(m_AccelInput.action!=null)
        {
            accel=m_AccelInput.action.ReadValue<Vector3>();
        }
        if(accel==Vector3.zero)    
        {
            accel=(vel-oldVel)*dtMult;
        }

        if(m_RotationInput.action!=null)
        {
            rotation=m_RotationInput.action.ReadValue<Quaternion>();
        }

        if(m_RotationalVelocityInput.action!=null)
        {
            rotationVelocity=m_RotationalVelocityInput.action.ReadValue<Vector3>();
        }


    }

}