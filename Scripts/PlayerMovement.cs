using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    public float Grav;
    public float lowjumpGrav;
    private Rigidbody2D rb;
    private Animator anim;
    private bool grounded;
    private bool canclimb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        float horizontalinput = Input.GetAxis("Horizontal");
        float verticalinput = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(horizontalinput * moveSpeed, rb.velocity.y);

        if (horizontalinput > 0.01f)
        {
            transform.localScale = new Vector3(7, 7, 1);
        } 
         if (horizontalinput < -0.01f)
        {
            transform.localScale = new Vector3(-7, 7, 1);
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {

            rb.velocity = Vector2.up * jumpSpeed;
            grounded = false;
            if (rb.velocity.y < 0)
            {
                
                rb.velocity += (Grav - 1) * Physics2D.gravity.y * Time.deltaTime * Vector2.up;
                
            }
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump") && grounded == false)
            {

                rb.velocity += (lowjumpGrav - 1) * Physics2D.gravity.y * Time.deltaTime * Vector2.up;

            }
            
        }

        anim.SetBool("run", horizontalinput != 0);
        anim.SetBool("grounded", grounded);
        anim.SetBool("canclimb", canclimb);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) {
            grounded = true;
            canclimb = false;
        }
        else if


            (collision.gameObject.CompareTag("Ladder")){
            canclimb = true;
            grounded = false;
        }

        }
       
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
            
        else if (collision.gameObject.CompareTag("Ladder"))
        {
            canclimb = false;
            grounded = false;
        }
            
    }

    private void OnTriggerStay2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            grounded = false;
            canclimb = true;
        }
        else if (collision.gameObject.CompareTag("Ground"))
            canclimb = false;
            grounded = true;
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder")){
            grounded = false;
            canclimb = false;
        }

    }
}
