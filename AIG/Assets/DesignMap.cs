using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DesignMap : MonoBehaviour {
   
    public GameObject wall ;
    public GameObject ground;
    public GameObject canvas;
    private bool isShown = false;
    DesignController dMap = DesignController.getInstance();
    ChoosenDesign cd = ChoosenDesign.getInstance();
    public List<string> mapstr = new List<string>();
    //public int cameraCurrentZoom = 8;
    //public int cameraZoomMax = 20;
    //public int cameraZoomMin = 5;
    public const int OBSTACLE_LION = -2;
    public const int OBSTACLE_BOX = -3;
    public const int OBSTACLE_PLUS = -4;
    public const int WALL = -1;
    public const int FLOOR = 0;
    public const int DOOR = 1;
    public const int ENEMY = 99;
    public Vector2 rotateCam;

    private void showEmptyMap(int height, int width)
    {
        Enemy.count = 0;
        wall = GameObject.FindGameObjectWithTag("WallTag");
        ground = GameObject.FindGameObjectWithTag("GroundTag");
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                var a = GameObject.Instantiate(ground, new Vector3((float)row, 0f, (float)col), Quaternion.identity);
                a.GetComponent<Floor>().row = row;
                a.GetComponent<Floor>().col = col;
                if (dMap.getCoor(row,col) == WALL)
                    GameObject.Instantiate(wall, new Vector3((float)row, 2f, (float)col), Quaternion.identity);
            }
            //Console.WriteLine();
        }
    }

    public void changeToWall() { cd.changeToWall(); }
    public void changeToLion() { cd.changeToLion(); }
    public void changeToCancel() { cd.changeToCancel(); }
    public void changeToBox() { cd.changeToBox(); }

    public static void printDebug()
    {
        string temp = maptoString(1);
        Debug.Log(temp);
    }

    public void convertToString()
    {
        for (int i = 0; i < 21; i++)
        {
            mapstr.Add(maptoString(i));
            Debug.Log(mapstr[i]);
        }
    }

    public void saveMap()
    {
        convertToString();
        CheckMap cm = new CheckMap(dMap.getMap(), 21, 21);
        bool temp = cm.isMapAccepted();
        Debug.Log("Accepted : " + temp);
        printDebug();
        
        if (temp)
        {
            Debug.Log("if  : " + temp);
            printDebug();
            WriteTxt();
            MapStatus.getInstance().setIndex(3);
            SceneManager.LoadScene("LoadingScreen");
            CurcorController.cursorCon = false;
        }
        else
        {
            Debug.Log("else : " + temp);
            isShown = true;
            canvas.SetActive(isShown);
        }

    }

    public static string maptoString(int row)
    {
        string temp = "";
        for (int i = 0; i < 21; i++)
        {
            if (DesignController.getInstance().getCoor(row,i) == WALL)
                temp = temp + "#";
            else if (DesignController.getInstance().getCoor(row, i) == FLOOR || DesignController.getInstance().getCoor(row, i) == DOOR)
                temp = temp + " ";
            else if (DesignController.getInstance().getCoor(row, i) == OBSTACLE_LION)
                temp = temp + "L";
            else if (DesignController.getInstance().getCoor(row, i) == OBSTACLE_BOX)
                temp = temp + "B";
            else if (DesignController.getInstance().getCoor(row, i) == OBSTACLE_PLUS)
                temp = temp + "X";
        }
        return temp;
    }

    public void WriteTxt()
    {
        string path = "";
        string db = "Map";
        if (MapStatus.getInstance().isAvailable(1) == false)
        {
            path = "Assets/Resources/Map1.txt";
            db = db + "1.txt";
            MapStatus.getInstance().setStatus(1);
            MapStatus.getInstance().setChoosenMap(2);
        }
        else if (MapStatus.getInstance().isAvailable(2) == false)
        {
            path = "Assets/Resources/Map2.txt";
            db = db + "2.txt";
            MapStatus.getInstance().setStatus(2);
            MapStatus.getInstance().setChoosenMap(3);
        }
        else if (MapStatus.getInstance().isAvailable(3) == false)
        {
            path = "Assets/Resources/Map3.txt";
            db = db + "3.txt";
            MapStatus.getInstance().setStatus(3);
            MapStatus.getInstance().setChoosenMap(4);
        }
        else {
            path = "Assets/Resources/Map1.txt";
            db = db + "1.txt";
            MapStatus.getInstance().setStatus(1);
            MapStatus.getInstance().setChoosenMap(2);
        }
        TxtController.getInstance().updateText();
        StreamWriter writer = new StreamWriter(path, false);
        for (int i = 0; i < 21; i++)
        {
            writer.WriteLine(mapstr[i]);
        }
        writer.Close();


        //AssetDatabase.ImportAsset(path);
        //TextAsset asset = Resources.Load<TextAsset>(db);
    }

    void zoomMouse()
    {
        float fieldOV = Camera.main.fieldOfView;
        fieldOV -= Input.GetAxis("Mouse ScrollWheel") * 35f;
        fieldOV = Mathf.Clamp(fieldOV, 20, 70);
        Camera.main.fieldOfView = fieldOV;
    }

    void rotateCamera()
    {
        if (Input.GetButton("Fire2"))
        {
            rotateCam += new Vector2(Input.GetAxis("Mouse X") * 5f, Input.GetAxis("Mouse Y") * 5f);
            transform.localRotation = Quaternion.Euler(24, rotateCam.x, 0);
        }
    }



    // Use this for initialization
    void Start () {
        canvas = GameObject.FindGameObjectWithTag("Txt0");
        canvas.SetActive(isShown);
        CurcorController.cursorCon = true;
        dMap.generateMap();
        showEmptyMap(21,21);
        transform.localRotation = Quaternion.Euler(24, rotateCam.x, 0);
        //Camera.main.orthographicSize = cameraCurrentZoom;
    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetMouseButtonUp(0))
        //{

        //    GameObject.Instantiate(wall, new Vector3((float)row, 2f, (float)col), Quaternion.identity);
        //}

        Vector3 position = this.transform.position;
        if (Input.GetKey(KeyCode.W) && transform.position.z < 40)
        { 
            position.z += Time.deltaTime * 4f;
        }
        if (Input.GetKey(KeyCode.A) && transform.position.x > -2)
        {
            position.x -= Time.deltaTime * 4f;
        }
        if (Input.GetKey(KeyCode.S) && transform.position.z > -12)
        {
            position.z -= Time.deltaTime * 4f;
        }
        if (Input.GetKey(KeyCode.D) && transform.position.x < 15)
        {
            position.x += Time.deltaTime * 4f;
        }
        transform.position = position;

        zoomMouse();
        rotateCamera();

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("MenuScene");
            CurcorController.cursorCon = false;
        }

    }
}
