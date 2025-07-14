using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    //rudimentary fishing system
    //press a key (e?) to fish
    //after a bit, you get a fish (maybe pull from an array?)

    public string[] FishTypes = {"Tuna", "Salmon", "Mackerel", "Shark"};
    public int[] EnduranceLevels = {1, 2, 3, 5};
    bool canFish = true;
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
        if (Input.GetKeyDown(KeyCode.E) && canFish){
            int fishingPower = Random.Range(0, 6);
            int chosenFish = Random.Range(0, FishTypes.Length);

            if (fishingPower >= EnduranceLevels[chosenFish]){
                print("You fished a " + FishTypes[chosenFish] + "!");
            } else {
                print("The fish got away...");
            }
            canFish = false;
        }
    }
}
