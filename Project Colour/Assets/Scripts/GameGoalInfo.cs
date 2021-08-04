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
    W_MainMenuMusic mainMenuMusic;

    void Start()
    {
        mainMenuMusic = GameObject.Find("music").GetComponent<W_MainMenuMusic>();
        StartCoroutine(info());
    }
    IEnumerator info()
    {
        yield return new WaitForSeconds(delayTimer);
        mainMenuMusic.StartCoroutine(mainMenuMusic.FadeMusic());
        anim.SetBool("infoSpawn", true);
        yield return new WaitForSeconds(18f);
        loadScene();
    }
    public void loadScene()
    {
        SceneManager.LoadScene("AdditiveSceneTest", LoadSceneMode.Single);
        mainMenuMusic.StopMusic();
    }
    public void StopMusic()
    {
        mainMenuMusic.StopMusic();
    }
}
