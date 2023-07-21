using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class CaptureDataScript : MonoBehaviour
{
    public Transform headset;
    public Transform leftController;
    public Transform rightController;
    public HeadsetMotionGetter motion_getter;

    private StreamWriter writer;

    float[] dataVals;

    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
        // make a log file name based on current time
        DateTime startTime = DateTime.Now;
        string path = Application.persistentDataPath + "/" + startTime.ToString("yyyyMMdd_HHmm_ss") + "_data.csv";
        writer = new StreamWriter(path, true);
        Debug.Log("Writing log to: " + path);
        string[] names = GetColumnNames();
        string header = String.Join(",", names);
        writer.WriteLine(header);
        dataVals = new float[names.Length];
    }

    string[] GetColumnNames()
    {
        string[] names = {
        "Input.acceleration.x",
        "Input.acceleration.y",
        "Input.acceleration.z",
        "Input.gyro.rotationRateUnbiased.x",
        "Input.gyro.rotationRateUnbiased.y",
        "Input.gyro.rotationRateUnbiased.z",
        "headset.localPosition.x",
        "headset.localPosition.y",
        "headset.localPosition.z",
        "headset.localRotation.w",
        "headset.localRotation.x",
        "headset.localRotation.y",
        "headset.localRotation.z",
        "leftController.localPosition.x",
        "leftController.localPosition.y",
        "leftController.localPosition.z",
        "leftController.localPosition.x",
        "leftController.localPosition.y",
        "leftController.localPosition.z"
        };
        return names;

    }

    // Update is called once per frame
    // use this to capture data and save to the CSV file
    void Update()
    {
        dataVals[0] = motion_getter.accel.x;
        dataVals[1] = motion_getter.accel.y;
        dataVals[2] = motion_getter.accel.z;
        dataVals[3] = motion_getter.rotationVelocity.x;
        dataVals[4] = motion_getter.rotationVelocity.y;
        dataVals[5] = motion_getter.rotationVelocity.z;
        dataVals[6] = headset.localPosition.x;
        dataVals[7] = headset.localPosition.y;
        dataVals[8] = headset.localPosition.z;
        dataVals[9] = headset.localRotation.w;
        dataVals[10] = headset.localRotation.x;
        dataVals[11] = headset.localRotation.y;
        dataVals[12] = headset.localRotation.z;
        dataVals[13] = leftController.localPosition.x;
        dataVals[14] = leftController.localPosition.y;
        dataVals[15] = leftController.localPosition.z;
        dataVals[16] = leftController.localPosition.x;
        dataVals[17] = leftController.localPosition.y;
        dataVals[18] = leftController.localPosition.z;

        string outStr = "";
        for (int c = 0; c < dataVals.Length; c++)
        {
            outStr += string.Format("{0:f}", dataVals[c]);
        }

        writer.WriteLine(outStr);
    }

    void OnDestroy()
    {
        writer.Close();
    }
}
