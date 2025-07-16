using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreatureDialogue : MonoBehaviour
{
    public GameObject dialogueTrigger;
    public GameObject creatureDialogue;
    public GameObject fishQuota;
    Button progressDialogue;
    TMP_Text dialogueBoxText;
    int requiredFishCount = 5;
    int clickCount = 0;
    bool talkedOnThisDay = false;

    void Awake(){
        GameObject[] persist = new GameObject[] {creatureDialogue, fishQuota, gameObject};
        foreach (GameObject objectToPersist in persist){
            DontDestroyOnLoad(objectToPersist);
        }
    }

    void OnTriggerEnter2D(Collider2D player){
        if (player.gameObject.CompareTag("Player")){
            creatureDialogue.SetActive(true);
            if (!talkedOnThisDay){
                dialogueBoxText.text = "I must be satisfied once more...";
                talkedOnThisDay = true;
            } else {
                dialogueBoxText.text = "Feed me " + requiredFishCount + " fish and I won't consume the whole world... for now.";
            }
        }
    }

    void OnTriggerExit2D(Collider2D player){
        if (player.gameObject.CompareTag("Player")){
            creatureDialogue.SetActive(false);
            Deposit.instance.DepositUI.SetActive(false);
            clickCount = 1;
        }
    }

    public void onClick(){
        if (clickCount == 0){
            dialogueBoxText.text = "Feed me " + requiredFishCount + " fish and I won't consume the whole world... for now.";
            fishQuota.SetActive(true);
            clickCount += 1;
        } else if (clickCount == 1){
            Deposit.instance.DepositUI.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        progressDialogue = creatureDialogue.GetComponentInChildren<Button>();
        dialogueBoxText = progressDialogue.GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
