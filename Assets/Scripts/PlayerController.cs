using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jump_speed;
    Camera cam;
    float cam_height;
    float cam_width;
    private bool facing_right = true;
    private bool warping = false;
    private bool controls_locked = false;

    private Rigidbody2D rb;
    private Animator anim;
    private Transform trans;

    void Start ()
    {
        cam = Camera.main;
        cam_height = 2f * cam.orthographicSize;
        cam_width = cam_height * cam.aspect;
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
        Debug.Log(rb.position.x+0.5 + ", " + (cam.transform.position.x - cam_width / 2));
        if (!warping && !controls_locked) { 
            float moveHorizontal = Input.GetAxis("Horizontal");
            if (rb.position.x <= cam.transform.position.x - cam_width / 2 && moveHorizontal < 0)
            {
                moveHorizontal = 0f;
            }
            else if (rb.position.x >= transform.position.x + cam_width / 2 && moveHorizontal > 0)
            {
                moveHorizontal = 0f;
            }
            Vector2 movement = new Vector2(moveHorizontal, 0.0f);
            rb.velocity = movement * speed;
            rb.AddForce(Vector2.up * 10f);
                // Flip sprite
                if ((facing_right && moveHorizontal < 0) || (!facing_right && moveHorizontal > 0))
            {
                Flip();
            }
        } else
        {
            rb.velocity = new Vector2(0f, 0f);
        }

        // Animation
        anim.SetFloat("SpeedX", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("SpeedY", Mathf.Abs(rb.velocity.y));
    }

    void Flip()
    {
        facing_right = !facing_right;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    // Not needed yet
    void Jump()
    {
        if (Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.AddForce(Vector2.up * jump_speed);
        }
    }

    public bool IsControlsLocked()
    {
        return controls_locked;
    }

    public void SetControlsLocked(bool state)
    {
        controls_locked = state;
    }

    public bool IsWarping()
    {
        return warping;
    }
}