using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEagle : Eagle
{
    private BoxCollider2D collEvent;
    protected override void Start()
    {
        base.Start();
        collEvent = GetComponent<BoxCollider2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;

        flyForward = 15f;
}
    


    private void Update()
    {
        if (gameObject.transform.position.y <= bottomCap)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - 1.5f);
            
        }
    }

}
