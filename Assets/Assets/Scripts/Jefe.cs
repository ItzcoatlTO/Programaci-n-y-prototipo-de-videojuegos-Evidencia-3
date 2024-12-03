using UnityEngine;
using UnityEngine.SceneManagement;  
using System.Collections;
using UnityEngine;

public class Jefe : MonoBehaviour, IDaño
{
    private Animator animator;
    public Transform jugador;
    private bool mirandoDerecha = true;

    [Header("Vida")]
    [SerializeField] private float vida;
    public BarraDeVidaEnemigo barraDeVida;

    [Header("Ataque")]
    [SerializeField] private Transform controladorAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private float dañoAtaque;

    [Header("Movimiento")]
    [SerializeField] private float velocidadMovimiento = 3f;

    private SpriteRenderer spriteRenderer;

    [Header("Color Rojo")]
    [SerializeField] private float tiempoColorRojo = 0.1f;
    private bool recibiendoDaño = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (barraDeVida != null)
        {
            barraDeVida.CambiarVida(vida);
        }
    }

    private void Update()
    {
        float distanciaJugador = Vector2.Distance(transform.position, jugador.position);
        animator.SetFloat("distanciaJugador", distanciaJugador);
        MirarJugador();
        SeguirJugador();
    }

    public void TomarDaño(float daño)
    {
        vida -= daño;

        if (vida <= 0)
        {
            animator.SetTrigger("Muerte");
        }
    }

    private void Muerte()
    {
        Destroy(gameObject);
    }

    public void TomarDaño(float daño, Vector2 direccion)
    {
        if (!recibiendoDaño)
        {
            recibiendoDaño = true;
            vida -= daño;

            if (barraDeVida != null)
            {
                barraDeVida.CambiarVida(vida);
            }

            if (spriteRenderer != null)
            {
                StartCoroutine(CambiarColorRojo(spriteRenderer));
            }

            if (vida <= 0)
            {
                animator.SetTrigger("Muerte");
                StartCoroutine(CambiarEscena(3));
            }
        }
    }

    public void Ataque()
    {
        Collider2D[] enemigos = Physics2D.OverlapCircleAll(controladorAtaque.position, radioAtaque);
        foreach (var enemigo in enemigos)
        {
            if (enemigo.CompareTag("Player"))
            {
                enemigo.GetComponent<IDaño>()?.TomarDaño(dañoAtaque, Vector2.zero);
            }
        }
    }

    private IEnumerator CambiarEscena(int escenaID)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(escenaID);
    }

    private IEnumerator CambiarColorRojo(SpriteRenderer spriteRenderer)
    {
        Color colorOriginal = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(tiempoColorRojo);
        spriteRenderer.color = colorOriginal;
        recibiendoDaño = false;
    }

    public void SeguirJugador()
    {
        Vector2 direccion = (jugador.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, jugador.position, velocidadMovimiento * Time.deltaTime);
    }
    public void MirarJugador()
    {
        if (jugador.position.x < transform.position.x && mirandoDerecha)
        {
            Flip();
        }
        else if (jugador.position.x > transform.position.x && !mirandoDerecha)
        {
            Flip();
        }
    }
    private void Flip()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1; 
        transform.localScale = escala;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorAtaque.position, radioAtaque);
    }
}
