using UnityEngine;

public class Jefe_CaminarBehaviour : StateMachineBehaviour
{
    private Jefe jefe;
    [SerializeField] private float velocidadMovimiento;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        jefe = animator.GetComponent<Jefe>();
        // Llamar al método MirarJugador para que el jefe se oriente hacia el jugador
        jefe.MirarJugador();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Mover al jefe hacia el jugador
        jefe.SeguirJugador();  // Llamamos al método SeguirJugador() del Jefe que se encarga del movimiento
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // No es necesario manipular el Rigidbody2D aquí
    }
}
