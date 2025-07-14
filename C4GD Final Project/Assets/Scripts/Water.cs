using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public Fishing fishingRod;

    private void OnTriggerStay2D(Collider2D player){
        fishingRod.inWater = true;
    }

    private void OnTriggerExit2D(Collider2D player){
        fishingRod.inWater = false;
    }
}
