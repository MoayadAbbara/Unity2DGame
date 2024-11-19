using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacking : MonoBehaviour
{
    //Bu script Ana Düşmanın childi olan ve sağ tarafında bulunan boş objeye atılmıştır.
    void OnCollisionEnter2D(Collision2D other)
    {
        //Player Düşmanın sağ tarafına çarptığında 
        if (other.gameObject.tag == "Player")
        {
            //Duşmanin Attack animasyonuna girer.
            transform.parent.GetComponent<Animator>().SetTrigger("IsAttacking");
            //Düşman sağa doğru giderken Player'e çarparsa Playeri sağa doğru iter sola giderken ise sola doğru iter.
            if (Enemy.movingToA)
            {
                other.gameObject.transform.position = other.gameObject.transform.position + new Vector3(5, 0, 0);
            }
            else
            {
                other.gameObject.transform.position = other.gameObject.transform.position + new Vector3(-5, 0, 0);
            }

            //Ses efecti ve Animasyonlar
            other.gameObject.GetComponent<AudioSource>().Play();
            Player.health--;
            other.gameObject.GetComponent<Animator>().SetTrigger("Hurt");
        }
    }
}
