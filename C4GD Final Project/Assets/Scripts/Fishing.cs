using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Fishing : MonoBehaviour
{
    Animator anim;
    public TMPro.TMP_Text resultText;
    public string[] FishTypes;
    public static int[] EnduranceLevels;
    public bool canFish = true;
    public bool inFishSpot = false;
    public bool isFishing;
    float fishRate = 1; //these two are for how fast the player can fish
    float canFishTimer = 1;
    float stayOnScreen = 3; //these two are for how long the result stays on screen
    float displayTimer = 3;
    public FishingMinigame minigameRef;
    public PlayerController player;
    public bool pickedFish;
    bool fishAnim;
    public static int chosenFish;
    public GameObject fUp;
    public GameObject fDown;
    public AudioSource catchAudio;

    void Start(){
        minigameRef= GameObject.FindGameObjectWithTag("FishConnect").GetComponent<FishingMinigame>();
        player=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        FishTypes = new string[] {"Tuna", "Red Snapper", "Perch", "Salmon"};
        EnduranceLevels = new int[] {3,2,1,1};
        chosenFish = Random.Range(0, FishTypes.Length);
        fUp.GetComponent<CanvasGroup>().alpha=1;
        fDown.GetComponent<CanvasGroup>().alpha=0;
        anim=player.GetComponent<Animator>();
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

        if (Input.GetKeyDown(KeyCode.F) && inFishSpot && canFish&&!fishAnim){
            float timer=0.4f;
            timer-=Time.deltaTime;

            isFishing=true;
            minigameRef.canvase.alpha=1;
            player.moveSpeed=0;

            anim.SetBool("startFishing", true); //play fishengage
            anim.SetBool("Fishing", true);

            if(timer>0){
                fUp.GetComponent<CanvasGroup>().alpha=0;
                fDown.GetComponent<CanvasGroup>().alpha=1;
            }else{
                fDown.GetComponent<CanvasGroup>().alpha=0;
            }
            async void moveToFishIdleAnim(){
                await Task.Delay(300);
                anim.SetBool("startFishing", false);
            }
            moveToFishIdleAnim();
            fishAnim=true;
        }

        if(minigameRef.minigameCompleted){
            
            if(minigameRef.minigameSuccess){
                resultText.text = "You fished a " + FishTypes[chosenFish] + "!";
                Inventory.instance.newCatches.Add(FishTypes[chosenFish]);
                anim.SetBool("fishSuccess", true);
            } else if(minigameRef.minigameFailure){
                resultText.text = "The " + FishTypes[chosenFish] + " got away...";
            }
            async void moveToIdleAnim(){
                await Task.Delay(50);
                anim.SetBool("fishSuccess", false);
                anim.SetBool("Fishing", false);
            }
            moveToIdleAnim();
            fishAnim = false;
            player.moveSpeed = 10;
            fUp.GetComponent<CanvasGroup>().alpha = 1;
            fDown.GetComponent<CanvasGroup>().alpha = 0;
            catchAudio.Play();
            
            minigameRef.minigameCompleted=false;
            minigameRef.minigameSuccess=false;
            minigameRef.minigameFailure=false;
            minigameRef.canvase.alpha=0;
            minigameRef.bar=40;
            
            isFishing=false;
            canFish = false;
            chosenFish = Random.Range(0, FishTypes.Length);
        }
            
    }

    private void OnTriggerStay2D(Collider2D player){
        inFishSpot = true;
    }

    private void OnTriggerExit2D(Collider2D player){
        inFishSpot = false;
    }
}