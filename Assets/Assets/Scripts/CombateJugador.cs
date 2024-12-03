using UnityEngine;

public class CombateJugador : MonoBehaviour
{
    [Header("Referencias")]
    public BarraDeVida barraDeVida;  
    public float vidaMaxima = 100f;  
    private float vidaActual;        
    private bool estaMuerto = false; 

    private void Start()
    {
        if (barraDeVida != null)
        {
            vidaActual = vidaMaxima; 
            barraDeVida.InicializarBarraDeVida(vidaMaxima); 
        }
    }
    public void TomarDa�o(float da�o)
    {
        if (estaMuerto) return;
        vidaActual -= da�o; 
        if (barraDeVida != null)
        {
            barraDeVida.CambiarVidaActual(vidaActual); 
        }
        if (vidaActual <= 0 && !estaMuerto)
        {
            Muerte();
        }
    }
    private void Muerte()
    {
        estaMuerto = true;
        Debug.Log("�El jugador ha muerto!");
    }
}
