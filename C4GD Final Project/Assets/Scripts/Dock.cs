using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dock : MonoBehaviour
{
    public GameObject enterOceanPrompt;

    private void OnTriggerEnter2D(Collider2D player){
        enterOceanPrompt.SetActive(true);
    }
}
