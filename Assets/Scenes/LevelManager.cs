using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public AudioSource audioSource;  
    public AudioClip startSound;     

    public void BotonStart()
    {
        
        if (audioSource != null && startSound != null)
        {
            audioSource.PlayOneShot(startSound);
        }
        StartCoroutine(CargarEscenaConSonido());
    }

    private IEnumerator CargarEscenaConSonido()
    {
        
        yield return new WaitForSeconds(startSound.length);
        SceneManager.LoadScene(1);
    }
}
