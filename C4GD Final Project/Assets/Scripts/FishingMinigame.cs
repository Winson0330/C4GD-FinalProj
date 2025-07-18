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
    public float barSpeed;
    public float fishStack; //spacebar click/hold (right speed)
    public float fishStrength; //how jhard fish is to catch(left speed)
    float randomized;
    float timer=0.5f;
    public float timerLength= .5f;
    public Canvas minigame;
    public GameObject checkBar; //bar that does the main checks
    public GameObject zoneBar; //range where bar increments
    public Fishing fishSystem;
    public CanvasGroup canvase;
    public GameObject barBoundL;
    public GameObject barBoundR;
    public GameObject spaceUp;
    public GameObject spaceDown;

    void Start(){
        fishSystem=GameObject.FindGameObjectWithTag("FishiesMain").GetComponent<Fishing>();
        canvase=GetComponent<CanvasGroup>();
        canvase.alpha=0;
        spaceUp.GetComponent<CanvasGroup>().alpha=1;
        spaceDown.GetComponent<CanvasGroup>().alpha=1;
    }

    void Update()
    {
        timer -= Time.deltaTime;
            if(timer<=0){
                timer=timerLength;
                randomized=Random.Range(0,1f);
            }

        fill.fillAmount=bar/barMax;
        float checkBarPosX=checkBar.transform.localPosition.x;
    if(!minigameCompleted&&fishSystem.canFish&&fishSystem.inFishSpot&&fishSystem.isFishing){
        if(checkBarPosX>=-400&&checkBarPosX<=400&&Input.GetKey(KeyCode.Space)){//space to move bar to right
            
            if(Fishing.EnduranceLevels[Fishing.chosenFish]==1){
                    checkBar.transform.position+=Vector3.right*fishStack*Random.Range(1f,2f);
            }
            if(Fishing.EnduranceLevels[Fishing.chosenFish]==2){
                    checkBar.transform.position+=Vector3.right*fishStack*Random.Range(2f,3f);
            }
            if(Fishing.EnduranceLevels[Fishing.chosenFish]==3){
                    checkBar.transform.position+=Vector3.right*fishStack*Random.Range(3f,4f);
            }
            }
        //print(checkBar.transform.position.x);
        if(checkBarPosX>-400&&checkBarPosX<400){
            if(Fishing.EnduranceLevels[Fishing.chosenFish]==1){
                    checkBar.transform.position-=Vector3.right*fishStrength*Random.Range(1f,2f);
                    enduranceOneBarSize();
            }
            if(Fishing.EnduranceLevels[Fishing.chosenFish]==2){
                    checkBar.transform.position-=Vector3.right*fishStrength*Random.Range(2f,3f);
                    enduranceTwoBarSize();
            }
            if(Fishing.EnduranceLevels[Fishing.chosenFish]==3){
                    checkBar.transform.position-=Vector3.right*fishStrength*Random.Range(3f,4f);
                    enduranceThreeBarSize();
            }
            if(checkBarPosX>=barBoundL.GetComponent<RectTransform>().anchoredPosition.x&&
            checkBarPosX<=barBoundR.GetComponent<RectTransform>().anchoredPosition.x){ //check if in bar range
                bar+=barInc*Time.deltaTime;
            }else{
                bar-=Time.deltaTime*barDec;
            }
        }
        if(barBoundL.GetComponent<RectTransform>().anchoredPosition.x>=-400&&
        barBoundR.GetComponent<RectTransform>().anchoredPosition.x<=400){

            barSpeed=Random.Range(1f,3f);
            if(randomized>.5f&&barBoundR.GetComponent<RectTransform>().anchoredPosition.x<396){
                print("move right");
                float currzoneBarX=zoneBar.GetComponent<RectTransform>().anchoredPosition.x;
                float currzoneBarY=zoneBar.GetComponent<RectTransform>().anchoredPosition.y;
                currzoneBarX+=1f*barSpeed;
                zoneBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(currzoneBarX,currzoneBarY);
            }else if(barBoundL.GetComponent<RectTransform>().anchoredPosition.x>-396){
                print("move left");
                float currzoneBarX=zoneBar.GetComponent<RectTransform>().anchoredPosition.x;
                float currzoneBarY=zoneBar.GetComponent<RectTransform>().anchoredPosition.y;
                currzoneBarX-=1f*barSpeed;
                zoneBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(currzoneBarX,currzoneBarY);
            }
        }
        if(!Input.GetKey(KeyCode.Space)){
            spaceDown.GetComponent<CanvasGroup>().alpha=0;
            spaceUp.GetComponent<CanvasGroup>().alpha=1;
        } else 
        {
            spaceUp.GetComponent<CanvasGroup>().alpha=0;
            spaceDown.GetComponent<CanvasGroup>().alpha=1;
    }
        float checkBarPosY=checkBar.transform.localPosition.y;
        if(checkBarPosX>=400) checkBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(399,checkBarPosY);
        if(checkBarPosX<=-400) checkBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(-399,checkBarPosY);
        if(zoneBar.GetComponent<RectTransform>().anchoredPosition.x>=399) zoneBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(398,zoneBar.GetComponent<RectTransform>().anchoredPosition.y);
        if(zoneBar.GetComponent<RectTransform>().anchoredPosition.x<=-399) zoneBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(-398,zoneBar.GetComponent<RectTransform>().anchoredPosition.y);
    }
        if(bar<=0||bar>=100){
            minigameCompleted=true;
            zoneBar.GetComponent<RectTransform>().anchoredPosition=new Vector2(0,-180);
        }
        if(bar<=0) minigameFailure=true;
        if(bar>=100) minigameSuccess=true;
    }

    public void enduranceOneBarSize(){
        Vector2 currSize=zoneBar.GetComponent<RectTransform>().sizeDelta;
        currSize.x=250+(250/2);
        zoneBar.GetComponent<RectTransform>().sizeDelta=currSize;
        float halfX=(zoneBar.GetComponent<RectTransform>().sizeDelta.x)/2f;
        float zoneBarLPosX=barBoundL.GetComponent<RectTransform>().anchoredPosition.x;
        float zoneBarRPosX=barBoundR.GetComponent<RectTransform>().anchoredPosition.x;
        zoneBarLPosX=-halfX+zoneBar.GetComponent<RectTransform>().anchoredPosition.x;
        barBoundL.GetComponent<RectTransform>().anchoredPosition = new Vector2(zoneBarLPosX,0);
        zoneBarRPosX=halfX+zoneBar.GetComponent<RectTransform>().anchoredPosition.x;
        barBoundR.GetComponent<RectTransform>().anchoredPosition = new Vector2(zoneBarRPosX,0);
    }
    public void enduranceTwoBarSize(){
        Vector2 currSize=zoneBar.GetComponent<RectTransform>().sizeDelta;
        currSize.x=250;
        zoneBar.GetComponent<RectTransform>().sizeDelta=currSize;
        float halfX=(zoneBar.GetComponent<RectTransform>().sizeDelta.x)/2f;
        float zoneBarLPosX=barBoundL.GetComponent<RectTransform>().anchoredPosition.x;
        float zoneBarRPosX=barBoundR.GetComponent<RectTransform>().anchoredPosition.x;
        zoneBarLPosX=-halfX+zoneBar.GetComponent<RectTransform>().anchoredPosition.x;
        barBoundL.GetComponent<RectTransform>().anchoredPosition = new Vector2(zoneBarLPosX,0);
        zoneBarRPosX=halfX+zoneBar.GetComponent<RectTransform>().anchoredPosition.x;
        barBoundR.GetComponent<RectTransform>().anchoredPosition = new Vector2(zoneBarRPosX,0);
    }
    public void enduranceThreeBarSize(){
        Vector2 currSize=zoneBar.GetComponent<RectTransform>().sizeDelta;
        currSize.x=250/2;
        zoneBar.GetComponent<RectTransform>().sizeDelta=currSize;
        float halfX=(zoneBar.GetComponent<RectTransform>().sizeDelta.x)/2f;
        float zoneBarLPosX=barBoundL.GetComponent<RectTransform>().anchoredPosition.x;
        float zoneBarRPosX=barBoundR.GetComponent<RectTransform>().anchoredPosition.x;
        zoneBarLPosX=-halfX+zoneBar.GetComponent<RectTransform>().anchoredPosition.x;
        barBoundL.GetComponent<RectTransform>().anchoredPosition = new Vector2(zoneBarLPosX,0);
        zoneBarRPosX=halfX+zoneBar.GetComponent<RectTransform>().anchoredPosition.x;
        barBoundR.GetComponent<RectTransform>().anchoredPosition = new Vector2(zoneBarRPosX,0);
    }
}