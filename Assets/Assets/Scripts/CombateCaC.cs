using System.Collections;
using UnityEngine;

public class CombateCaC : MonoBehaviour
{
    [SerializeField] private Transform controladorGolpe; 
    [SerializeField] private float radioGolpe;          
    [SerializeField] private float dañoGolpe;          
    [SerializeField] private float tiempoEntreAtaques;  
    private float tiempoSiguienteAtaque;                
    private Animator animator;                          

    private void Start()
    {
        animator = GetComponent<Animator>();  
    }

    private void Update()
    {
        
        if (tiempoSiguienteAtaque > 0)
        {
            tiempoSiguienteAtaque -= Time.deltaTime;
        }

        
        if (Input.GetButtonDown("Fire1") && tiempoSiguienteAtaque <= 0)
        {
            Golpe(); 
            tiempoSiguienteAtaque = tiempoEntreAtaques;
        }
    }

    private void Golpe()
    {
        animator.SetTrigger("Golpe");  
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);
        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemy"))
            {
                IDaño enemigo = colisionador.GetComponent<IDaño>();
                if (enemigo != null)
                {
                    Vector2 direccionGolpe = (colisionador.transform.position - transform.position).normalized;
                    enemigo.TomarDaño(dañoGolpe, direccionGolpe);
                    SpriteRenderer spriteRenderer = colisionador.GetComponent<SpriteRenderer>();
                    if (spriteRenderer != null)
                    {
                        StartCoroutine(CambiarColorRojo(spriteRenderer));
                    }
                }
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);  
    }
}
