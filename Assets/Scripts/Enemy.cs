using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int health;
    public float speed;
    public float attackdistance;
    public GameObject deathanimation;

    protected Animator anim;
    protected bool Orientation;
    protected Transform target;
    protected float targetDistance;
    protected Rigidbody2D player;
    protected SpriteRenderer Sprite;


    void Awake()
    {
        anim = GetComponent<Animator>();
        target = FindObjectOfType<PlayerMovementV2>().transform;
        player = GetComponent<Rigidbody2D>();
        Sprite = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        targetDistance = transform.position.x - target.position.x;
    }

    protected void Flip()
    {
        Orientation = !Orientation;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {

            gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(TookDamageCoRoutine());
        }
    }

    IEnumerator TookDamageCoRoutine()
    {
        Sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        Sprite.color = Color.white;
    }
}
