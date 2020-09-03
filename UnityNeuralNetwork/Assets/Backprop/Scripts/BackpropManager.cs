using System.Collections.Generic;
using System;
using UnityEngine;
using MLGames;
using static MLGames.NeuralNetwork;
using static MLGames.NNSettings;

public class BackpropManager : MonoBehaviour
{
    NeuralNetwork net;
    public int[] layers = new int[4]{ 3, 5, 5, 1 };
    //string[] activation = new string[2] { "leakyrelu", "leakyrelu" };
    public ActivationFunctions[] activation = new ActivationFunctions[3] { ActivationFunctions.leakyrelu, ActivationFunctions.leakyrelu, ActivationFunctions.leakyrelu };

    void Start()
    {
        this.OldWay();
        this.NewWay();
    }
    void OldWay()
    {
        this.net = new NeuralNetwork(layers, activation);
        for (int i = 0; i < 20000; i++)
        {
            net.BackPropagate(new float[] { 0, 0, 0 },new float[] { 0 });
            net.BackPropagate(new float[] { 1, 0, 0 },new float[] { 1 });
            net.BackPropagate(new float[] { 0, 1, 0 },new float[] { 1 });
            net.BackPropagate(new float[] { 0, 0, 1 },new float[] { 1 });
            net.BackPropagate(new float[] { 1, 1, 0 },new float[] { 1 });
            net.BackPropagate(new float[] { 0, 1, 1 },new float[] { 1 });
            net.BackPropagate(new float[] { 1, 0, 1 },new float[] { 1 });
            net.BackPropagate(new float[] { 1, 1, 1 },new float[] { 1 });
        }
        print("cost: "+net.cost);
        
        UnityEngine.Debug.Log(net.FeedForward(new float[] { 0, 0, 0 })[0] + "***");
        UnityEngine.Debug.Log(net.FeedForward(new float[] { 1, 0, 0 })[0]);
        UnityEngine.Debug.Log(net.FeedForward(new float[] { 0, 1, 0 })[0]);
        UnityEngine.Debug.Log(net.FeedForward(new float[] { 0, 0, 1 })[0]);
        UnityEngine.Debug.Log(net.FeedForward(new float[] { 1, 1, 0 })[0]);
        UnityEngine.Debug.Log(net.FeedForward(new float[] { 0, 1, 1 })[0]);
        UnityEngine.Debug.Log(net.FeedForward(new float[] { 1, 0, 1 })[0]);
        UnityEngine.Debug.Log(net.FeedForward(new float[] { 1, 1, 1 })[0]);
        //We want the gate to simulate 3 input or gate (A or B or C)
        // 0 0 0    => 0
        // 1 0 0    => 1
        // 0 1 0    => 1
        // 0 0 1    => 1
        // 1 1 0    => 1
        // 0 1 1    => 1
        // 1 0 1    => 1
        // 1 1 1    => 1
    }

    void NewWay()
    {
        int epochs = 20000;
        this.net = new NeuralNetwork(layers, activation);
        List<float[]> X = new List<float[]>();
        X.Add(new float[] { 0, 0, 0 });
        X.Add(new float[] { 1, 0, 0 });
        X.Add(new float[] { 0, 1, 0 });
        X.Add(new float[] { 0, 0, 1 });
        X.Add(new float[] { 1, 1, 0 });
        X.Add(new float[] { 1, 0, 1 });
        X.Add(new float[] { 1, 1, 1 });

        List<float[]> Y = new List<float[]>();
        Y.Add(new float[] { 0 });
        Y.Add(new float[] { 1 });
        Y.Add(new float[] { 1 });
        Y.Add(new float[] { 1 });
        Y.Add(new float[] { 1 });
        Y.Add(new float[] { 1 });
        Y.Add(new float[] { 1 });
        Y.Add(new float[] { 1 });

        this.net.Fit(X, Y, epochs);

     
        print("cost: " + net.cost);

        UnityEngine.Debug.Log(net.FeedForward(new float[] { 0, 0, 0 })[0] + "***" );
        UnityEngine.Debug.Log(net.FeedForward(new float[] { 1, 0, 0 })[0]);
        UnityEngine.Debug.Log(net.FeedForward(new float[] { 0, 1, 0 })[0]);
        UnityEngine.Debug.Log(net.FeedForward(new float[] { 0, 0, 1 })[0]);
        UnityEngine.Debug.Log(net.FeedForward(new float[] { 1, 1, 0 })[0]);
        UnityEngine.Debug.Log(net.FeedForward(new float[] { 0, 1, 1 })[0]);
        UnityEngine.Debug.Log(net.FeedForward(new float[] { 1, 0, 1 })[0]);
        UnityEngine.Debug.Log(net.FeedForward(new float[] { 1, 1, 1 })[0]);

    }


}
