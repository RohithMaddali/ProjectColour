using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ObjectiveDisplayPanel : MonoBehaviour
{
    public float timeToFadeOut = 3f;
    public float fadeOutDuration = 1f;

    private void OnEnable()
    {
        GetComponent<Image>().canvasRenderer.SetAlpha(1);
        GetComponentInChildren<Text>().canvasRenderer.SetAlpha(1);
        
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut() {
        yield return new WaitForSeconds(timeToFadeOut);
        GetComponent<Image>().CrossFadeAlpha(0, fadeOutDuration, false);
        GetComponentInChildren<Text>().CrossFadeAlpha(0, fadeOutDuration, false);
        yield return new WaitForSeconds(fadeOutDuration);
        gameObject.SetActive(false);
    }
}
