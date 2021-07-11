using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Saving : MonoBehaviour
{
    public bool saving;

    public bool loading;
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

    public void SaveGame()
    {
        //creates and runs the instance save
        Save save = CreateSaveGameObject();
        
        //Create the relevant files/information needed to create the save file itself
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/gamesave.save");
        binaryFormatter.Serialize(file,save);
        //closes file so that it can't be edited again during this instance 
        file.Close();
        
        //Reset the player position TODO remove this as it is just here for testing purposes
        FindObjectOfType<RBMove>().transform.position = new Vector3(0, 0, 0);
        Debug.Log("Game Saved");
    }
    
    //Function for Saving the game to a instance of the save script
    private Save CreateSaveGameObject()
    {
        //Creates an instance of Save 
        Save save = new Save();
        //Holds the information that we want to track
        save.testingInt = 3;
        
        //save.playerLocation = FindObjectOfType<RBMove>().transform.position;
        return save;
    }
    
    //Function for loading the game, it refers to the instance of the save file that exists within the assets folder
    public void Load()
    {
        if (File.Exists(Application.dataPath + "/gamesave.save"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(Application.dataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save) binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            
            Debug.Log(save.testingInt);
            
        }
        else
        {
            Debug.Log("No Save File");
        }
    }
}
