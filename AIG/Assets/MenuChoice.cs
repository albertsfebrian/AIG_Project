using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuChoice : MonoBehaviour {

    class Indexing
    {
        public int min;
        public int max;

        public Indexing(int min,int max)
        {
            this.min = min;
            this.max = max;
        }
    }
    class C_Resolution {
        public int width;
        public int height;
        public string text;

        public C_Resolution(int width, int height, string text)
        {
            this.width = width;
            this.height = height;
            this.text = text;
        }
    }

    private List<GameObject> txts;
    public static int index;
    //private GameObject[] canvas;
    private List<GameObject> boxes;
    private Indexing indexing;
    private List<string> quality;
    private List<C_Resolution> resol;
    private const int MAX_Q = 5, MIN_Q = 1, MIN_R = 0;
    private int MAX_R = -1;
    private int idxQ = 0, idxR = 0;
    private const int MR=90;
    private int[] rotation = { MR, MR, MR, MR, MR };
    private bool isRotate = false;
    private MapStatus ms = MapStatus.getInstance();

    private void changeColor(int indexToChange,int indexBefore)
    {
        UnityEngine.UI.Text changeBefore = txts[indexBefore].GetComponent<UnityEngine.UI.Text>();
        changeBefore.color = Color.black;
        UnityEngine.UI.Text change = txts[indexToChange].GetComponent<UnityEngine.UI.Text>();
        change.color = Color.blue;
    }
    
    private void changeText(int indexToChange,string textToWrite)
    {
        UnityEngine.UI.Text change = txts[indexToChange].GetComponent<UnityEngine.UI.Text>();
        change.text = textToWrite;
    }

    private int minIndex(int idx)
    {
        idx--;
        if (idx < indexing.min) idx = indexing.max;
        return idx;
    }

    private int plusIndex(int idx)
    {
        idx++;
        if (idx > indexing.max) idx = indexing.min;
        return idx;
    }

    private void minQuality()
    {
        idxQ--;
        if (idxQ < MIN_Q) idxQ = MAX_Q;
    }

    private void plusQuality()
    {
        idxQ++;
        if (idxQ > MAX_Q) idxQ = MIN_Q;
    }

    private void minResol()
    {
        idxR--;
        if (idxR < MIN_R) idxR = MAX_R;
    }

    private void plusResol()
    {
        idxR++;
        if (idxR > MAX_R) idxR = MIN_R;
    }


    //private void changeCanvasTo(int index)
    //{
    //    int toHide = (index == 0) ? 1 : 0;
    //    canvas[index].SetActive(true);
    //    canvas[toHide].SetActive(false);
    //}

    private void rotateBox(int index,int boxIndex)
    {
        isRotate = true;

        if(index< MR-(0.1*MR))
        {
            if (rotation[boxIndex] > 0)
            {
                boxes[boxIndex].transform.Rotate(0, 2f, 0);
                rotation[boxIndex]--;
            }
        }
    }

    private void resetFinish()
    {
        for (int i = 0; i < 5; i++)
        {
            rotation[i] = MR;
        }
    }

    private void rotateBoxes()
    {
        rotateBox(0, 0);
        rotateBox(rotation[0], 1);
        rotateBox(rotation[1], 2);
        rotateBox(rotation[2], 3);
        rotateBox(rotation[3], 4);

        if (rotation[0] == 0 && rotation[1] == 0 && rotation[2] == 0 && rotation[3] == 0 && rotation[4] == 0)
            isRotate = false;
    }

    private void checkText()
    {
        //Debug.Log(ms.getStatus());
        for (int i = 1; i <= 3; i++)
        {
            //Debug.Log(ms.isAvailable(i));
            if (ms.isAvailable(i)) {
                int temp = (i != 2 && i == 3) ? 1 : 3;
                if (i == 2) temp = 2;
                UnityEngine.UI.Text changes = txts[temp].GetComponent<UnityEngine.UI.Text>();
                changes.text = "Map " + i;
            }
        }

    }

    void getTextTag() {
        GameObject temp = GameObject.FindGameObjectWithTag("Txt0");
        txts.Add(temp);
        temp = GameObject.FindGameObjectWithTag("Txt1");
        txts.Add(temp);
        temp = GameObject.FindGameObjectWithTag("Txt2");
        txts.Add(temp);
        temp = GameObject.FindGameObjectWithTag("Txt3");
        txts.Add(temp);
        temp = GameObject.FindGameObjectWithTag("Txt4");
        txts.Add(temp);
        temp = GameObject.FindGameObjectWithTag("Txt5");
        txts.Add(temp);
        temp = GameObject.FindGameObjectWithTag("Txt6");
        txts.Add(temp);
        temp = GameObject.FindGameObjectWithTag("Txt7");
        txts.Add(temp);
        temp = GameObject.FindGameObjectWithTag("Txt8");
        txts.Add(temp);
        temp = GameObject.FindGameObjectWithTag("Txt9");
        txts.Add(temp);
    }
    void getBoxTag() {
        GameObject temp = GameObject.FindGameObjectWithTag("Box1");
        boxes.Add(temp);
        temp = GameObject.FindGameObjectWithTag("Box2");
        boxes.Add(temp);
        temp = GameObject.FindGameObjectWithTag("Box3");
        boxes.Add(temp);
        temp = GameObject.FindGameObjectWithTag("Box4");
        boxes.Add(temp);
        temp = GameObject.FindGameObjectWithTag("Box5");
        boxes.Add(temp);

    }
    void generateString()
    {
        quality.Add("Very Low");
        quality.Add("Low");
        quality.Add("Medium");
        quality.Add("High");
        quality.Add("Very High");
        quality.Add("Ultra");

        //resol.Add(new C_Resolution(640, 480, "640 X 480"));
        //resol.Add(new C_Resolution(720, 480, "720 X 480"));
        //resol.Add(new C_Resolution(720, 576, "720 X 576"));
        //resol.Add(new C_Resolution(800, 600, "800 X 600"));
        //resol.Add(new C_Resolution(1024, 768, "1024 X 768"));
        //resol.Add(new C_Resolution(1152, 648, "1152 X 648"));
        //resol.Add(new C_Resolution(1280, 720, "1280 X 720"));
        //resol.Add(new C_Resolution(1280, 1024, "1280 X 1024"));
        //resol.Add(new C_Resolution(1366, 768, "1366 X 768"));
        //resol.Add(new C_Resolution(1600, 900, "1600 X 900"));
        //resol.Add(new C_Resolution(1680, 1050, "1680 X 1050"));
        //resol.Add(new C_Resolution(1776, 1000, "1776 X 1000"));
        //resol.Add(new C_Resolution(1920, 1080, "1920 X 1080"));
        temp();
    }

    void temp()
    {
        Resolution[] resolut = Screen.resolutions;

        foreach (var res in resolut)
        {
            string _string = res.width + " X " + res.height;
            resol.Add(new C_Resolution(res.width, res.height, _string));
            MAX_R++;
            //Debug.Log("W:" + res.width + "H:" + res.height);
        }
    }

   


    // Use this for initialization
    void Start()
    {
        Debug.Log(ms.getStatus());
        quality = new List<string>();

        resol = new List<C_Resolution>();
        generateString();
        //canvas = GameObject.FindGameObjectsWithTag("Canvas");
        txts = new List<GameObject>();
        //txts = GameObject.FindGameObjectsWithTag("TextTag");
        getTextTag();
        boxes = new List<GameObject>();
        //boxes = GameObject.FindGameObjectsWithTag("BoxTag");
        getBoxTag();
        //changeCanvasTo(1);
        indexing = new Indexing(5, 9);
        index = 9;
        changeColor(index, index);
        //ms.setStatus(1);
        //ms.setStatus(2);
        //ms.setStatus(3);
        //Debug.Log(ms.getStatus());
    }
	
	// Update is called once per frame
	void Update () {
        if(isRotate== true)
        {
            checkText();
            rotateBoxes();
        }

		if(Input.GetKeyDown(KeyCode.UpArrow) && !isRotate)
        {
          //  Debug.Log("Key Up " + index);
            index = plusIndex(index);
          //  Debug.Log("Update : "+ index);
            changeColor(index, minIndex(index));
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && !isRotate)
        {
           // Debug.Log("Key Up " + index);
            index = minIndex(index);
           // Debug.Log("Update : " + index);
            changeColor(index, plusIndex(index));
        }

        if (Input.GetKeyDown(KeyCode.Return) && !isRotate)
        {
            if (index == 9)
            {
                // play 
                index = 4;
                changeColor(index, 9);
                resetFinish();
                isRotate = true;
                //changeCanvasTo(0);
                indexing = new Indexing(0, 4);
            }

            else if (index == 8)
            {
                // design map
                ms.setIndex(2);
                SceneManager.LoadScene("LoadingScreen");
            }

            else if (index == 5)
            {
                //exit
                Debug.Log("Exit");
                Application.Quit();
            }

            else if (index == 4)
            {
                //play game
                ms.setChoosenMap(1);
                TxtController.getInstance().updateText();
                ms.setIndex(1);
                SceneManager.LoadScene("LoadingScreen");
            }

            else if (index == 3 || index == 2 || index == 1)
            {
                //play game
                
                int temp = (index != 2 && index == 3) ? 1 : 3;
                if (index == 2) temp = 2;
                //Debug.Log(temp);
                //Debug.Log(ms.getStatus());
                //Debug.Log(ms.isAvailable(temp));
                if (ms.isAvailable(temp))
                {
                    ms.setToPlay(true);
                    ms.setChoosenMap(temp + 1);
                    TxtController.getInstance().updateText();
                    ms.setIndex(1);
                    SceneManager.LoadScene("LoadingScreen");
                }
                
            }

            else if (index == 0)
            {
                //back 
                index = 9;
                changeColor(index, 0);
                resetFinish();
                isRotate = true;
                //changeCanvasTo(1);
                indexing = new Indexing(5, 9);
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //Debug.Log("Right");
            if (index == 7)
            {
                //resol
                plusResol();
                //Debug.Log(idxR);
                Screen.SetResolution(resol[idxR].width, resol[idxR].height, true);
                changeText(7, resol[idxR].text);
            }

            else if (index == 6)
            {
                //quality
                plusQuality();
                QualitySettings.SetQualityLevel(idxQ, true);
                changeText(6, quality[idxQ]);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //Debug.Log("Left");
            if (index == 7)
            {
                //change resol
                minResol();
                Screen.SetResolution(resol[idxR].width, resol[idxR].height, true);
                //Debug.Log(idxR);
                changeText(7, resol[idxR].text);
            }

            else if (index == 6)
            {
                minQuality();
                QualitySettings.SetQualityLevel(idxQ, true);
                changeText(6, quality[idxQ]);
            }
        }



    }
}
