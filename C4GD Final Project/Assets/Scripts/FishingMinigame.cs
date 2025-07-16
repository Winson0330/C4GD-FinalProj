using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingMinigame : MonoBehaviour
{
    public Image fill;
    public bool minigameCompleted;
    public bool minigameSuccess;//defaulted false
    public bool minigameFailure;
    public float bar=40;
    public float barMax=100;
    public float barDec=10;
    public float barInc=10;
    public float fishStack=20; //spacebar click/hold (right speed)
    public float fishStrength=1; //how jhard fish is to catch(left speed)
    public Canvas minigame;
    public GameObject checkBar; //bar that does the main checks
    public GameObject zoneBar; //range where bar increments
    public Fishing fishSystem;
    public CanvasGroup canvase;

    void Start(){
        fishSystem=GameObject.FindGameObjectWithTag("FishiesMain").GetComponent<Fishing>();
        canvase=GetComponent<CanvasGroup>();
        canvase.alpha=0;
    }

    void Update()
    {
        print(bar);
        fill.fillAmount=bar/barMax;
        float checkBarPosX=checkBar.transform.localPosition.x;
    if(!minigameCompleted&&fishSystem.canFish&&fishSystem.inFishSpot&&fishSystem.isFishing){
        if(checkBarPosX>=-400&&checkBarPosX<=400&&Input.GetKey(KeyCode.Space)){//space to move bar to right
                checkBar.transform.position+=Vector3.right*fishStack;
            }
        //print(checkBar.transform.position.x);
        if(checkBarPosX>-400&&checkBarPosX<400){
            checkBar.transform.position-=Vector3.right*fishStrength;
            if(checkBarPosX>=zoneBar.transform.GetChild(0).localPosition.x&&
            checkBarPosX<=zoneBar.transform.GetChild(1).localPosition.x){ //check if in bar range
                bar+=barInc*Time.deltaTime;
            }else{
                bar-=Time.deltaTime*barDec;
            }
        }
        float checkBarPosY=checkBar.transform.localPosition.y;
        if(checkBarPosX>=400) checkBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(399,checkBarPosY);
        if(checkBarPosX<=-400) checkBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(-399,checkBarPosY);
    }
        if(bar<=0||bar>=100) minigameCompleted=true;
        if(bar<=0) minigameFailure=true;
        if(bar>=100) minigameSuccess=true;
    }
}