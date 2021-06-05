using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform positionUp, positionDown;
    public float speed;
    public Transform startPosition;
    public bool test = false;

    Vector3 nextPosition;

    // Start is called before the first frame update
    void Start()
    {
        nextPosition = startPosition.position;
        
        
        
}

    // Update is called once per frame
    void Update()
    {
        GameObject crank = GameObject.Find("crank-down");
        TurnOnCrank turnOnCrank = crank.GetComponent<TurnOnCrank>();

        
        if (turnOnCrank.triggered == true)
        {
            MovePlatform();
        }
    }

    private void MovePlatform()
    {
        if (transform.position == positionUp.position)
        {
            nextPosition = positionDown.position;
        }
        if (transform.position == positionDown.position)
        {
            nextPosition = positionUp.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(positionUp.position, positionDown.position);
    }
}
