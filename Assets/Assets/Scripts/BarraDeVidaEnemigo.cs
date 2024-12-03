using UnityEngine;
using UnityEngine.UI;

public class BarraDeVidaEnemigo : MonoBehaviour
{
    [Header("Referencia UI")]
    public Slider barraVida;      
    public Gradient gradiente;    
    public Image relleno;         

    [Header("Vida")]
    public float vidaMaxima = 100f;
    private float vidaActual;

    private void Start()
    {
        vidaActual = vidaMaxima;  
        barraVida.maxValue = vidaMaxima;  
        barraVida.value = vidaActual;     
    }

    private void Update()
    {
        relleno.color = gradiente.Evaluate(barraVida.normalizedValue);
    }
    public void CambiarVida(float nuevaVida)
    {
        vidaActual = nuevaVida; 
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);
        barraVida.value = vidaActual;

        if (vidaActual <= 0)
        {
            MuerteEnemigo();
        }
    }

    private void MuerteEnemigo()
    {
        Destroy(gameObject);  
    }
}
