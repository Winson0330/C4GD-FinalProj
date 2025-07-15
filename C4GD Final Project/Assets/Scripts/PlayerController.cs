using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D body;
    public static PlayerController instance;
    public float moveSpeed = 10;
    public bool onIsland;
    Animator anim;

    void Start(){
        instance = this;
        anim = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput < 0){
            transform.localScale = new Vector3(-1, 1, 1);
        } else if (horizontalInput > 0){
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (onIsland){
            Vector2 inputDir = new Vector2(horizontalInput, verticalInput).normalized;

            body.velocity = inputDir * moveSpeed;
        } else {
            Vector2 inputDir = new Vector2(horizontalInput, 0);

            body.velocity = inputDir * moveSpeed;
        }

        anim.SetBool("isRunning", body.velocity != Vector2.zero);
    }
}
