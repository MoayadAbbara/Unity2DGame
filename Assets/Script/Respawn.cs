using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject opossumPref;
    public GameObject ExplosionPref;
    public GameObject DamagePref;
    public GameObject KeyPref;
    public int Damage = 10;
    public AudioSource audioS;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(respawn());
    }

    // Update is called once per frame
    void Update()
    {
        isDestoyred();
    }

    IEnumerator respawn()
    {
        //5 Saniye aralıklarla Respawndan Köpek çıkarmak.
        while (true)
        {
            yield return new WaitForSeconds(5f);
            Instantiate(opossumPref, transform.position, Quaternion.identity);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Respawn'a ateş topu ile temas ettiğimde hasar alması , patlatma effecti  ve ses efecti çalıştırılması 
        if (other.gameObject.tag == "FireBall")
        {
            Instantiate(DamagePref, other.gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Damage--;
            audioS.Play();
        }

    }

    void isDestoyred()
    {
        //Respawn 10 kez hasar alınca yok yok olması büyük bir patlatma effecti oluşturması ve ortaya bir anahtar çıkarması;
        if (Damage <= 0)
        {
            Instantiate(ExplosionPref, transform.position, Quaternion.identity);
            Instantiate(KeyPref, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
