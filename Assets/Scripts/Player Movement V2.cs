using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementV2 : MonoBehaviour
{
    public float speed;
    public float JumpForce;

    private Rigidbody2D RGBD2D;
    private bool Orientation = true;
    private bool Jump;
    private bool Grounded = false;
    private Transform groundCheck;
    private float hForce = 0;

    private bool isDead = false;

    void Start()
    {
        RGBD2D = GetComponent<Rigidbody2D>();
        groundCheck = GameObject.Find("GroundCheck").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            // Usar Raycast para detectar el suelo con el tag "Ground"
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));

            // Verificar si el rayo golpea algo con el tag "Ground"
            Grounded = hit.collider != null;

            if (Input.GetButtonDown("Jump") && Grounded)
            {
                Jump = true;
            }
            else if (Input.GetButtonUp("Jump"))
            {
                if (RGBD2D.velocity.y > 0)
                {
                    RGBD2D.velocity = new Vector2(RGBD2D.velocity.x, RGBD2D.velocity.y * 0.5f);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            hForce = Input.GetAxisRaw("Horizontal");
            RGBD2D.velocity = new Vector2(hForce * speed, RGBD2D.velocity.y);
        }
        else if (hForce > 0 && !Orientation)
        {
            Flip();
        }
        else if (hForce < 0 && Orientation)
        {
            Flip();
        }

        if (Jump)
        {
            Jump = false;
            RGBD2D.AddForce(Vector2.up * JumpForce);
        }
    }

    void Flip()
    {
        Orientation = !Orientation;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
