using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    public TMPro.TMP_Text resultText;
    public string[] FishTypes = {"Tuna", "Salmon", "Mackerel", "Shark"};
    public int[] EnduranceLevels = {1, 2, 3, 5};
    public bool canFish = true;
    public bool inFishSpot = false;
    public bool isFishing;
    float fishRate = 5; //these two are for how fast the player can fish
    float canFishTimer = 5;
    float stayOnScreen = 3; //these two are for how long the result stays on screen
    float displayTimer = 3;
    public FishingMinigame minigameRef;

    void Start(){
        minigameRef= GameObject.FindGameObjectWithTag("FishConnect").GetComponent<FishingMinigame>();
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
            isFishing=true;
            minigameRef.minigame.GetComponent<RectTransform>().anchoredPosition = new Vector2(399.375f, 224.5f);
            minigameRef.canvase.alpha=1;
        }

        if(minigameRef.minigameCompleted){
            int chosenFish = Random.Range(0, FishTypes.Length);
            if(minigameRef.minigameSuccess){
                  resultText.text = "You fished a " + FishTypes[chosenFish] + "!";
                 Inventory.instance.newCatches.Add(FishTypes[chosenFish]);
            }else if(minigameRef.minigameCompleted&&minigameRef.minigameFailure) {
                 resultText.text = "The " + FishTypes[chosenFish] + " got away...";
               }
                 minigameRef.minigameCompleted=!minigameRef.minigameCompleted;
                 minigameRef.minigameSuccess=!minigameRef.minigameSuccess;
                 minigameRef.minigameFailure=!minigameRef.minigameFailure;
                 minigameRef.canvase.alpha=0;
                 minigameRef.bar=40;
                 isFishing=false;
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
