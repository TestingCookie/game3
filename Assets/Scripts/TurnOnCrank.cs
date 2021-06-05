using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnCrank : MonoBehaviour
{
    public bool triggered = false;
    private SpriteRenderer sprite;
    private Sprite spriteChanged;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        spriteChanged = Resources.Load<Sprite>("crank-up");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D touchPlayer)
    {
        if (touchPlayer.gameObject.tag == "Player")
        {
            triggered = true;
            sprite.sprite = spriteChanged;
        }
    }

}
