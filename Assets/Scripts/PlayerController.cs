using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jump_speed;

    private bool facing_right = true;
    private bool warping = false;

    private Rigidbody2D rb;
    private Animator anim;
    private Transform trans;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void SetWarping(bool state)
    {
        warping = state;
        anim.SetBool("Warping", state);
    }

    void FixedUpdate ()
    {
        if (!warping) { 
            float moveHorizontal = Input.GetAxis("Horizontal");
            Vector2 movement = new Vector2(moveHorizontal, 0.0f);
            rb.velocity = movement * speed;

            // Animation
            anim.SetFloat("SpeedX", Mathf.Abs(rb.velocity.x));
            anim.SetFloat("SpeedY", Mathf.Abs(rb.velocity.y));

            // Flip sprite
            if ((facing_right && moveHorizontal < 0) || (!facing_right && moveHorizontal > 0))
            {
                Flip();
            }
        } else
        {
            rb.velocity = new Vector2(0f, 0f);
        }
    }

    void Flip()
    {
        facing_right = !facing_right;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    void Jump()
    {
        if (Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.AddForce(Vector2.up * jump_speed);
        }
    }

    public bool IsWarping()
    {
        return warping;
    }
}