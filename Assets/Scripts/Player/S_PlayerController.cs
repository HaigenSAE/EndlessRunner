using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class S_PlayerController : MonoBehaviour
{
    public float moveSpeed = 3;
    public float speedMultiplier = 1.0f;
    public float score;
    public float jumpForce = 10;
    public int lives = 3;
    public bool canMove;

    [SerializeField] private LayerMask groundLM;
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private bool isGrounded;

    private float groundRad = 0.15f;
    private Rigidbody2D rb;
    private Collider2D col2D;
    private Animator anim;
    private GameManager gm;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col2D = GetComponent<Collider2D>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        //reset jump anim trigger
        anim.ResetTrigger("jumped");
        //ground checking
        if(Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLM))
        {
            isGrounded = true;
            anim.SetBool("isInAir", false);
        }
        else
        {
            isGrounded = false;
        }

        if(canMove)
        {
            rb.velocity = new Vector2(moveSpeed * speedMultiplier, rb.velocity.y);

            if(isGrounded)
            {
                //Jump if on ground
                if (Input.GetButtonDown("Jump") || Input.touchCount > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    isGrounded = false;
                    anim.SetTrigger("jumped");
                    anim.SetBool("isInAir", true);
                }
            }
            else
            {
                anim.SetBool("isInAir", true);
            }
            
        }
        else
        {
            //Stop moving
            rb.velocity = Vector2.zero;
        }
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //handling collision cases
        switch (col.gameObject.tag)
        {
            case "Obstacle":
                lives--;
                gm.UpdateLives();
                if(lives <= 0)
                {
                    gm.ShowEndScreen();
                    canMove = false;
                }
                Destroy(col.gameObject);
                break;
            case "KillZone":
                gm.RestartGame();
                break;
            case "Pickup":
                score++;
                //check if speed should increase every 10 score
                if(score % 10 == 0)
                {
                    speedMultiplier = score / 10 * 0.1f + 1;
                }
                gm.scoreText.text = "SCORE: " + score;
                Destroy(col.gameObject);
                break;
        }
    }
}
