using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;

    [SerializeField] private float jumpLenght = 10f;
    [SerializeField] private float jumpHigh = 10f;
    [SerializeField] private LayerMask ground;
    private Collider2D coll;

    private bool factingLeft = true;

    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        //transision from jump to fall4
        if (anim.GetBool("jumping"))
        {
            if(rb.velocity.y < .1)
            {
                anim.SetBool("falling", true);
                anim.SetBool("jumping", false);
            }
        }

        //transition from fall to idle
        if (coll.IsTouchingLayers(ground) && anim.GetBool("falling"))
        {
            anim.SetBool("falling", false);   
        }
    }

    private void Move()
    {
        if (factingLeft)
        {
            if (transform.position.x > leftCap)
            {

                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-jumpLenght, jumpHigh);
                    anim.SetBool("jumping", true);
                }
            }
            else
            {
                factingLeft = false;
            }
        }
        else
        {
            if (transform.position.x < rightCap)
            {

                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(jumpLenght, jumpHigh);
                    anim.SetBool("jumping", true);
                }
            }
            else
            {
                factingLeft = true;
            }
        }
    }

    

}


