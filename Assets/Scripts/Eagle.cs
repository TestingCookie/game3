using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : Enemy
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    [SerializeField] private float topCap;
    [SerializeField] private float bottomCap;

    //private float flyUp = 7f;
    private float flyForward = 7f;

    [SerializeField] private bool factingLeft = true;

    private Collider2D coll;

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
   
                rb.velocity = new Vector2(-flyForward, rb.velocity.y);
  
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

                rb.velocity = new Vector2(flyForward, rb.velocity.y);

            }
            else
            {
                factingLeft = true;
            }
        }
    }
}
