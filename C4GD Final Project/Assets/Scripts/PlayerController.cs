using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D body;
    public float moveSpeed = 10;
    Animator anim;
    public List<string> newCatches = new List<string>(); //newCatches keeps track of the fish caught between scenes
    List<string> FishTypes = new List<string>(); //FishTypes and FishCounts keep track of all fish ever caught
    List<int> FishCounts = new List<int>();

    void Start(){
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

        if (Inventory.instance.onIsland){
            Vector2 inputDir = new Vector2(horizontalInput, verticalInput).normalized;
            body.velocity = inputDir * moveSpeed;
        } else {
            Vector2 inputDir = new Vector2(horizontalInput, 0);
            body.velocity = inputDir * moveSpeed;
        }

        anim.SetBool("isRunning", body.velocity != Vector2.zero);
    }
}