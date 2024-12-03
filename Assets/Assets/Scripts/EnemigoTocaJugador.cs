using System.Collections;
using UnityEngine;

public class EnemigoTocaJugador : MonoBehaviour
{
    [SerializeField] private GameObject jugador;            
    [SerializeField] private float daño = 20f;              

    [Header("Componente Barra de Vida")]
    public BarraDeVida barraDeVida; 

    private void Start()
    {
        if (jugador == null) jugador = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            JugadorSalud jugadorSalud = jugador.GetComponent<JugadorSalud>();
            if (jugadorSalud != null)
            {
                jugadorSalud.TomarDaño(daño * Time.deltaTime, Vector2.zero);  
            }
            SpriteRenderer spriteRenderer = jugador.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                StartCoroutine(CambiarColorRojo(spriteRenderer));
            }
            if (barraDeVida != null && jugadorSalud != null)
            {
                barraDeVida.CambiarVidaActual(jugadorSalud.salud);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SpriteRenderer spriteRenderer = jugador.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                StartCoroutine(RestaurarColorOriginal(spriteRenderer));
            }
        }
    }
    private IEnumerator CambiarColorRojo(SpriteRenderer spriteRenderer)
    {
        Color colorOriginal = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = colorOriginal;
    }
    private IEnumerator RestaurarColorOriginal(SpriteRenderer spriteRenderer)
    {
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
}
