using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Rigidbody2D rb;
    public Rigidbody2D planet;
    public Vector2 jumpForce;
    public float moveSpeed;
    public float maxSpeed;

    public bool onGround;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey)
        {
            if (Input.GetKey(KeyCode.UpArrow) && onGround)
            {
                rb.AddForce(jumpForce);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                planet.transform.Rotate(transform.forward, moveSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                planet.transform.Rotate(transform.forward, -moveSpeed * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Planet":
                onGround = true;
                break;
            case "Platform":
                
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Planet":
                onGround = false;
                break;
        }
    }
}
