using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRaycast : MonoBehaviour {
    public GameObject wall,lion,box;
    public const int OBSTACLE_LION = -2;
    public const int OBSTACLE_BOX = -3;
    public const int OBSTACLE_PLUS = -4;
    public const int WALL = -1;
    public const int FLOOR = 0;
    public const int DOOR = 1;
    public const int ENEMY = 99;
    private ChoosenDesign cd = ChoosenDesign.getInstance();
    RaycastHit hit;


    public bool checkDimension(int row,int col, int dimension)
    {
        for (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                if (DesignController.getInstance().getCoor((int)hit.transform.position.x +i, (int)hit.transform.position.z + j) != DesignController.FLOOR)
                {
                    Debug.Log(DesignController.getInstance().getCoor((int)hit.transform.position.x + i, (int)hit.transform.position.z + j));
                    Debug.Log("Coor False: " + (int)hit.transform.position.x + " " + (int)hit.transform.position.z);
                    return false;
                }
            }
        }
        return true;
    }

    public void changeValue(int row, int col , int dimension, int value)
    {
        for (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                //if(i ==0 && j ==0 ) DesignController.getInstance().changeValue((int)hit.transform.position.x, (int)hit.transform.position.z, value);
                if (i == 0 && j == 0) continue;
                else DesignController.getInstance().changeValue((int)hit.transform.position.x + i, (int)hit.transform.position.z + j, OBSTACLE_PLUS);
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        wall = GameObject.FindGameObjectWithTag("WallTag");
        lion = GameObject.FindGameObjectWithTag("LionTag");
        box = GameObject.FindGameObjectWithTag("BoxTag");
    }

    void showDesign()
    {
        GameObject go = null;

        if(cd.getTexture() == WALL)
        {
            go = GameObject.Instantiate(wall, new Vector3(hit.transform.position.x, hit.transform.position.y + 2, hit.transform.position.z), hit.transform.rotation);
        }
        else if(cd.getTexture() == OBSTACLE_LION)
        {
            go= GameObject.Instantiate(lion, new Vector3(hit.transform.position.x + 1, hit.transform.position.y + 0.52f, hit.transform.position.z + 1f), hit.transform.rotation);
        }
        else if(cd.getTexture() == OBSTACLE_BOX)
        {
            go = GameObject.Instantiate(box, new Vector3(hit.transform.position.x - 0.02f, hit.transform.position.y + 0.8f, hit.transform.position.z - 1.5f), Quaternion.Euler(new Vector3(hit.transform.rotation.x + 90, hit.transform.rotation.y + 0, hit.transform.rotation.z + 0)));
        }

        if (go != null)
        {
            go.GetComponent<Collider>().enabled = false;
            Destroy(go, 0.02f);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.GetComponent<Floor>() != null)
            {
                showDesign();
            }
        }

        if (Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0))
        {
            //showDesign();

            if (hit.transform.GetComponent<Floor>() != null && cd.getTexture() == WALL)
            {
                if(DesignController.getInstance().getCoor((int)hit.transform.position.x , (int)hit.transform.position.z) == DesignController.FLOOR)
                {
                    GameObject temp = GameObject.Instantiate(wall, new Vector3(hit.transform.position.x, hit.transform.position.y + 2, hit.transform.position.z), hit.transform.rotation);
                    DesignController.getInstance().changeValue((int)hit.transform.position.x, (int)hit.transform.position.z, WALL);
                    temp.GetComponent<Wall>().fill((int)hit.transform.position.x, (int)hit.transform.position.z);
                }
               
            }
            //else if (hit.transform.GetComponent<Floor>() != null && cd.getTexture() == OBSTACLE_LION)
            //{
            //    Debug.Log("Coor : " + (int)hit.transform.position.x + " "+ (int)hit.transform.position.z);
            //    if (checkDimension((int)hit.transform.position.x, (int)hit.transform.position.z, 3))
            //    {
            //        GameObject temp = GameObject.Instantiate(lion, new Vector3(hit.transform.position.x + 1, hit.transform.position.y + 0.52f, hit.transform.position.z + 1f), hit.transform.rotation);
            //        DesignController.getInstance().changeValue((int)hit.transform.position.x, (int)hit.transform.position.z, OBSTACLE_LION);
            //        changeValue((int)hit.transform.position.x, (int)hit.transform.position.z, 3, OBSTACLE_LION);
            //        temp.GetComponent<Lion>().fill((int)hit.transform.position.x, (int)hit.transform.position.z);
            //    }
               
            //    //DesignController.getInstance().changeValue((int)hit.transform.position.x, (int)hit.transform.position.z, OBSTACLE_LION);
            //    //DesignController.getInstance().changeValue((int)hit.transform.position.x+1, (int)hit.transform.position.z, OBSTACLE_PLUS);
            //    //DesignController.getInstance().changeValue((int)hit.transform.position.x+2, (int)hit.transform.position.z, OBSTACLE_PLUS);
            //    //DesignController.getInstance().changeValue((int)hit.transform.position.x, (int)hit.transform.position.z+1, OBSTACLE_PLUS);
            //    //DesignController.getInstance().changeValue((int)hit.transform.position.x+1, (int)hit.transform.position.z+1, OBSTACLE_PLUS);
            //    //DesignController.getInstance().changeValue((int)hit.transform.position.x+2, (int)hit.transform.position.z+1, OBSTACLE_PLUS);
            //    //DesignController.getInstance().changeValue((int)hit.transform.position.x, (int)hit.transform.position.z+2, OBSTACLE_PLUS);
            //    //DesignController.getInstance().changeValue((int)hit.transform.position.x+1, (int)hit.transform.position.z+2, OBSTACLE_PLUS);
            //    //DesignController.getInstance().changeValue((int)hit.transform.position.x+2, (int)hit.transform.position.z+2, OBSTACLE_PLUS);
                
            //}
            else if (hit.transform.GetComponent<Floor>() != null && cd.getTexture() == OBSTACLE_LION)
            {
                if (checkDimension((int)hit.transform.position.x, (int)hit.transform.position.z, 3))
                {
                    //GameObject.Instantiate(box, new Vector3(hit.transform.position.x - 0.02f, hit.transform.position.y+1, hit.transform.position.z - 1.5f), hit.transform.rotation);

                    GameObject.Instantiate(lion, new Vector3(hit.transform.position.x + 1, hit.transform.position.y + 0.52f, hit.transform.position.z + 1f), hit.transform.rotation);
                    DesignController.getInstance().changeValue((int)hit.transform.position.x, (int)hit.transform.position.z, OBSTACLE_LION);
                    changeValue((int)hit.transform.position.x, (int)hit.transform.position.z, 3, OBSTACLE_LION);
                }
            }
            else if (hit.transform.GetComponent<Floor>() != null && cd.getTexture() == OBSTACLE_BOX)
            {
                if (checkDimension((int)hit.transform.position.x, (int)hit.transform.position.z, 2))
                {
                    //GameObject.Instantiate(box, new Vector3(hit.transform.position.x - 0.02f, hit.transform.position.y+1, hit.transform.position.z - 1.5f), hit.transform.rotation);

                    GameObject.Instantiate(box, new Vector3(hit.transform.position.x - 0.02f, hit.transform.position.y + 0.8f, hit.transform.position.z - 1.5f), Quaternion.Euler(new Vector3(hit.transform.rotation.x + 90, hit.transform.rotation.y + 0, hit.transform.rotation.z +0)));
                    DesignController.getInstance().changeValue((int)hit.transform.position.x, (int)hit.transform.position.z, OBSTACLE_BOX);
                    changeValue((int)hit.transform.position.x, (int)hit.transform.position.z, 2, OBSTACLE_BOX);
                }
            }
            //else if(hit.transform)
           
            if(ChoosenDesign.getInstance().getTexture() == ChoosenDesign.cancel)
            {
                if(hit.transform.gameObject.tag == "WallTag")
                {
                    Wall wall = hit.transform.GetComponent<Wall>();
                    wall.removeWall();
                    Destroy(wall.gameObject);
                }
                else if (hit.transform.gameObject.tag == "LionTag")
                {
                    Lion lion = hit.transform.GetComponent<Lion>();
                    lion.removeLion();
                    Destroy(lion.gameObject);
                }
            }

        }

        

    }
}
