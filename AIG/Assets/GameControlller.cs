using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControlller : MonoBehaviour {
    MapStatus ms = MapStatus.getInstance();
    private float timer = 0;
    private float lastTimer = 0;
    private bool isGameFinish = false;


    private GameObject blackScreen;
    private GameObject status;


    // Use this for initialization
    void Start () {
        blackScreen = GameObject.FindGameObjectWithTag("BlackScreen");
        status = GameObject.FindGameObjectWithTag("Status");
        status.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (Enemy.count <= 0 && isGameFinish == false)
        {
            //Debug.Log("Count = 0 " + Enemy.count);
            int temp = (ms.getChoosenMap() == 1) ? 0 : ms.getChoosenMap();
            Debug.Log("Temp: " + temp);
            ms.setChoosenMap(temp);
            TxtController.getInstance().updateText();
            timer = Time.time;
            isGameFinish = true;
            //Debug.Log(timer);
        }

        if (PlayerCode.isDeath == true && isGameFinish == false)
        {
            //Debug.Log("Count = 0 " + Enemy.count);
            int temp = (ms.getChoosenMap() == 1) ? 0 : ms.getChoosenMap();
            Debug.Log("Temp: " + temp);
            ms.setChoosenMap(temp);
            TxtController.getInstance().updateText();
            timer = Time.time;
            isGameFinish = true;
            //Debug.Log(timer);
        }

        if (isGameFinish)
        {
            lastTimer = Time.time;

            blackScreen.GetComponent<Image>().enabled = true;
            
            status.SetActive(true);

            UnityEngine.UI.Text textStatus = status.GetComponent<UnityEngine.UI.Text>();
            textStatus.text = Enemy.count <= 0 ? "You Win" : "You Lose";
            //Debug.Log("Last Timer : " + lastTimer + "timer : " + timer);
            //Debug.Log(lastTimer - timer);
        }

        if((lastTimer - timer) >= 3f && isGameFinish == true)
        {
            DisplayMap.removeEnemy(DisplayMap.xRand, DisplayMap.yRand);
            ms.setToPlay(false);
            SceneManager.LoadScene("MenuScene");
        }
	}
}
