using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BionicApe : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update(); 

        if (targetDistance < attackdistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }
}
