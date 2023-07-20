using NatML;
using NatML.Features;
using System.Threading.Tasks;
using UnityEngine;
using System;

class ModelPredictor:IMLPredictor<float[] >
{

public static async Task<ModelPredictor> CreateFromFile (MLModelData data) {
    // Load edge model
    var model = await MLEdgeModel.Create(data);
    // Create predictor
    var predictor = new ModelPredictor(model);
    // Return predictor
    return predictor;
}

private MLEdgeModel model;

private ModelPredictor(MLEdgeModel model)
{
    this.model=model;
    // show the details of the current model
    Debug.Log(model);
}

public float[] Predict (params MLFeature[] inputs) {
    // Check that the input is an image feature
    if (!(inputs[0] is MLArrayFeature<float> arrayFeature))
        throw new ArgumentException(@"Predictor makes predictions on array features");

    MLFeatureType inputType = model.inputs[0];
    
    using MLEdgeFeature edgeFeature = (inputs[0] as IMLEdgeFeature).Create(inputType);
    using var outputFeatures = model.Predict(edgeFeature);
    var result = new MLArrayFeature<float>(outputFeatures[0]);

    return result.ToArray();    
}

public void Dispose () { 
}

}