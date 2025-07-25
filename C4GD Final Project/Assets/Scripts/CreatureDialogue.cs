using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreatureDialogue : MonoBehaviour
{
    public static CreatureDialogue instance;
    public GameObject dialogueTrigger;
    public GameObject creatureDialogue;
    Button progressDialogue;
    TMP_Text dialogueBoxText;
    int clickCount = 0;
    float curDay;
    public bool talkedOnThisDay = false;
    public GameObject seaCreature;
    public TimeManager time;
    public Inventory inventory;

    void Awake(){
        // GameObject[] persist = new GameObject[] {creatureDialogue, Inventory.instance.fishQuota, gameObject};
        // foreach (GameObject objectToPersist in persist){
        //     DontDestroyOnLoad(objectToPersist);
        // }
        GameObject[] persist = new GameObject[] {creatureDialogue, Inventory.instance.fishQuota, gameObject, seaCreature};
        if(instance == null){
            instance = this;
            // foreach (GameObject objectToPersist in persist){
            //     DontDestroyOnLoad(objectToPersist);
            // }

            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(creatureDialogue);
            DontDestroyOnLoad(seaCreature);
        }
        else {
            Destroy(creatureDialogue);
            Destroy(gameObject);
        }

    }

    void Start()
    {
        progressDialogue = creatureDialogue.GetComponentInChildren<Button>();
        dialogueBoxText = progressDialogue.GetComponentInChildren<TMP_Text>();
        time=GameObject.FindObjectOfType<TimeManager>();
        inventory=GameObject.FindObjectOfType<Inventory>();
        curDay=time.days;
        talkedOnThisDay=false;
    }
    
    void Update(){
        if(curDay!=time.days){
            inventory.requiredFishCount+=(int) (64f*Time.deltaTime);
            inventory.remaniningFishCount=inventory.requiredFishCount;
            curDay=time.days;
            talkedOnThisDay=false;
            seaCreature = GameObject.Find("Sea Creature");
            if(seaCreature.GetComponent<SpriteRenderer>().color.a<=255){
                Color color=seaCreature.GetComponent<SpriteRenderer>().color;
                color.a+=25*Time.deltaTime;
                seaCreature.GetComponent<SpriteRenderer>().color=color;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D player){
        if (player.gameObject.CompareTag("Player")){
            Inventory.instance.talkingWithCreatureState = 1;
            //Creature Dialogue
            //creatureDialogue = GameObject.Find("Creature Dialogue");
            creatureDialogue.SetActive(true);
            if (!talkedOnThisDay){
                dialogueBoxText.text = "I must be satisfied once more...";
                talkedOnThisDay = true;
            } else if (Inventory.instance.remaniningFishCount <= 0){
                dialogueBoxText.text = "You have fed me well... Your kind has been spared for another day...";
            } else {
                dialogueBoxText.text = "Feed me " + Inventory.instance.remaniningFishCount + " more fish and I won't consume the whole world... for now.";
            }
        }
    }

    void OnTriggerExit2D(Collider2D player){
        if (player.gameObject.CompareTag("Player")){
            //creatureDialogue = GameObject.Find("Creature Dialogue");
            creatureDialogue.SetActive(false);
            Inventory.instance.talkingWithCreatureState = 2;
            Inventory.instance.justFinishedTalking = true;
            clickCount = 1;
        }
    }

    public void onClick(){
        if (Inventory.instance.remaniningFishCount > 0){
            Inventory.instance.fishQuota.SetActive(true);
            if (clickCount == 0){
                dialogueBoxText.text = "Feed me " + Inventory.instance.remaniningFishCount + " fish and I won't consume the whole world... for now.";
                clickCount += 1;
            } else if (clickCount == 1){
                if (Inventory.instance.FishTypes.Count <= 0){
                    dialogueBoxText.text = "You have no fish... I'll be waiting here for you... ";
                } else {
                    Inventory.instance.talkingWithCreatureState = 3;
                }
            }
        } else {
            dialogueBoxText.text = "I will be here tomorrow...";
            seaCreature = GameObject.Find("Sea Creature");
            if(seaCreature.GetComponent<SpriteRenderer>().color.a>0){
                Color color=seaCreature.GetComponent<SpriteRenderer>().color;
                color.a-=25*Time.deltaTime;
                seaCreature.GetComponent<SpriteRenderer>().color=color;
            }
        }
    }
}