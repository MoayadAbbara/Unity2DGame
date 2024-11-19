using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFoot : MonoBehaviour
{
    public GameObject ExplosionPref;
    void OnCollisionEnter2D(Collision2D other)
    {
        //Playerin ayaklari Kuşla temas ettiği zaman kuş yok olur ve patlama effecti oluşur.
        if (other.gameObject.name == "Bat")
        {
            Instantiate(ExplosionPref, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }
}
