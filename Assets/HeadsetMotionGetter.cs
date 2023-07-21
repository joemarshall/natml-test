using UnityEngine.XR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class HeadsetMotionGetter : MonoBehaviour
{
    [System.NonSerialized]
    public string []input_bindings=
    {
        "<XRHMD>/centerEyePosition",
        "<XRHMD>/centerEyeVelocity",
        "<XRHMD>/centerEyeAcceleration",
        "<XRHMD>/centerEyeRotation",
        "<XRHMD>/centerEyeAngularVelocity",
        "<XRController>{LeftHand}/devicePosition",
        "<XRController>{LeftHand}/deviceRotation",
        "<XRController>{RightHand}/devicePosition",
        "<XRController>{RightHand}/deviceRotation",
    };

    private InputAction []actions;
    public Dictionary<string,Vector4> outputs=new Dictionary<string, Vector4>(); 

    void Start()
    {
        actions=new InputAction[input_bindings.Length];
        for(int c=0;c<input_bindings.Length;c++)
        {
            actions[c]=new InputAction();
            actions[c].AddBinding(input_bindings[c]);
            outputs[input_bindings[c]]=Vector4.zero;
            actions[c].Enable();
        }
    }


    public void Update()
    {
        Vector4 oldVel=outputs["<XRHMD>/centerEyeVelocity"];
        Vector4 oldPos=outputs["<XRHMD>/centerEyePosition"];

        float dtMult=1.0f/Time.deltaTime;

        for(int c=0;c<actions.Length;c++)
        {
            if(input_bindings[c].EndsWith("Rotation"))
            {
                Quaternion value=actions[c].ReadValue<Quaternion>();
                outputs[input_bindings[c]]=new Vector4(value.w,value.x,value.y,value.z);
            }else
            {
                outputs[input_bindings[c]]=actions[c].ReadValue<Vector3>();
            }
        }

        // if no velocity, make it up from position change
        if(outputs["<XRHMD>/centerEyeVelocity"]==Vector4.zero)
        {
            outputs["<XRHMD>/centerEyeVelocity"]=(outputs["<XRHMD>/centerEyePosition"]-oldPos)/Time.deltaTime;
        }


        // if no acceleration, make it up
        if(outputs["<XRHMD>/centerEyeAcceleration"]==Vector4.zero)
        {
            outputs["<XRHMD>/centerEyeAcceleration"]=(outputs["<XRHMD>/centerEyeVelocity"]-oldVel)/Time.deltaTime;
        }
    }

}