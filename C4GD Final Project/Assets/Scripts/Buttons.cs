using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Buttons : MonoBehaviour
{
    public Button promptButton;
    public GameObject changeScenePrompt;
    
    public void onClick(){
        TMP_Text promptText = promptButton.GetComponentInChildren<TMP_Text>();
        if (promptText.text == "Yes"){
            if (PlayerController.instance.onIsland){
                GameManager.instance.LoadScene(GameManager.instance.ocean);
                PlayerController.instance.onIsland = false;
            } else {
                GameManager.instance.LoadScene(GameManager.instance.mainIsland);
                PlayerController.instance.onIsland = true;
            }
        } else {
            changeScenePrompt.SetActive(false);
        }
    }
}
