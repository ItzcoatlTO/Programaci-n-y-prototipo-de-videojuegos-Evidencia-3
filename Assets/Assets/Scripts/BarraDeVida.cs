using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    [Header("Componentes de la Barra de Vida")]
    public Image barraVidaRelleno;  
    public float vidaMaxima = 100f; 
    public void InicializarBarraDeVida(float vidaActual)
    {
        vidaMaxima = vidaActual; 
        CambiarVidaActual(vidaActual);
    }
    public void CambiarVidaActual(float vidaActual)
    {
        float porcentajeVida = Mathf.Clamp(vidaActual / vidaMaxima, 0f, 1f);
        barraVidaRelleno.fillAmount = porcentajeVida;
    }
}
