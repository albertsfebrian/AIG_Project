using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScript : MonoBehaviour {

    public Asyn loadingAsyn;

    private void changeText()
    {
        //UnityEngine.UI.Text change = txt.GetComponent<UnityEngine.UI.Text>();
        //string percentage = (lastTimer-timer)*20 + " %";
        //if((lastTimer- timer)*20 >=100 ) percentage = 100 + " %";
        //change.text = percentage;
    }

    //private float lastTimer = 0;
    //private float timer = 0;


    //private void changeText()
    //{
    //    UnityEngine.UI.Text change = txt.GetComponent<UnityEngine.UI.Text>();
    //    //string percentage = (lastTimer-timer)*20 + " %";
    //    //if((lastTimer- timer)*20 >=100 ) percentage = 100 + " %";
    //    //change.text = percentage;
    //}
    // Use this for initialization
    void Start () {
        //timer = Time.time;
        //lastTimer = Time.time;
        //txt = GameObject.FindGameObjectWithTag("Txt0");
        if (MapStatus.getInstance().getIndex() == 2) StartCoroutine(loadingAsyn.Loading(2)); //SceneManager.LoadScene("DesignMap"); 
        if (MapStatus.getInstance().getIndex() == 3)
            StartCoroutine(loadingAsyn.Loading(0)); //SceneManager.LoadScene("MenuScene");
        if (MapStatus.getInstance().getIndex() == 1)
            StartCoroutine(loadingAsyn.Loading(1)); //SceneManager.LoadScene("GameScene");
    }
	
	// Update is called once per frame
	void Update () {
        //lastTimer = Time.time;
        //changeText();

        //if (lastTimer-timer >= 5f)
        //{
            //if (MapStatus.getInstance().getIndex() == 2 ) StartCoroutine(loadingAsyn.Loading(2)); //SceneManager.LoadScene("DesignMap"); 
            //if (MapStatus.getInstance().getIndex() == 3 )
            //    StartCoroutine(loadingAsyn.Loading(0)); //SceneManager.LoadScene("MenuScene");
            //if (MapStatus.getInstance().getIndex() == 1 )
            //    StartCoroutine(loadingAsyn.Loading(1)); //SceneManager.LoadScene("GameScene");   
        //}
    }

    
}
