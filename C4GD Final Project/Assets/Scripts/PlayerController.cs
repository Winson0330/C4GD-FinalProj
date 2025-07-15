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
    public List<string> inventory = new List<string>();
    public GameObject InventoryUI;
    bool inventoryOpened;
    List<string> FishTypes = new List<string>();
    List<int> FishCounts = new List<int>();

    void Start(){
        instance = this;
        anim = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    { 
        // Set movement
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

        // Set inventory
        if (onIsland){
            if (inventory.Count > 0){ //if player has fish in inv
                foreach (string fish in inventory){ 
                    if (!FishTypes.Contains(fish)){ //if fish is not in inventory, add to inventory
                        FishTypes.Add(fish);
                        FishCounts.Add(1);
                    } else {
                        FishCounts[FishTypes.IndexOf(fish)] += 1;
                    }
                }
            }
        }

        foreach (string fish in FishTypes){
            print(fish);
        }
        foreach (int count in FishCounts){
            print(count);
        }

        if (Input.GetKeyDown(KeyCode.E) && onIsland && !inventoryOpened){
            InventoryUI.SetActive(true);
            inventoryOpened = true;
        } else if (Input.GetKeyDown(KeyCode.E) && onIsland && inventoryOpened){
            InventoryUI.SetActive(false);
            inventoryOpened = false;
        }
    }
}