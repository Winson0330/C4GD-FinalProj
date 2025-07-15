using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public string mainIsland = "Main Island";
    public string ocean = "Ocean";

    public void LoadScene(string name){
        SceneManager.LoadScene(name);
    }

    // Start is called before the first frame update
    void Awake(){
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
}
