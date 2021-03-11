using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opossum : Enemy
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    private float move = 7f;

    [SerializeField] private LayerMask ground;

    private Collider2D coll;


    [SerializeField] private bool factingLeft = true;

    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();

    }


    private void Update()
    {
        Move();
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
                    rb.velocity = new Vector2(-move, rb.velocity.y);
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
                    rb.velocity = new Vector2(move, rb.velocity.y);
                }
            }
            else
            {
                factingLeft = true;
            }
        }
    }


}
