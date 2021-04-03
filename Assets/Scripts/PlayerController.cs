using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;
    private CircleCollider2D coll2;



    private enum State {idle, running, jumping, falling, hurt, climb};
    private State state = State.idle;

    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 7f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private int points = 0;
    [SerializeField] private Text pointsText;
    private int health = 5;
    [SerializeField] private Text healthAmount;
    [SerializeField] private float hurtForce = 10f;
    [SerializeField] private AudioSource footstep;
    [SerializeField] private AudioSource gemCollect;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        coll2 = GetComponent<CircleCollider2D>();

        Time.timeScale = 1;
    }
    private void Update()
    {
        if (state != State.hurt)
        {
            Movement();
        }

        AnimationState();
        anim.SetInteger("state", (int)state);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collectable")
        {
            gemCollect.Play();
            Destroy(collision.gameObject);
            if(pointsText != null)
                {
                points += 1;
                pointsText.text = points.ToString();
            }
        }

        if(collision.name == "SpecialEagle2")
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy") 
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (state == State.falling && other.gameObject.name != "SpecialEagle2")
            {
                if (pointsText != null)
                {
                    points += 5;
                    pointsText.text = points.ToString();
                }
                enemy.Killed();
                Jump();
            }
            else
            {
                state = State.hurt;
                healthHandler();
                
                StartCoroutine(HurtedColor());
                if (other.gameObject.transform.position.x > transform.position.x) //enemy is to my right
                {
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }

            }
        }
    }

    private void healthHandler()
    {
        health -= 1;

        if (healthAmount != null)
        {
            
            healthAmount.text = health.ToString();
        }
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");

        //Moving L/R
        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        //Jump
        if (Input.GetButtonDown("Jump") && coll2.IsTouchingLayers(ground))
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        state = State.jumping;
    }
    private void AnimationState()
    {
        if (state == State.jumping)
        {
            if(rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if(state == State.falling){
            if (coll2.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            if (Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }
        //else if (coll.IsTouchingLayers(ground))
        //{
        //    state = State.climb;
        //
        else if(Mathf.Abs(rb.velocity.x) > 2f)
        {
            //Moving
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }

    private void Footsteps()
    {
        footstep.Play();
    }

    private IEnumerator HurtedColor()
    {
       GetComponent<SpriteRenderer>().color = Color.red;
       yield return new WaitForSeconds(1);
       GetComponent<SpriteRenderer>().color = Color.white;
    }
}
