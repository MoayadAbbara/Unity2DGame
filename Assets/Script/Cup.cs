using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cup : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
            SceneManager.LoadScene("WinScene");
    }

    // Update is called once per frame
    void Update()
    {
        //Ana düşman öldüğünde Kupa hareket etmeye başlar.
        if (Player.KilledMainEnemy)
        {
            anim.SetBool("KilledEnemy", true);
        }
    }
}
