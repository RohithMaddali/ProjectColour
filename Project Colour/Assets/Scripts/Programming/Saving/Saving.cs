﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Quontity;

public class Saving : MonoBehaviour
{
    public bool saving;
    public bool loading;
    KillFloor killFloor;
    
    public float loadTimer = .1f;
    // Update is called once per frame
    void Update()
    {
        if (saving)
        {
            SaveGame();
        }

        if (loading)
        {
            Load();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RBMove>())
        {
            Debug.Log("Collided, save");
            SaveGame();
        }
    }


    public void SaveGame()
    {
        //find killfloor
        killFloor = FindObjectOfType<KillFloor>();
        //set respawn point for killfloor to this poisition
        killFloor.respawnPoint = transform;
        //creates and runs the instance save
        Save save = CreateSaveGameObject();
        
        //Create the relevant files/information needed to create the save file itself
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/gamesave.save");
        binaryFormatter.Serialize(file,save);
        PlayerPrefs.SetInt("Objective01", ObjectiveManager.i.GetObjectiveCurrentNumber(0));
        PlayerPrefs.SetInt("Objective02", ObjectiveManager.i.GetObjectiveCurrentNumber(1));
        PlayerPrefs.SetInt("Objective03", ObjectiveManager.i.GetObjectiveCurrentNumber(2));
        for (int i = 0; i < ObjectiveManager.i.objectives[0].actualObjectName.Count; i++)
        {
            PlayerPrefs.SetString("GreenPrism" + i, ObjectiveManager.i.objectives[0].actualObjectName[i]);
        }
        for (int i = 0; i < ObjectiveManager.i.objectives[1].actualObjectName.Count; i++)
        {
            PlayerPrefs.SetString("BluePrism" + i, ObjectiveManager.i.objectives[1].actualObjectName[i]);
        }
        for (int i = 0; i < ObjectiveManager.i.objectives[2].actualObjectName.Count; i++)
        {
            PlayerPrefs.SetString("RedPrism" + i, ObjectiveManager.i.objectives[2].actualObjectName[i]);
        }
        //closes file so that it can't be edited again during this instance 
        file.Close();
        Debug.Log("Game Saved");
    }
    
    //Function for Saving the game to a instance of the save script
    private Save CreateSaveGameObject()
    {
        //Creates an instance of Save 
        Save save = new Save();
        //Finds a player and temporarily save their reference  
        GameObject player = FindObjectOfType<RBMove>().gameObject;
        //Holds the information that we want to track
        save.testingInt = 3;
        save.xLocation = player.transform.position.x;
        save.yLocation = player.transform.position.y;
        save.zLocation = player.transform.position.z;
        
        return save;

    }

    //Function for loading the game, it refers to the instance of the save file that exists within the assets folder
    public void Load()
    {
        StartCoroutine(loader());
    }
    
    
    IEnumerator loader()
    {
        yield return new WaitForSeconds(loadTimer);
        Debug.Log("Loading");
        if (File.Exists(Application.dataPath + "/gamesave.save"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(Application.dataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save) binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            
            Debug.Log(save.testingInt);
            //Setting a variable to hold the player's transform
            Transform playerLocation = FindObjectOfType<RBMove>().transform;
            if (playerLocation != null)
            {
                //Setting a local vector3 to the saved location
                Vector3 transformPosition = new Vector3 {x = save.xLocation, y = save.yLocation, z = save.zLocation};
                playerLocation.position = transformPosition;
                Debug.Log("Load has completed");
            }
        }
        else
        {
            Debug.Log("No Save File");
        }
    }
    
    
    
   
}
