using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float distance;
    [SerializeField] private Transform groundDetection;
    private bool movingLeft = true;
    private enum State {idle, running, jumping, falling, hurt}
    private State state = State.idle;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        state = State.running;
    }

    
    void Update()
    {
        anim.SetInteger("state", (int)state);
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);

        if(groundInfo.collider == false)
        {
            if(movingLeft == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingLeft = false;
            }
            else 
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingLeft = true;
            }
        }
    }
}
