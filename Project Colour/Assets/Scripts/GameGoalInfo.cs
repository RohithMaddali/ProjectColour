using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameGoalInfo : MonoBehaviour
{
    public Animator anim;
    public Scene thisScene;
    public bool infoGiven = false;
    public int delayTimer;

    void Start()
    {

    }

    void Update()
    {
        thisScene = SceneManager.GetActiveScene();
        Debug.Log("this scene is " + thisScene.buildIndex);
        if(thisScene.buildIndex == 5 && infoGiven == false)
        {
            infoGiven = true;
            Debug.Log("Spawning info");
            StartCoroutine(info());
        }
        if(thisScene.buildIndex != 5)
        {
            infoGiven = false;
        }
    }
    IEnumerator info()
    {
        yield return new WaitForSeconds(delayTimer);
        anim.SetBool("infoSpawn", true);
    }
}
