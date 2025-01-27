﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class SpawMobScript : MonoBehaviour {
    float timer = 0;
    public List<GameObject> ListMob = new List<GameObject>();
    public List<Texture2D> ListImg = new List<Texture2D>();
    public float frequence;
    public Texture2D prevTex, newTex;
    public Texture2D defaultText;
    public GameObject debugTxt;
    bool start = false;


    void Awake()
    {
        UnityMessageManager.Instance.OnRNMessage += onMessage;
    }

    void onDestroy()
    {
        UnityMessageManager.Instance.OnRNMessage -= onMessage;
    }


    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {

      //  UnityMessageManager.Instance.SendMessageToRN("Tes qu'une pute");
        if (timer >= frequence)
        {
            timer = 0;
            frequence =  UnityEngine.Random.Range(0, 6);

            if(ListImg.Count <= 0)
            ListMob[UnityEngine.Random.Range(0, ListMob.Count-1)].GetComponent<targetScript>().Activate(defaultText);
            else
            ListMob[UnityEngine.Random.Range(0, ListMob.Count)].GetComponent<targetScript>().Activate(ListImg[UnityEngine.Random.Range(0, ListImg.Count-1)]);

        }
        else
        {
            if (start)
            {
                timer += Time.deltaTime; 
            }
        }
    }

     void onMessage(MessageHandler message)
      {
          var data = message.getData<string>();
          Debug.Log("onMessage:" + data);

        start = true;

        if (data.Length > 25)
            printImage(data);
        else
            Camera.main.GetComponent<cameraScript>().getGyro(data);
    }

    void printImage(String iconBase64String)
    {
        Texture2D newPhoto = new Texture2D(1, 1);
        newPhoto.LoadImage(Convert.FromBase64String(iconBase64String));
        newPhoto.Apply();
        ListImg.Add(newPhoto);
    }
}
