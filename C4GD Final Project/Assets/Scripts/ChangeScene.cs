using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public GameObject changeScenePrompt;

    private void OnTriggerEnter2D(Collider2D player){
        if (player.gameObject.CompareTag("Player")){
            changeScenePrompt.SetActive(true);
        }
    }
}