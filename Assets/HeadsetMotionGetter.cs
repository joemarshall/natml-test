using UnityEngine.XR;
using System.Collections.Generic;
using UnityEngine;
public class HeadsetMotionGetter : MonoBehaviour
{

    List<XRNodeState> nodeStates = new List<XRNodeState>();

    UnityEngine.XR.InputDevice device;

    void Start()
    {
        device = InputDevices.GetDeviceAtXRNode(XRNode.CenterEye);
    }

    public Vector3 pos;
    public Vector3 vel;
    public Vector3 accel;
    public Vector3 rotations;
    

    public void Update()
    {
        float dtMult=1.0f/Time.deltaTime;
        Vector3 oldPos=pos;
        Vector3 oldVel=vel;

        InputFeatureUsage<Vector3> pos_usage = UnityEngine.XR.CommonUsages.centerEyePosition;
        if (device.TryGetFeatureValue(pos_usage, out pos))
        {
            Debug.Log("PX:" + (pos.x));
        }
        vel=(pos-oldPos)*dtMult;
        accel=(vel-oldVel)*dtMult;
        Debug.Log("VX:"+ (vel.x));
        Debug.Log("AX:"+ (accel.x));

    }

}