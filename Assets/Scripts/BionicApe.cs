using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float velocidad = 5f;
    public float radioDeteccion = 10f;
    public float radioAtaque = 2f;
    private Transform jugador;
    private Animator animator;

    private void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();

        if (jugador == null)
        {
            Debug.LogError("No se pudo encontrar el jugador. Asegúrate de que tenga la etiqueta 'Player'.");
        }
    }

    private void Update()
    {
        float distanciaAlJugador = Vector3.Distance(transform.position, jugador.position);

        // Verificar si hay un obstáculo entre el enemigo y el jugador
        bool hayObstaculo = Physics.Linecast(transform.position, jugador.position, LayerMask.GetMask("Obstacle"));

        if (distanciaAlJugador <= radioAtaque)
        {
            animator.SetBool("Attack", true);
        }
        else if (distanciaAlJugador <= radioDeteccion && !hayObstaculo)
        {
            animator.SetBool("Walk", true);
            animator.SetBool("Attack", false);
            MoverHaciaJugador();
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Walk", false);
            animator.SetBool("Attack", false);
        }
    }

    private void MoverHaciaJugador()
    {
        Vector3 direccionAlJugador = (jugador.position - transform.position).normalized;
        transform.Translate(direccionAlJugador * velocidad * Time.deltaTime);
    }
}
