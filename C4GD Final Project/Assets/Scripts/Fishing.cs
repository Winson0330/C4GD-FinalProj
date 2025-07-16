using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    public TMPro.TMP_Text resultText;
    public string[] FishTypes;
    public int[] EnduranceLevels;
    bool canFish = true;
    bool inFishSpot = false;
    float fishRate = 5; //these two are for how fast the player can fish
    float canFishTimer = 5;
    float stayOnScreen = 3; //these two are for how long the result stays on screen
    float displayTimer = 3;

    void Start(){
        FishTypes = new string[] {"Tuna", "Red Snapper", "Perch", "Salmon"};
        EnduranceLevels = new int[] {1, 1, 2, 2, 2};
    }

    void Update(){
        if (canFishTimer > 0 && !canFish){ //5s timer before player can fish again
            canFishTimer -= Time.deltaTime;
        } else {
            canFishTimer = fishRate;
            canFish = true;
        }

        if (stayOnScreen > 0){
            stayOnScreen -= Time.deltaTime;
        } else {
            stayOnScreen = displayTimer;
            resultText.text = "";
        }

        if (Input.GetKeyDown(KeyCode.F) && inFishSpot && canFish){
            int fishingPower = Random.Range(0, 4);
            int chosenFish = Random.Range(0, FishTypes.Length);

            if (fishingPower >= EnduranceLevels[chosenFish]){
                resultText.text = "You fished a " + FishTypes[chosenFish] + "!";
                Inventory.instance.newCatches.Add(FishTypes[chosenFish]);
                Deposit.instance.newCatches.Add(FishTypes[chosenFish]);
            } else {
                resultText.text = "The " + FishTypes[chosenFish] + " got away...";
            }
            canFish = false;
        }
    }

    private void OnTriggerStay2D(Collider2D player){
        inFishSpot = true;
    }

    private void OnTriggerExit2D(Collider2D player){
        inFishSpot = false;
    }
}