using System.Collections;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 12f;
    public bool isRight = true;
    public float ThrowTime;

    public bool movingToA = true;
    public GameObject EggPref;

    void Start()
    {
        StartCoroutine(ThrowEgg());
    }

    void Update()
    {
        //rastgale süre aralığında kuşların yumurta atması
        ThrowTime = Random.Range(1f, 2f);
        //Bitiş noktasına mı başlangıc noktasına mı doğru gitmek kontrolu 
        if (movingToA)
        {
            MoveTowards(pointA);
        }
        else
        {
            MoveTowards(pointB);
        }
    }
    //Hareket ettirme Fonkisyonu
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

    //Yumurta Düşürme Fonkisyonu
    IEnumerator ThrowEgg()
    {
        while (true)
        {
            yield return new WaitForSeconds(ThrowTime);
            Instantiate(EggPref, transform.position, Quaternion.identity);
        }
    }
}
