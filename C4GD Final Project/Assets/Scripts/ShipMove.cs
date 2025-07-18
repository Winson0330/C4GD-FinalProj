using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMove : MonoBehaviour
{
    public float rotationAmt=0;
    public float rotationAmtTop=0;
    public GameObject hull;
    public GameObject flagPole;

    // Update is called once per frame
    void Update()
    {
        hull.transform.localEulerAngles=new Vector3(0,0,Mathf.Sin(Time.time*rotationAmt)*2.5f);
        flagPole.transform.localEulerAngles=new Vector3(0,0,Mathf.Sin(Time.time*rotationAmt)*2f);
    }
}
