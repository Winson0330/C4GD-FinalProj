using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    //rudimentary fishing system
    //press a key (e?) to fish
    //after a bit, you get a fish (maybe pull from an array?)

    public string[] FishTypes = {"Tuna", "Salmon", "Mackerel", "Shark"};
    bool canFish = true;
    public bool inWater = false;
    float fishRate = 3;
    float canFishTimer = 3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canFishTimer > 0 && !canFish){ //3s timer before player can fish again
            canFishTimer -= Time.deltaTime;
        } else {
            canFishTimer = fishRate;
            canFish = true;
        }
        if (Input.GetKeyDown(KeyCode.E) && canFish && inWater){
            print("You fished a " + FishTypes[Random.Range(0, FishTypes.Length)] + "!");
            canFish = false;
        }
    }
}
