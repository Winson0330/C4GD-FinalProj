using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public GameObject TimeTracker;
    public Sprite SunBG;
    public Sprite MoonBG;
    int hours = 7;
    int minutes = 0;
    float timer = 0;
    public float days=0;
    public bool isnextDay;
    private static bool gameObjectExists = false;
    private static bool timeTrackerExists = false;
    public float intensityReductionFactor=0.5f;
    private static float intensitySave=1f;
    public Light2D sceneLight;
    public Light2D playerLight;
    public GameObject player;
    public AudioSource dayMusic;
    public AudioSource nightMusic;
    public AudioSource oceanStuff;

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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sceneLight = GameObject.FindWithTag("GlobalLight")?.GetComponent<Light2D>();
        playerLight=GameObject.FindWithTag("PlayerLight")?.GetComponent<Light2D>();
        if(sceneLight!=null){
            sceneLight.intensity=intensitySave;
        }
        if(playerLight!=null){
            playerLight.transform.localPosition=Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (SceneManager.GetActiveScene().name == "Ocean"||SceneManager.GetActiveScene().name == "Main Island"){
            TMP_Text timeText = TimeTracker.GetComponentInChildren<TMP_Text>();
            if (timer >= 1f){
                minutes += 12;
                timer -= 1f;
                if (minutes == 60){
                    hours += 1;
                    minutes = 0;
                }
                if (hours == 24){
                    hours = 0;
                }
            }
            timeText.text = string.Format("{0:00}:{1:00}", hours, minutes);
        }
        if(hours==6&&minutes==0) isnextDay=true;
        if (hours <= 6 || hours >= 19){
            gameObject.GetComponentInChildren<Image>().sprite = MoonBG;
            if(isnextDay){
            days+=(int) 1*Time.deltaTime;
            isnextDay=false;
            }
            dayMusic.Stop();
            if(nightMusic.time==0){
            nightMusic.Play();
            oceanStuff.Play();
            }

            if (sceneLight!=null&&sceneLight.intensity > 0.05f)
            {
                sceneLight.intensity -= intensityReductionFactor * Time.deltaTime;
                if (sceneLight.intensity < 0.05f)
                {
                    sceneLight.intensity = 0.05f;
                }
            }
            intensitySave=sceneLight.intensity;
        } else {
            gameObject.GetComponentInChildren<Image>().sprite = SunBG;
            if(dayMusic.time==0){
            dayMusic.Play();
            oceanStuff.Play();
            }

            nightMusic.Stop();
                sceneLight.intensity += intensityReductionFactor * Time.deltaTime;
                if (sceneLight.intensity > 1f)
                {
                    sceneLight.intensity = 1f;
                }
                intensitySave=sceneLight.intensity;
            }
        }
    }
