using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCode : MonoBehaviour {
    private float moveX = 0f, moveY = 0f;

    public static int playerRow, playerCol;

    private int health;
    public static bool isDeath;
    private GameObject txt;
    private GameObject redScreen;

    private GameObject myCanvas;

    //private Animator animator;

    // Use this for initialization
    void Start () {
        //animator = GetComponent<Animator>();
        isDeath = false;
        health = 100;
        txt = GameObject.FindGameObjectWithTag("Txt0");
        myCanvas = GameObject.Find("Health");
        redScreen = GameObject.FindGameObjectWithTag("RedScreen");

        playerRow = Mathf.RoundToInt(transform.position.x);
        playerCol = Mathf.RoundToInt(transform.position.z);
        
        //Debug.Log(playerX + "dan y : " + playerY);
        DisplayMap.map.coor[playerRow, playerCol].value = DesignController.PLAYER;
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag.Equals("TreasureTag"))
        {
            Debug.Log("TERTABRAK BUNG");
            this.health += 50;
            Destroy(c.gameObject);
        }
    }

    // Update is called once per frame

    public void Damage()
    {
        health = health-10 > 0 ? health - 10 : 0;
        redScreen.GetComponent<Image>().enabled = true;
        StartCoroutine(delayRedScreen());
    }

    void Update()
    {
        DisplayMap.map.coor[playerRow, playerCol].value = DesignController.FLOOR;
        playerRow = Mathf.RoundToInt(transform.position.x);
        playerCol = Mathf.RoundToInt(transform.position.z);
        
        //Debug.Log(playerX + "dan y : " + playerY);
        DisplayMap.map.coor[playerRow, playerCol].value = DesignController.PLAYER;
        //Debug.Log(DisplayMap.map.coor[2, 9].value);
        //Map.getSpot(playerY, playerX)= DesignController.PLAYER;

        UnityEngine.UI.Text change = txt.GetComponent<UnityEngine.UI.Text>();
        change.text = "Health : " + health;

        if (health <= 0) isDeath = true;


        moveY += 2 * Input.GetAxis("Mouse X");
        moveX -= 2 * Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(moveX, moveY, 0);

        if (Input.GetKey(KeyCode.W))
        {
            //animator.SetBool("isWalk", true);
            if (Input.GetKey(KeyCode.LeftShift)) transform.Translate(new Vector3(0, 0, Time.deltaTime * 2f));
            transform.Translate(new Vector3(0, 0, Time.deltaTime * 4f));
        }
        if (Input.GetKey(KeyCode.A))
        {
            //animator.SetBool("isWalk", true);
            if (Input.GetKey(KeyCode.LeftShift)) transform.Translate(new Vector3(Time.deltaTime * -2f, 0, 0));
            transform.Translate(new Vector3(Time.deltaTime * -4f, 0, 0));
        }
        if (Input.GetKey(KeyCode.S))
        {
            //animator.SetBool("isWalk", true);
            if (Input.GetKey(KeyCode.LeftShift)) transform.Translate(new Vector3(0, 0, Time.deltaTime * -2f));
            transform.Translate(new Vector3(0, 0, Time.deltaTime * -4f));
        }
        if (Input.GetKey(KeyCode.D))
        {
            //animator.SetBool("isWalk", true);
            if (Input.GetKey(KeyCode.LeftShift)) transform.Translate(new Vector3(Time.deltaTime * 2f, 0, 0));
            transform.Translate(new Vector3(Time.deltaTime * 4f, 0, 0));
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            //animator.SetBool("isWalk", false);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(new Vector3(0, Time.deltaTime * 4f, 0));
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            DisplayMap.removeEnemy(DisplayMap.xRand, DisplayMap.yRand);
            DisplayMap.removeTreasure(DisplayMap.xTRand, DisplayMap.yTRand);
            MapStatus.getInstance().setToPlay(false);
            if (MapStatus.getInstance().getChoosenMap() == 1) MapStatus.getInstance().setChoosenMap(0);
            TxtController.getInstance().updateText();
            SceneManager.LoadScene("MenuScene");
        }

        

    }

    IEnumerator delayRedScreen()
    {
        yield return new WaitForSeconds(0.15f);
        redScreen.GetComponent<Image>().enabled = false;
    }

}
