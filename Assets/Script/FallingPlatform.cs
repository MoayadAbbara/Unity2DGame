using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private float fallDelay = 0.2f;
    private float destroyDelay = 2f;

    public AudioSource audioS;

    [SerializeField] private Rigidbody2D rb;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Player platformu temas ettiği zaman düşürme Fonkisyonu başlatır 
            audioS.Play();
            StartCoroutine(Fall());
        }
    }

    //Platform Düşmesi
    private IEnumerator Fall()
    {
        //Player Platformun üzerinde belirli bir süre sonra RigidBody Türü değişir ve Dynamic olarak ayarlanir 
        //Bi önceki halde düşmemesi için  statik olarak ayarlandi 
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        //belirli bir süre sonra yok olur
        Destroy(gameObject, destroyDelay);
    }
}