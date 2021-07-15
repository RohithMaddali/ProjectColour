using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDespawner : MonoBehaviour
{
    public GameObject tutInfo;
    public int despawnTime;
    void Start()
    {
        StartCoroutine(despawn());
    }

    IEnumerator despawn()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(tutInfo);
    }
}
