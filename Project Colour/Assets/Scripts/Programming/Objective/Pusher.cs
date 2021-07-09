using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    public float speed;
    public float pauseTime;
    public Transform destination;
    public Transform start;
    public Transform end;
    // Start is called before the first frame update
    void Start()
    {
        start = transform;
        destination = end;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != destination.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination.position, speed * Time.deltaTime);
        }
        else
        {
            StartCoroutine(Pause());
        }
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(pauseTime);
        if (destination == start)
        {
            destination = end;
        }
        else if (destination == end)
        {
            destination = start;
        }
    }
}
