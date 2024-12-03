using UnityEngine;
using UnityEngine.SceneManagement;  // Para cargar escenas
using System.Collections;

public class JugadorSalud : MonoBehaviour, IDa�o
{
    [SerializeField] public float salud = 100f;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool estaMuerto = false;
    private Color colorOriginal;

    [Header("Componente Barra de Vida")]
    public BarraDeVida barraDeVida;

    [Header("Da�o del Enemigo")]
    public float da�oPorSegundo = 10f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        colorOriginal = spriteRenderer.color;

        if (barraDeVida != null)
        {
            barraDeVida.InicializarBarraDeVida(salud);
        }
    }

    public void TomarDa�o(float da�o, Vector2 direccion)
    {
        if (estaMuerto) return;

        salud -= da�o;
        StartCoroutine(CambiarColorTemporalmente(Color.red));

        if (barraDeVida != null)
        {
            barraDeVida.CambiarVidaActual(salud);
        }

        if (salud <= 0 && !estaMuerto)
        {
            Muerte();
        }
    }

    public void Muerte()
    {
        if (estaMuerto) return;
        estaMuerto = true;
        animator.SetTrigger("Muerte");
        StartCoroutine(EsperarMuerteAnimacion());
    }

    private IEnumerator EsperarMuerteAnimacion()
    {
        AnimatorStateInfo animStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        while (!animStateInfo.IsName("Muerte") || animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
            animStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        }

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(2);  
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !estaMuerto)
        {
            TomarDa�o(da�oPorSegundo * Time.deltaTime, Vector2.zero);  
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            spriteRenderer.color = colorOriginal;  
        }
    }
    private IEnumerator CambiarColorTemporalmente(Color color)
    {
        spriteRenderer.color = color;  
        yield return new WaitForSeconds(0.2f);  
        spriteRenderer.color = colorOriginal;  
    }
}
