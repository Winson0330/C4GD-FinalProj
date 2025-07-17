using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreatureDialogue : MonoBehaviour
{
    public GameObject dialogueTrigger;
    public GameObject creatureDialogue;
    Button progressDialogue;
    TMP_Text dialogueBoxText;
    int clickCount = 0;
    bool talkedOnThisDay = false;

    void Awake(){
        GameObject[] persist = new GameObject[] {creatureDialogue, Inventory.instance.fishQuota, gameObject};
        foreach (GameObject objectToPersist in persist){
            DontDestroyOnLoad(objectToPersist);
        }
    }

    void OnTriggerEnter2D(Collider2D player){
        if (player.gameObject.CompareTag("Player")){
            Inventory.instance.talkingWithCreatureState = 1;
            creatureDialogue.SetActive(true);
            if (!talkedOnThisDay){
                dialogueBoxText.text = "I must be satisfied once more...";
                talkedOnThisDay = true;
            } else {
                dialogueBoxText.text = "Feed me " + Inventory.instance.remaniningFishCount + " more fish and I won't consume the whole world... for now.";
            }
        }
    }

    void OnTriggerExit2D(Collider2D player){
        if (player.gameObject.CompareTag("Player")){
            creatureDialogue.SetActive(false);
            Inventory.instance.talkingWithCreatureState = 2;
            Inventory.instance.justFinishedTalking = true;
            clickCount = 1;
        }
    }

    public void onClick(){
        if (clickCount == 0){
            dialogueBoxText.text = "Feed me " + Inventory.instance.remaniningFishCount + " fish and I won't consume the whole world... for now.";
            Inventory.instance.fishQuota.SetActive(true);
            clickCount += 1;
        } else if (clickCount == 1){
            if (Inventory.instance.FishTypes.Count <= 0){
                dialogueBoxText.text = "You have no fish... I'll be waiting here for you... ";
            } else {
                Inventory.instance.talkingWithCreatureState = 3;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        progressDialogue = creatureDialogue.GetComponentInChildren<Button>();
        dialogueBoxText = progressDialogue.GetComponentInChildren<TMP_Text>();
    }
}