using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class endGame : MonoBehaviour
{
    public GameManager gm;

    public bool EndGame = false;
    public GameObject First;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(First);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (EndGame)
        {
            Debug.Log("End game interaction possible");
            //bring the colour back to the world!!
            //coroutine and then display text Thank you for playing this demo
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && gm.itemsPicked == 6)
        {
            EndGame = true;
            Debug.Log("End game interaction possible");
            gm.Endgame();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && gm.itemsPicked == 6)
        {
            EndGame = false;
        }
    }
}
