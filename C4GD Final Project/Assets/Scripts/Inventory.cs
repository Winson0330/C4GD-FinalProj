using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public GameObject InventoryUI;
    public bool onIsland;
    bool inventoryOpened;
    public List<string> newCatches = new List<string>(); //newCatches keeps track of the fish caught between scenes
    List<string> FishTypes = new List<string>(); //FishTypes and FishCounts keep track of all fish ever caught
    List<int> FishCounts = new List<int>();

    void Awake(){
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(InventoryUI);
        } else {
            Destroy(gameObject);
            Destroy(InventoryUI);
        }
    }

    void Update(){
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
                }
            }
            newCatches.Clear();
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
