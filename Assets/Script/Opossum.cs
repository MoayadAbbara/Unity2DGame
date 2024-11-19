using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opossum : MonoBehaviour
{
    public Rigidbody2D rb;
    public int speed;
    public int health;
    public GameObject ExplosionPref;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = -4;
        health = 1;
    }

    // Update is called once per frame
    void Update()
    {
        BasicMoves();
        IsDied();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //Köpek Ateş topu ile vurulduğu zaman hasar alır.
        if (other.gameObject.tag == "FireBall")
        {
            health--;
            Destroy(other.gameObject);
            Instantiate(ExplosionPref, transform.position, Quaternion.identity);
        }
    }

    void BasicMoves()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    void IsDied()
    {
        if (health <= 0)
            Destroy(gameObject);
    }
}
