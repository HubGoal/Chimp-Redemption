using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float JumpForce;
    public float Speed;

    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    private float Vertical;
    private bool Grounded;
    private Animator Animator;
    private float LastShoot;
    private float playerSpeed = 8;


    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");


        Animator.SetBool("Walking", Horizontal != 0.0f);

        if (Horizontal < 0.0f) transform.localScale = new Vector3(-0.8f, 0.8f, 0.8f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);



        if (Physics2D.Raycast(transform.position, Vector3.down, 0.2f))
        {
            Grounded = true;
            Animator.SetBool("Jumping", false);
        }
        else
        {
            Grounded = false;
        }

        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }
         
        if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }

    }

    private void Jump()
    {
        Animator.SetBool("Jumping", Vertical < 0.2f);
        Rigidbody2D.AddForce(Vector2.up * JumpForce);


    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 0.8f) direction = Vector2.right;
        else direction = Vector2.left;

        // Calculate the position of the bullet based on the character's position
        Vector3 bulletSpawnPosition = transform.position + new Vector3(direction.x * 0.6f, 0.5f, 0.0f);

        // Instantiate the bullet at the correct position
        GameObject bullet = Instantiate(BulletPrefab, bulletSpawnPosition, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }


    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal, Rigidbody2D.velocity.y);
    }

}