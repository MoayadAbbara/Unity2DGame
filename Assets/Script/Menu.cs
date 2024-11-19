using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void onPlayButton()
    {
        SceneManager.LoadScene("SampleScene");
        Player.health = 3;
        Player.KilledMainEnemy = false;
    }

    public void onQuitButton()
    {
        // Uygulama editor'de çalışıyorsa, bu satırı kullanın
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Uygulama derlenmiş bir versiyon olarak çalışıyorsa,bu satırı kullanın
        Application.Quit(); I
#endif
    }

    public void SelectLevel()
    {
        SceneManager.LoadScene("SelectLevel");
    }
}
