using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;
    protected AudioSource boomDeath;

    public GameObject makegem;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boomDeath = GetComponent<AudioSource>();


    }
    public void Killed()
    {
        anim.SetTrigger("death");
        boomDeath.Play();
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Collider2D>().enabled = false;
    }


    private void Death()
    {
        Destroy(this.gameObject);
        //Instantiate(makegem, new Vector2(0, 0), Quaternion.identity);
    }
}
