using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D body;
    public float moveSpeed = 10;
    AudioSource audio;
    Animator anim;

    void Start(){
        anim = GetComponent<Animator>();
        audio=GetComponent<AudioSource>();
    }
    
    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if(body.velocity.magnitude>=0.1f&&!audio.isPlaying) audio.Play();
        else if(body.velocity.magnitude<=0.1f&&audio.isPlaying) audio.Stop();

        if (horizontalInput < 0){
            transform.localScale = new Vector3(-1, 1, 1);
        }
        
        if (horizontalInput > 0){
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (Inventory.instance.onIsland){ //requires you to start from main island scene
            Vector2 inputDir = new Vector2(horizontalInput, verticalInput).normalized;
            body.velocity = inputDir * moveSpeed;
        } else {
            Vector2 inputDir = new Vector2(horizontalInput, 0);
            body.velocity = inputDir * moveSpeed;
        }

        anim.SetBool("isRunning", body.velocity != Vector2.zero);
    }
}