using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public GameObject TimeTracker;
    int hours = 0;
    int minutes = 0;
    float timer = 0;
    private static bool gameObjectExists = false;
    private static bool timeTrackerExists = false;

    void Awake(){
        if (gameObjectExists) {
            Destroy(gameObject);
        } else {
            gameObjectExists = true;
            DontDestroyOnLoad(gameObject);
        }

        if (TimeTracker != null) {
            if (timeTrackerExists) {
                Destroy(TimeTracker);
            } else {
                timeTrackerExists = true;
                DontDestroyOnLoad(TimeTracker);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (SceneManager.GetActiveScene().name == "Ocean"){
            TMP_Text timeText = TimeTracker.GetComponentInChildren<TMP_Text>();
            if (timer >= 1f){
                minutes += 12;
                timer -= 1f;
                if (minutes == 60){
                    hours += 1;
                    minutes = 0;
                }
            }
            timeText.text = string.Format("{0:00}:{1:00}", hours, minutes);
        }
    }
}
