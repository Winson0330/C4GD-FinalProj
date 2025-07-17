using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public GameObject InventoryUI;
    public GameObject GiveFish;
    public GameObject fishQuota;
    public TMP_Text howManyToGive;
    public TMP_Text amountToGive;
    public bool onIsland;
    public int talkingWithCreatureState;
    //1: talking to creature but not giving fish
    //2: not talking to creature
    //3: talking to creature and giving fish
    public bool justFinishedTalking = false;
    bool inventoryOpened;
    public List<Sprite> FishSprites = new List<Sprite>();
    public List<string> newCatches = new List<string>(); //newCatches keeps track of the fish caught between scenes
    public List<string> FishTypes = new List<string>(); //FishTypes and FishCounts keep track of all fish ever caught
    List<int> FishCounts = new List<int>();
    string chosenFishToGive;
    int amountCurrentlyGiven = 0;
    public int remaniningFishCount;
    int requiredFishCount;

    void Awake(){
        requiredFishCount = 5;
        remaniningFishCount = requiredFishCount;
        talkingWithCreatureState = 2;
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void onDeposit(string buttonName){
        justFinishedTalking = true;
        InventoryUI.SetActive(false);
        GiveFish.SetActive(true);
        chosenFishToGive = InventoryUI.transform.Find(buttonName).Find("Fish Type").GetComponent<TMP_Text>().text;
        howManyToGive.text = "How many " + chosenFishToGive + " do you want to give?";
    }

    public void increaseAmt(){
        int currentAmountToGive = int.Parse(amountToGive.text);
        int maxFish = FishCounts[FishTypes.IndexOf(chosenFishToGive)];
        if (currentAmountToGive < maxFish){
            currentAmountToGive += 1;
            amountToGive.text = currentAmountToGive.ToString();
        }
    }

    public void decreaseAmt(){
        int currentAmountToGive = int.Parse(amountToGive.text);
        if (currentAmountToGive > 1){
            currentAmountToGive -= 1;
            amountToGive.text = currentAmountToGive.ToString();
        }
    }

    public void giveFishToCreature(){
        amountCurrentlyGiven += int.Parse(amountToGive.text);
        fishQuota.GetComponentInChildren<TMP_Text>().text = amountCurrentlyGiven + " / " + requiredFishCount + " fish";
        remaniningFishCount -= amountCurrentlyGiven;
        FishCounts[FishTypes.IndexOf(chosenFishToGive)] -= amountCurrentlyGiven;
        GiveFish.SetActive(false);
    }

    void Update(){
        //update inventory with new fish
        if (onIsland){
            if (newCatches.Count > 0){ //if player has caught fish
                foreach (string fish in newCatches){ 
                    if (!FishTypes.Contains(fish)){ //if fish is not in inventory, add to inventory
                        FishTypes.Add(fish);
                        FishCounts.Add(1);
                    } else {
                        FishCounts[FishTypes.IndexOf(fish)] += 1;
                    }
                }
            }

            if (FishTypes.Count > 0 && FishCounts.Count > 0){
                //fishtypes(0) -> slot1 fishtype text, fishcounts(0) -> slot1 fishcount text
                for (int i = 0; i < FishTypes.Count; i++){
                    Transform currentSlot = InventoryUI.transform.Find("Slot" + (i + 1)).transform;
                    currentSlot.Find("Fish Type").GetComponent<TMP_Text>().text = FishTypes[i];
                    currentSlot.Find("Fish Count").GetComponent<TMP_Text>().text = "x" + FishCounts[i];
                    foreach (Sprite fishSprite in FishSprites){
                        if (fishSprite.name == FishTypes[i]){
                            GameObject fishImage = currentSlot.Find("Fish Image").gameObject;
                            fishImage.SetActive(true);
                            fishImage.GetComponent<Image>().sprite = fishSprite;
                        }
                    }
                }
            }
            newCatches.Clear();
        }

        //opening and closing inventory
        if ((Input.GetKeyDown(KeyCode.E) && !inventoryOpened && onIsland && talkingWithCreatureState == 2) || talkingWithCreatureState == 3){
            InventoryUI.SetActive(true);
            inventoryOpened = true;
        } else if ((Input.GetKeyDown(KeyCode.E) && inventoryOpened) || !onIsland || talkingWithCreatureState == 1 || justFinishedTalking){
            InventoryUI.SetActive(false);
            inventoryOpened = false;
            justFinishedTalking = false;
        } else if (justFinishedTalking){
            GiveFish.SetActive(false);
        }

        //depositing fish to creature
        foreach (Transform slot in InventoryUI.transform){
            slot.gameObject.GetComponent<Button>().interactable = (talkingWithCreatureState == 3);
        }
    }
}