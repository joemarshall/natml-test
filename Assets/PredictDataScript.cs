using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NatML;
using NatML.Features;
using System.Threading.Tasks;

public class PredictDataScript : MonoBehaviour
{
    // the shape of the input data to the predictor
    int [] INPUT_SHAPE={1,512,3};

    public MLModelData model_android;
    public MLModelData model_windows;
    public HeadsetMotionGetter motion_getter;

    private ModelPredictor predictor;

    private MLArrayFeature<float> databuffer;

    // Start is called before the first frame update
    void Start()
    {
        databuffer=new MLArrayFeature<float>(new float[INPUT_SHAPE[0]*INPUT_SHAPE[1]*INPUT_SHAPE[2]],INPUT_SHAPE);
        CreateModel();
    }

    async void CreateModel()
    {
        if (Application.isEditor)
        {
            predictor=await ModelPredictor.CreateFromFile(model_windows);
        }else
        {
            predictor=await ModelPredictor.CreateFromFile(model_android);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(predictor == null)return;

        int datalen=databuffer.shape[1];
        // shift the history in the buffer along
        for(int c=datalen-1;c>0;c--)
        {
            databuffer[0,c,0]=databuffer[0,c-1,0];
        }
        // add accelerometer data to buffer
        databuffer[0,0,0]=motion_getter.accel.x;
        databuffer[0,0,1]=motion_getter.accel.y;
        databuffer[0,0,2]=motion_getter.accel.z;

        float[] outputs=predictor.Predict(databuffer);        
        Debug.Log("Output data: "+outputs[0]);
    }
}
