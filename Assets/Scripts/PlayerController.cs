using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


    private float speed = 1;

    private int spacing = 1;

    private Vector2 pos;

	private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pos = transform.position;
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal, 0.0f);

        if (Input.GetKeyDown(KeyCode.W))
            pos.y += spacing;
        if (Input.GetKeyDown(KeyCode.S))
            pos.y -= spacing;
        if (Input.GetKeyDown(KeyCode.A))
            pos.x -= spacing;
        if (Input.GetKeyDown(KeyCode.D))
            pos.x += spacing;

        transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
    }
}