using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpossumDamage : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        //Player Kopeğin sol tarafına temas ettiği zaman hasar alır ve hasar alma animsayonu girer ve arakaya doğru iter
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Animator>().SetTrigger("Hurt");
            Player.health--;
            other.gameObject.transform.position = other.gameObject.transform.position + new Vector3(-2, 0, 0);
            other.gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
