using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Egg : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Yumurta yere değdiğinde yok olur
        if (other.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
