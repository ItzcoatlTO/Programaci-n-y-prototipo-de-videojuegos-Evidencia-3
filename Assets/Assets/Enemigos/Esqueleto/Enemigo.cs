using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemigo : MonoBehaviour, IDaño
{
    [SerializeField] private float velocidad;          // Velocidad de movimiento
    [SerializeField] private Transform puntoA;         // Punto A (inicio del patrullaje)
    [SerializeField] private Transform puntoB;         // Punto B (final del patrullaje)
    private bool moviendoDerecha = true;               // Si está moviéndose hacia la derecha
    private Rigidbody2D rb;                            // Rigidbody2D para el movimiento
    private float vida = 100f;                         // Vida del enemigo
    private Animator animator;                         // Referencia al componente Animator

    // Agregar una referencia al jugador
    [SerializeField] private GameObject jugador;

    private bool estaMuerto = false;                   // Estado del enemigo (vivo o muerto)
    [SerializeField] private float tiempoEsperaRevivir = 3f;  // Tiempo de espera antes de revivir

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();               // Obtener el componente Rigidbody2D
        animator = GetComponent<Animator>();            // Obtener el componente Animator
        transform.eulerAngles = new Vector3(0, 0, 0);   // Iniciar mirando hacia la derecha
    }

    private void FixedUpdate()
    {
        // Si el enemigo no está muerto y tiene vida, se mueve
        if (vida > 0 && !estaMuerto)
        {
            if (moviendoDerecha)
            {
                rb.linearVelocity = new Vector2(velocidad, rb.linearVelocity.y);  // Movimiento hacia la derecha
            }
            else
            {
                rb.linearVelocity = new Vector2(-velocidad, rb.linearVelocity.y); // Movimiento hacia la izquierda
            }

            // Comprobar si el enemigo ha llegado al punto A o B
            if (transform.position.x >= puntoB.position.x)
            {
                moviendoDerecha = false;
                Girar();
            }
            else if (transform.position.x <= puntoA.position.x)
            {
                moviendoDerecha = true;
                Girar();
            }
        }
    }

    private void Girar()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }

    // Función para aplicar daño y controlar la muerte
    public void TomarDaño(float daño, Vector2 direccion)
    {
        // El enemigo recibe daño y lo resta de su vida
        vida -= daño;

        if (vida <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        estaMuerto = true;  // Marcar al enemigo como muerto
        animator.SetTrigger("Muerte");  // Activar animación de muerte
        rb.linearVelocity = Vector2.zero;    // Detener el movimiento del enemigo

        // Deshabilitar el movimiento y esperar a que reviva
        StartCoroutine(EsperarParaRevivir());
    }

    private IEnumerator EsperarParaRevivir()
    {
        // Esperar el tiempo de espera antes de revivir
        yield return new WaitForSeconds(tiempoEsperaRevivir);

        Revivir();  // Llamar a la función de revivir
    }

    private void Revivir()
    {
        animator.SetTrigger("Revivir");  // Activar animación de revivir

        // Esperar el tiempo de la animación de revivir
        StartCoroutine(EsperarFinAnimacionRevivir());
    }

    private IEnumerator EsperarFinAnimacionRevivir()
    {
        // Esperar a que termine la animación de revivir
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Cuando termine la animación de revivir, el enemigo comienza a caminar inmediatamente
        animator.SetTrigger("Caminar");

        // Restaurar vida y marcar como vivo
        vida = 100f;
        estaMuerto = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(puntoA.position, puntoB.position);  // Dibujar línea entre los puntos A y B
        Gizmos.DrawSphere(puntoA.position, 0.2f);           // Mostrar punto A
        Gizmos.DrawSphere(puntoB.position, 0.2f);           // Mostrar punto B
    }
}
