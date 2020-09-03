﻿using MLGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MLGames.NeuralNetwork;
using static MLGames.NNSettings;

public class Manager : MonoBehaviour
{

    public float timeframe;
    public int populationSize;//creates population size
    public GameObject prefab;//holds bot prefab

    public int[] layers = new int[3] { 5, 3, 2 };//initializing network to the right size
    public ActivationFunctions[] activation = new ActivationFunctions[2] { ActivationFunctions.leakyrelu, ActivationFunctions.leakyrelu };

    [Range(0.0001f, 1f)] public float MutationChance = 0.01f;

    [Range(0f, 1f)] public float MutationStrength = 0.5f;

    [Range(0.1f, 10f)] public float Gamespeed = 1f;

    //public List<Bot> Bots;
    public List<NeuralNetwork> networks;
    private List<Bot> cars;
    

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        Debug.Log("entered start");
        if (populationSize % 2 != 0)
            populationSize = 50;//if population size is not even, sets it to fifty

        InitNetworks();
        InvokeRepeating("CreateBots", 0.1f, timeframe);//repeating function
    }

    /// <summary>
    /// 
    /// </summary>
    public void InitNetworks()
    {
        string strErrMsg = string.Empty;
        networks = new List<NeuralNetwork>();

        for (int i = 0; i < populationSize; i++)
        {
            NeuralNetwork net = new NeuralNetwork(layers, activation);
            net.SendMessage += this.NetworkMessage;
            //net.Load_old("Assets/Save.txt");//on start load the network save
            //net.Save("Assets/Save.xml", ref strErrMsg);
            net.Load("Assets/Save.xml", ref strErrMsg);
            Debug.Log(strErrMsg);
            networks.Add(net);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void CreateBots()
    {
        Time.timeScale = Gamespeed;//sets gamespeed, which will increase to speed up training
        if (cars != null)
        {
            for (int i = 0; i < cars.Count; i++)
            {
                GameObject.Destroy(cars[i].gameObject);//if there are Prefabs in the scene this will get rid of them
            }

            SortNetworks();//this sorts networks and mutates them
        }

        cars = new List<Bot>();
        for (int i = 0; i < populationSize; i++)
        {
            //create botes
            Bot car = (Instantiate(prefab, new Vector3(0, 1.6f, -16), new Quaternion(0, 0, 1, 0))).GetComponent<Bot>();
            car.network = networks[i];//deploys network to each learner
            cars.Add(car);
        }
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="messageType"></param>
    /// <param name="strMsg"></param>
    private void NetworkMessage(MessageTypes messageType, string strMsg)
    {
        Debug.Log(strMsg);
    }

    /// <summary>
    /// 
    /// </summary>
    public void SortNetworks()
    {
        string strErrMsg = string.Empty;
        for (int i = 0; i < populationSize; i++)
        {
            cars[i].UpdateFitness();//gets bots to set their corrosponding networks fitness
        }
        networks.Sort();
        networks[populationSize - 1].Save("Assets/Save.txt", ref strErrMsg);//saves networks weights and biases to file, to preserve network performance
        for (int i = 0; i < populationSize / 2; i++)
        {
            NeuralNetwork nn = new NeuralNetwork(layers, activation);
            nn.SendMessage = this.NetworkMessage;
            networks[i] = networks[i + populationSize / 2].copy(nn);
            networks[i].Mutate((int)(1/MutationChance), MutationStrength);
        }
    }
}