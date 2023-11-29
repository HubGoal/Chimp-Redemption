using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementV2 : MonoBehaviour
{
    public float Speed = 10f;
    public float JumpForce = 20f;
    public GameObject bulletPrefab;
    public Transform shootSpawner;
    public float damageTime = 1f;


    private Animator anim;
    private Rigidbody2D RGBD2D;
    private bool Orientation = true;
    private bool Jump;
    private bool Grounded = false;
    private Transform groundCheck;
    private float hForce = 0;
    public float fireRate = 0.5f;
    private int health;
    private int maxHealth;
    private float nextFire;
    private bool tookDamage = false;


    private bool isDead = false;

    GameManager gameManager;
    void Start()
    {
        RGBD2D = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck");
        anim = GetComponent<Animator>();
        health = maxHealth;

        gameManager = GameManager.gameManager;

        SetPlayerStatus();
        health = maxHealth;


    }

    void Update()
    {
        if (!isDead)
        {
            Grounded = Physics2D.Linecast(transform.position, transform.position + groundCheck.localPosition, 1 << LayerMask.NameToLayer("Ground"));

            if (Grounded)
            {
                anim.SetBool("Jump", false);
            }


            if (Input.GetKeyDown(KeyCode.W) && Grounded)
            {
                Jump = true;
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                if (RGBD2D.velocity.y > 0)
                {
                    RGBD2D.velocity = new Vector2(RGBD2D.velocity.x, RGBD2D.velocity.y * 0.5f);
                }
            }
            if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                anim.SetTrigger("Shoot");
                GameObject tempBullet = Instantiate(bulletPrefab, shootSpawner.position, shootSpawner.rotation);

                if (!Orientation)
                {
                    tempBullet.transform.eulerAngles = new Vector3(0, 0, 180);
                }
            }


        }
    }



    private void FixedUpdate()
    {
        if (!isDead)
        {

            hForce = Input.GetAxisRaw("Horizontal");
            anim.SetFloat("Speed", Mathf.Abs(hForce));

            RGBD2D.velocity = new Vector2(hForce * Speed, RGBD2D.velocity.y);

            if (hForce > 0 && !Orientation)
            {
                Flip();
            }
            else if (hForce < 0 && Orientation) {
                Flip();
            }

            if (Jump)
            {
                anim.SetBool("Jump", true);
                Jump = false;
                RGBD2D.AddForce(Vector2.up * JumpForce);

            }
        }
    }
    void Flip()
    {
        Orientation = !Orientation;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void SetPlayerStatus()
    {
        fireRate = gameManager.firerate;
        maxHealth = gameManager.health;
    }

    void UpdateHealthUI()
    {
        FindObjectOfType<UIManager>().UpdateHealthUI(health);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !tookDamage)
        {
            StartCoroutine(TookDamage());
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && !tookDamage)
        {
            StartCoroutine(TookDamage());
        }
    }

    IEnumerator TookDamage()
    {
        tookDamage = true;
        health --;
        UpdateHealthUI();
        if(health <= 0)
        {
            isDead = true;
            anim.SetTrigger("Death");
            Invoke("ReloadScene", 2f);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(9,10);
            for (float i = 0; i < damageTime; i+= 0.2f)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                yield return new WaitForSeconds(0.1f);
                GetComponent<SpriteRenderer>().enabled = true;
                yield return new WaitForSeconds(0.1f);

            }
            Physics2D.IgnoreLayerCollision(9, 10, false);
            tookDamage = false;
        }

    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
