using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] bool inPlay;
    [SerializeField] Transform paddle;
    [SerializeField] float ballSpeed = 500f;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(!inPlay)
        {
            transform.position = paddle.position;
        }

        if (Input.GetButtonDown("Jump") && !inPlay)
        {
            inPlay = true;
            rb.AddForce(Vector2.up * ballSpeed);
        }
    }

    public void ResetBall()
    {
        transform.position = paddle.position;
        inPlay = false;
        rb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Bottom"))
        {
            ResetBall();
            GameManager.i.UpdateNumberOfLives();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Brick"))
        {
            other.gameObject.GetComponent<BrickParent>().TakeDamage(1);
        }
    }
}