using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeroController : MonoBehaviour
{
    //Start() variables
    private Rigidbody2D rb;
    private Animator anim;

    //FSM - Finite State Machine
    private enum State {idle, running, jumping, falling, hurt}
    private State state = State.idle;

    private bool isFacingRight = true;
    private bool isGrounded = false; //contact ground check
    private float moveInput;
    private int extraJump;
    public int extraJumpValue;
    public bool keyCheck = false;
    [SerializeField] private GameObject Door;

    //Inspector variables
    [SerializeField] private Transform groundCheck; //the link to the definition of land contact
    [SerializeField] private LayerMask whatIsGround; //the link layer of the earth
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float checkRadius; // the radius of contact
    [SerializeField] private int gem = 0;
    [SerializeField] private Text gemText;
    [SerializeField] private float hurtForce = 10f;
    [SerializeField] private int health;

    private void Start()
    {
        extraJump = extraJumpValue;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(state != State.hurt)
        {
            Run();
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if (!isGrounded) 
        {
            return;
        }
    }

    private void Update() {
        if (isGrounded == true) {
            extraJump = extraJumpValue;
        }
        if (Input.GetKeyDown(KeyCode.Space) && extraJump > 0) 
        {
            Jump();
            extraJump--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJump == 0 && isGrounded == true) 
        {
            Jump();
        }
        AnimationState();
        anim.SetInteger("state", (int)state);//sets animation based on Enumerator state
    }

    private void AnimationState()
    {
        if(state == State.jumping)
        {
            if(rb.velocity.y < 0.1f)
            {
                state = State.falling;
            }
        }
        else if(state == State.falling)
        {
            if(isGrounded)
            {
                state = State.idle;
            }
        }
        else if(state == State.hurt)
        {
            //if(Mathf.Abs(rb.velocity.x) < .1f)
            //{
            //    state = State.idle;
            //}
        }
        else if (Mathf.Abs(rb.velocity.x) > Mathf.Epsilon)
        {
            //Moving
            
            state = State.running;
        }
        else if (Mathf.Abs(rb.velocity.x) > Mathf.Epsilon || rb.velocity.y < 0.01f)
        {
            //Moving
            
            state = State.falling;
        }
        else
        {
            state = State.idle;
        }
    }

    private void Run()
    {
        moveInput = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (moveInput > 0 && isFacingRight == false) 
        {
            Flip();
        }
        else if (moveInput < 0 && isFacingRight) 
        {
            Flip();
        }
    }
    
    private void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
        state = State.jumping;
    }

    private void Flip() 
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D collison) 
    {
        if (collison.tag == "Key")
        {
            Door.GetComponent<Animator>().SetBool("open", true);
            Destroy(collison.gameObject);
            keyCheck = true;
        }  
        if (collison.tag == "Gem")
        {
            Destroy(collison.gameObject);
            gem += 1;
            gemText.text = gem.ToString();
        }   
        if (collison.tag == "DeadCheck" || collison.tag == "Missile")
        {
            state = State.hurt;
            extraJumpValue = -1;
            Destroy(collison.gameObject);
            Invoke("ControlHealth", 2.0f);
        } 
    }

    private void OnCollisionEnter2D(Collision2D kill) 
    {
        if(kill.gameObject.tag == "Enemy")
        {
            if(state == State.falling)
            {
                Destroy(kill.gameObject);
                Jump();
            }
            else
            {
                state = State.hurt;
                Invoke("ControlHealth", 2.5f);
                if (kill.gameObject.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
            }
        }
    }

    private void ControlHealth()
    {
        health -= 1;

        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}