using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float velocidad = 5f;
    public float radioDeteccion = 4f;
    public float radioAtaque = 1f;
    private Transform jugador;
    private Animator animator;
    private bool estaObstaculizado = false;
    private int Health = 3;

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
        if (!estaObstaculizado)
        {
            float distanciaAlJugador = Vector3.Distance(transform.position, jugador.position);

            if (distanciaAlJugador <= radioAtaque)
            {
                animator.SetBool("Attack", true);
                MoverHaciaJugador();
            }
            else if (distanciaAlJugador <= radioDeteccion)
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
    }

    private void MoverHaciaJugador()
    {
        Vector3 direccionAlJugador = (jugador.position - transform.position).normalized;

        // Cambiar la escala para voltear en el eje x
        if (direccionAlJugador.x >= 0.0f)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        else
        {
            transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
        }

        // Mover hacia el jugador
        transform.position = Vector3.MoveTowards(transform.position, jugador.position, velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            estaObstaculizado = true;
            DetenerMovimiento();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            estaObstaculizado = false;
        }
    }

    public void Hit()
    {
        Health = Health - 1;
        if (Health == 0) Destroy(gameObject);
    }

    private void DetenerMovimiento()
    {
        animator.SetBool("Idle", true);
        animator.SetBool("Walk", false);
        animator.SetBool("Attack", false);
    }
}
