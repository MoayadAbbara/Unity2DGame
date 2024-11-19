using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Saw : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 5f;

    private Vector3 currentTarget;

    void Start()
    {
        currentTarget = startPoint.position;
    }

    void Update()
    {
        //Bıçağın hareket ettirmek.
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

        // Bıçağın başalngıç ve bitiş noktasına geldiği zaman yönünü terslemek.
        if (transform.position == currentTarget)
        {
            if (currentTarget == startPoint.position)
            {
                currentTarget = endPoint.position;
            }
            else
            {
                currentTarget = startPoint.position;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Player Bıçağa Temas ederse kaybeder ve oyun biter.
            SceneManager.LoadScene("LoseScene");
        }
    }
}
