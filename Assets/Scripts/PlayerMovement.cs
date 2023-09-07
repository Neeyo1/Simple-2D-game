using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    public float speed = 10.0f;
    public float boundX = 10.0f;
    public float boundY = 10.0f;
    public Rigidbody2D rb;
    public bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            transform.Translate(Vector2.right * horizontalInput * Time.deltaTime * speed);
            transform.Translate(Vector2.up * verticalInput * Time.deltaTime * speed);
        }
        
        if(transform.position.x <= -boundX)
        {
            transform.position = new Vector2(-boundX, transform.position.y);
        }
        if(transform.position.x >= boundX)
        {
            transform.position = new Vector2(boundX, transform.position.y);
        }
        if(transform.position.y <= -boundY)
        {
            transform.position = new Vector2(transform.position.x, -boundY);
        }
        if(transform.position.y >= boundY)
        {
            transform.position = new Vector2(transform.position.x, boundY);
        }
        
    }
}
