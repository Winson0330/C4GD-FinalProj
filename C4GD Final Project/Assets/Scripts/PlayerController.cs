using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D body;
    public static PlayerController instance;
    public float moveSpeed = 10;
    public bool onIsland;

    void Start(){
        instance = this;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (onIsland){
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            Vector2 inputDir = new Vector2(horizontalInput, verticalInput).normalized;

            body.velocity = inputDir * moveSpeed;
        } else {
            float horizontalInput = Input.GetAxisRaw("Horizontal");

            Vector2 inputDir = new Vector2(horizontalInput, 0);

            body.velocity = inputDir * moveSpeed;
        }
    }
}
