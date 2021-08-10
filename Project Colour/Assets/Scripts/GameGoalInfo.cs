using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameGoalInfo : MonoBehaviour
{
    public Animator anim;
    public Scene thisScene;
    public bool infoGiven = false;
    public int delayTimer;
    W_MainMenuMusic mainMenuMusic;
    public GameObject First;

    void Start()
    {
        mainMenuMusic = GameObject.Find("music").GetComponent<W_MainMenuMusic>();
        StartCoroutine(info());
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(First);
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
