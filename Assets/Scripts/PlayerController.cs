using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;

	private Rigidbody2D rb;
    private Animator anim;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");

		Vector2 movement = new Vector2 (moveHorizontal, 0.0f);

		rb.AddForce (movement * speed);

        // Animation
        anim.SetFloat("SpeedX", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("SpeedY", Mathf.Abs(rb.velocity.y));
	}
}