using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //Ana Düşman Scripti
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 12f;
    public bool isRight;
    public float ThrowTime;
    public float health;
    public Image HealthBar;


    public GameObject knifePrefab;
    public Transform throwPoint;
    public float throwInterval = 3f;

    public Transform player;


    static public bool movingToA = true;

    void Start()
    {
        health = 100f;
        isRight = true;
        StartCoroutine(ThrowKnifeRoutine());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Düşman Ateş topuna temas ettiği zaman canı azalır ve healthBar'da gösterilir hızı düşer  
        if (other.gameObject.tag == "FireBall")
        {
            Destroy(other.gameObject);
            health -= 10f;
            HealthBar.fillAmount = health / 100f;
            moveSpeed -= 1;
        }
    }

    void Update()
    {
        if (movingToA)
        {
            MoveTowards(pointA);
        }
        else
        {
            MoveTowards(pointB);
        }
        if (health <= 0)
        {
            Player.KilledMainEnemy = true;
            Destroy(gameObject);
        }
    }

    //Hareket ettirme fonkisyonu
    void MoveTowards(Transform target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        //Player gittiği noktaya geldiği zaman diğer noktaya doğru yön değiştirmesi
        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            movingToA = !movingToA;
            changeDirection();
        }
    }

    void changeDirection()
    {
        isRight = !isRight;
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;
    }

    //Bıçak Fırlatma Fonkisyonu
    IEnumerator ThrowKnifeRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(throwInterval);
            //Player Ve düşman arasındaki mesafe 30 küçük olduğu durumlarda bıçak fıratılır yani birbirlerini gördüklerinde
            if (movingToA && Mathf.Abs(player.position.x - transform.position.x) < 30)
            {
                //Fırlatma yönü
                Vector2 direction = (player.position - throwPoint.position).normalized;

                GameObject knife = Instantiate(knifePrefab, throwPoint.position, Quaternion.identity);

                Rigidbody2D knifeRb = knife.GetComponent<Rigidbody2D>();
                knifeRb.AddForce(direction * 750f);
                //5 Saniye sonra bıçağın yok olması
                Destroy(knife, 5f);
            }
        }
    }
}
