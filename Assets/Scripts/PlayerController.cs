using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;

    private bool facing_right = true;

    private Rigidbody2D rb;
    private Animator anim;
    private Transform trans;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        Vector2 movement = new Vector2 (moveHorizontal, 0.0f);
        rb.velocity = movement * speed;

        // Animation
        anim.SetFloat("SpeedX", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("SpeedY", Mathf.Abs(rb.velocity.y));

        // Flip sprite
        if((facing_right && moveHorizontal < 0) || (!facing_right && moveHorizontal > 0))
        {
            Flip();
        }
    }

    void Flip()
    {
        facing_right = !facing_right;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }
}