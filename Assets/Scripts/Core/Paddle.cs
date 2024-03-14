using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float paddleSpeed = 10f;
    float horizontal;
    float edge = 7.64f;

    private void Update(){
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * horizontal * Time.deltaTime * paddleSpeed);

        if (transform.position.x < -edge){
            transform.position = new Vector2(-edge, transform.position.y);
        }if(transform.position.x > edge){
            transform.position = new Vector2(edge, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Collided");
        if (other.CompareTag("PowerUp"))
        {
            print("Collided");
            other.GetComponent<ExtraLife>().ApplyPowerUp();
        }
    }

}
