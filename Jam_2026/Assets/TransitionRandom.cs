using UnityEngine;

public class TransitionRandom : StateMachineBehaviour
{


    public float tiempoMinimo = 260f;
    public float tiempoMaximo = 580f;
    private float cronometro;
    private float tiempoSiguiente;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cronometro = 0;
        tiempoSiguiente = Random.Range(tiempoMinimo, tiempoMaximo);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cronometro += Time.deltaTime;

        if (cronometro >= tiempoSiguiente)
        {
            int eleccion = Random.Range(1, 4);

            animator.SetInteger("NumAction", eleccion);
            animator.SetTrigger("Action");

            cronometro = 0;
        }
    }


}
