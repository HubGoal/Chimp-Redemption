using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BioApeScript : Enemy
{

    public float walkDistance;

    private bool Walk = false;
    private bool Attack = false;


    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        
        base.Update();

        anim.SetBool("Walk", Walk);
        anim.SetBool("Attack", Attack);

        if(Mathf.Abs(targetDistance) < walkDistance)
        {
            Walk = true;
        }
        else
        {
            Walk = false;
        }
        if(Mathf.Abs(targetDistance) < attackdistance)
        {
            Attack = true;
            Walk = false;
        }
    }

    private void FixedUpdate()
    {
        if(Walk && !Attack) 
        { 
            if(targetDistance < 0)
            {
                player.velocity = new Vector2(speed, player.velocity.y);
                if(Orientation)
                {
                    Flip();
                }
            }
            else
            {
                player.velocity = new Vector2(-speed, player.velocity.y);
                if (!Orientation)
                {
                    Flip();
                }
            }

        }
    }

    public void ResetAttack()
    {
        Attack = false; 
    }
}
