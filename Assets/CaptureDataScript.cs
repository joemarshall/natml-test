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
    string[] dataNames;

    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
        // make a log file name based on current time
        DateTime startTime = DateTime.Now;
        string path = Application.persistentDataPath + "/" + startTime.ToString("yyyyMMdd_HHmm_ss") + "_data.csv";
        writer = new StreamWriter(path, true);
        Debug.Log("Writing log to: " + path);
        string header = String.Join(",", motion_getter.input_bindings);
        print(header);
        writer.WriteLine(header);
    }

    // Update is called once per frame
    // use this to capture data and save to the CSV file
    void Update()
    {
        Dictionary<string,Vector4> values=motion_getter.outputs;
        string[] col_names=motion_getter.input_bindings;
        string outStr = "";
        for (int c = 0; c < col_names.Length; c++)
        {
            outStr += string.Format("{0:f}", values[col_names[c]]);
        }
        print(outStr);
        writer.WriteLine(outStr);
    }

    void OnDestroy()
    {
        writer.Close();
    }
}
