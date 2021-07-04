using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    private float currentY, startTime;
    public float speed, max;
    public Material disolveMaterial;
    public Material Green;
    public bool dissolve;
    public int countDown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentY < max) //initiateds the start of the dissolve effect
        {
            disolveMaterial.SetFloat("_DissolveY", currentY);
            currentY += Time.deltaTime * speed;
            dissolve = true;
        }

        if(dissolve == true)
        {
            //StartCoroutine(changeColour());
        }
    }

    IEnumerator changeColour() //changes the end result to green inorder to be used again
    {
        yield return new WaitForSeconds(countDown);
        gameObject.GetComponent<Renderer>().material = Green;
    }
}
