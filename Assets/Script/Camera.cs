using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target; // Kameranın takip edeceği hedef (karakter)
    public float smoothSpeed = 0.125f; // Kamera takibinin yumuşaklığını ayarlayan değişken
    public Vector3 offset = new Vector3(0f, 0f, -10f); // Kamera ve karakter arasındaki uzaklık ayarı

    void LateUpdate()
    {
        if (target != null)
        {
            // Hedefin yeni pozisyonunu hesapla
            Vector3 desiredPosition = target.position + offset;
            // Kameranın yeni pozisyonunu yumuşak bir şekilde güncelle
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}