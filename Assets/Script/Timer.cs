using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text TimeText;
    public float RemainTime = 90; //kalan süreyi unity saniye olarak ayarlanir
    void Update()
    {
        //zaman azalmasını sağlar
        RemainTime -= Time.deltaTime;
        int min = Mathf.FloorToInt(RemainTime / 60);
        int sec = Mathf.FloorToInt(RemainTime % 60);
        //Dakika:Saniye şekilinde formatına getirmek
        TimeText.text = string.Format("{0:00}:{1:00}", min, sec);
        if (RemainTime <= 0)
        {
            SceneManager.LoadScene("LoseScene");
        }
    }
}
