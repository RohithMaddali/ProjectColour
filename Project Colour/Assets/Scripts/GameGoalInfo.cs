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
        StartCoroutine(info());
    }

    void Update()
    {

    }
    IEnumerator info()
    {
        yield return new WaitForSeconds(delayTimer);
        anim.SetBool("infoSpawn", true);
        yield return new WaitForSeconds(18f);
        loadScene();
    }

    public void loadScene()
    {
        SceneManager.LoadScene("AdditiveSceneTest", LoadSceneMode.Single);
    }
}
