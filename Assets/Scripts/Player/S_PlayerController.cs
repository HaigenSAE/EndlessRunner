using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class S_PlayerController : MonoBehaviour
{
    public float speedMultiplier = 1.0f;
    public float score;
    public float jumpForce = 10;
    public int lives = 3;
    public bool canMove;

    [SerializeField] private LayerMask groundLM;
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private bool isGrounded;
	[SerializeField] private bool invincible;

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

		anim.SetFloat("vertMotion", rb.velocity.y);
	
        //ground checking
        if(Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLM))
        {
            isGrounded = true;
			//reset jump anim trigger
			anim.ResetTrigger("jumped");
            anim.SetBool("isInAir", false);
        }
        else
        {
            isGrounded = false;
        }

        if(canMove)
        {
			if(rb.velocity.y < -0.01)
			{
				anim.SetBool("isInAir", true);
			}
			
            if(isGrounded)
            {
                //Jump if on ground
                if (Input.GetButtonDown("Jump") || Input.touchCount > 0)
                {
                    anim.SetTrigger("jumped");
					isGrounded = false;
                }
            }
			anim.SetFloat("Speed", gm.gameSpeed * 2);			
        }
        else
        {
            //Stop moving
            rb.velocity = Vector2.zero;
			anim.SetFloat("Speed", 0);
        }
        

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //handling collision cases
        switch (col.gameObject.tag)
        {
            case "Obstacle":
				if(!invincible)
				{
					lives--;
					gm.UpdateLives();
					if(lives <= 0)
					{
						gm.ShowEndScreen();
						canMove = false;
					}
					Destroy(col.gameObject);
				} 
                break;
            case "KillZone":
                gm.RestartGame();
                break;
            case "Pickup":
                score++;
                //check if speed should increase every 10 score
                if(score % 2 == 0)
                {
                    gm.gameSpeed = score / 2 * 0.5f + 1;
                }
                gm.scoreText.text = "SCORE: " + score;
                Destroy(col.gameObject);
                break;
        }
    }
	
	public void Jump()
	{
		rb.velocity = new Vector2(rb.velocity.x, jumpForce);   
	}
}
