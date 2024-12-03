using UnityEngine;

public class ReproducirSonido : MonoBehaviour
{
    [Header("Configuración de Audio")]
    public AudioClip sonidoDeEvento;  
    private AudioSource audioSource;
    private bool sonidoReproducido = false; 

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        if (!sonidoReproducido)
        {
            ReproducirSonidoDeEvento();
        }
    }

    private void ReproducirSonidoDeEvento()
    {
        if (sonidoDeEvento != null && audioSource != null)
        {
            audioSource.PlayOneShot(sonidoDeEvento);
            sonidoReproducido = true;
        }
    }
}
