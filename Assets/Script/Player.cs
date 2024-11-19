using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Animator Anim;
    float Horizontal;
    public int jumpPower;
    public bool isGround;
    public int speed = 10;
    public bool isRight;
    static public int health = 3;
    static public bool KilledMainEnemy = false;
    static public bool isCrouching;
    public GameObject[] HealthBar;
    public AudioSource Audios;
    public AudioSource AudioKey;
    public AudioSource AudioChest;
    private float vertical;
    private float speedV = 8f;
    private bool isLeader;
    private bool isClimbing;
    public GameObject FBPrefab;
    public GameObject KeyIcon;
    public bool haveKey;
    public GameObject[] Potion;

    // Oyuncunun başlangıç ayarları
    void Start()
    {
        jumpPower = 8;
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        isGround = false;
        haveKey = false;
    }

    // Oyuncunun her frame'de kontrol edilmesi gereken hareketleri
    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        //Player eğildiği zaman hareket edemez.
        if (!isCrouching)
            BasicMoves(Horizontal);
        changeDirection(Horizontal);
        HealthBarFun();
        if (Input.GetKeyDown(KeyCode.L))
            ThrowFireBall();
        //Player yerde ve S ye bastığı zaman eğiliyor.
        if (Input.GetKey(KeyCode.S) && isGround)
        {
            isCrouching = true;
            Anim.SetBool("IsCrouching", isCrouching);
        }
        else
        {
            isCrouching = false;
            Anim.SetBool("IsCrouching", isCrouching);
        }

        if (isLeader && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;

        }

    }

    void FixedUpdate()
    {
        //Merdivan Tırmanma işlemleri
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * speedV);
            Anim.SetBool("Climbing", isClimbing);
        }
        else
        {
            Anim.SetBool("Climbing", isClimbing);
            rb.gravityScale = 1f;
        }

    }

    // Oyuncunun yere değdiği zaman çalışan fonksiyon
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Oyuncu Kuşların düşürdüğü yumurta veya ana düşmanın attığı bıçakla temas ettiği zaman hasar alması 
        //Eğildiği halde hasar almaz.
        if (((other.gameObject.tag == "Egg") || (other.gameObject.tag == "Axe")) && !isCrouching)
        {
            Anim.SetTrigger("Hurt");
            Audios.Play();
            Destroy(other.gameObject);
            health--;
        }
        if (other.gameObject.tag == "Ladder")
        {
            isLeader = true;
        }
        if (other.gameObject.tag == "Key")
        {
            haveKey = true;
            KeyIcon.SetActive(true);
            Destroy(other.gameObject);
            AudioKey.Play();
        }
        //Sandıktan Çıkan İksirlerin türüne göre hız arttırması veya azaltması.
        if (other.gameObject.name == "SlowPot(Clone)")
        {
            Destroy(other.gameObject);
            StartCoroutine(CahngeSpeedForDuration(0));
        }
        if (other.gameObject.name == "SpeedPot(Clone)")
        {
            Destroy(other.gameObject);
            StartCoroutine(CahngeSpeedForDuration(1));
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        //Player Anahtarı aldığı halde sandık açabilir ve sandıktan yararlı veya zararlı bir İksir çıkabilir rastgele bi şekilde
        //Sandık açma Animasyonu da girdi 
        if (other.gameObject.name == "Chest")
        {
            if (haveKey && Input.GetKeyDown(KeyCode.J))
            {
                other.gameObject.GetComponent<Animator>().SetTrigger("Open");
                other.gameObject.GetComponent<AudioSource>().Play();
                KeyIcon.SetActive(false);
                haveKey = false;
                ThrowPotion(Random.Range(0, 2));
            }
        }
    }

    // Tetikleyiciden çıktığı zaman çalışan fonksiyon
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            isLeader = false;
            isClimbing = false;
            Debug.Log("Exit ladder");
        }
    }

    // Temel hareketlerin kontrol edildiği fonksiyon
    void BasicMoves(float Horizontal)
    {
        rb.velocity = new Vector2(Horizontal * speed, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            isGround = false;
        }
        Anim.SetFloat("Speed", Mathf.Abs(Horizontal));
        Anim.SetBool("isJumping", !isGround);
    }

    void changeDirection(float Horizontal)
    {
        if (isRight && Horizontal < 0 || !isRight && Horizontal > 0)
        {
            isRight = !isRight;
            Vector3 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;
        }
    }


    void ThrowFireBall()
    {
        // Ateş topunun atıldığı fonksiyon 
        //Oyuncu sağa doğru giderken top sağa doğru sola doğru giderken ise sola doğru gider.
        GameObject fireball = Instantiate(FBPrefab, transform.position, Quaternion.identity);
        Rigidbody2D FBrb = fireball.GetComponent<Rigidbody2D>();
        if (isRight)
        {
            FBrb.velocity = new Vector2(8, 0);
        }
        else
        {
            FBrb.velocity = new Vector2(-8, 0);
            Vector3 temp = fireball.transform.localScale;
            temp.x *= -1;
            fireball.transform.localScale = temp;
        }
    }


    void HealthBarFun()
    {
        // Can barının güncellendiği fonksiyon
        if (health == 2)
        {
            HealthBar[0].SetActive(false);
        }
        if (health == 1)
        {
            HealthBar[1].SetActive(false);
        }
        if (health <= 0)
        {
            SceneManager.LoadScene("LoseScene");
        }
    }


    void ThrowPotion(int index)
    {
        // İksir atıldığı zaman çalışan fonksiyon ve parametre olarak gelen rastgele sayıya göre yaralı yada zaralı nesne olmasını
        //belirtilmesi
        if (index == 0)
        {
            Instantiate(Potion[0], new Vector3(15.23f, 23.13f, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(Potion[1], new Vector3(15.23f, 23.13f, 0), Quaternion.identity);
        }
    }

    IEnumerator CahngeSpeedForDuration(int x)
    {
        //Yararlı nesne çıktığı halde hızı 10 saniye boyunca artması
        if (x == 1)
        {
            speed = 15;

            yield return new WaitForSeconds(10f);

            speed = 10;
        }
        //Zararlı nesne çıktığı halde hızı 10 saniye boyunca azalması
        else
        {
            speed = 5;

            yield return new WaitForSeconds(10f);

            speed = 10;
        }
    }
}
