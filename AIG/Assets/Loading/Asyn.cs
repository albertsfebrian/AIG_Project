using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Asyn : MonoBehaviour {

    public Text txt;
    public GameObject loadingBar; 
 


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator Loading(int sceneIndex)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneIndex);

        loadingBar.SetActive(true);
        while (!op.isDone)
        {
            float pencentage = Mathf.Clamp01(op.progress / .9f);
            txt.text = pencentage * 100f + "%";
            yield return null;
        }
    }

}
