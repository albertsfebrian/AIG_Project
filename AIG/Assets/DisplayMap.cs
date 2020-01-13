using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;



public class DisplayMap : MonoBehaviour {
    public static Map map;
    MapStatus ms = MapStatus.getInstance();
    public static int yRand = 0, xRand = 0;
    public static int yTRand = 0, xTRand = 0;
    System.Random rand = new System.Random();

    public void generateEnemy()
    {
        do
        {
            yRand = rand.Next(18 - 5 + 1) + 5;
            xRand = rand.Next(18 - 5 + 1) + 5;
        } while (!DisplayMap.isMapFloor(xRand, yRand));

        setEnemy(xRand, yRand);
    }

    public void generateTreasure()
    {
        do
        {
            yTRand = rand.Next(18 - 5 + 1) + 5;
            xTRand = rand.Next(18 - 5 + 1) + 5;
        } while (!DisplayMap.isMapFloor(xTRand, yTRand));

        setTreasure(xTRand, yTRand);
    }

    public static void setTreasure(int row, int col)
    {
        map.coor[row, col].value = DesignController.TREASURE;
    }
    public static void removeTreasure(int row, int col)
    {
        map.coor[row, col].value = DesignController.TREASURE;
    }

    public static void setEnemy(int row,int col) {
        map.coor[row, col].value = DesignController.ENEMY;
    }
    public static void removeEnemy(int row,int col)
    {
        map.coor[row, col].value = DesignController.FLOOR;
    }
    public static Boolean isMapFloor(int row,int col)
    {
        if (map.coor[row, col].value == DesignController.FLOOR) return true;
        else return false;
    }

    public void scanMap(string path)
    {

        string rowStr;
        int row = 0;
        StreamReader theReader = new StreamReader(path, Encoding.Default);

        using (theReader)
        {
            do
            {
                rowStr = theReader.ReadLine();
                if (rowStr != null)
                {
                    for (int i = 0; i < 21; i++)
                    {
                        if (rowStr[i] == '#')
                            map.coor[row, i].value = DesignController.WALL;
                        else if (rowStr[i] == ' ')
                            map.coor[row, i].value = DesignController.FLOOR;
                        else if (rowStr[i] == 'L')
                            map.coor[row, i].value = DesignController.OBSTACLE_LION;
                        else if (rowStr[i] == 'B')
                            map.coor[row, i].value = DesignController.OBSTACLE_BOX;
                        else if (rowStr[i] == 'X')
                            map.coor[row, i].value = DesignController.OBSTACLE_PLUS;
                    }
                    row++;
                }
            }
            while (rowStr != null);
            theReader.Close();
        }


    }
   
    // Use this for initialization
    void Start () {
        TxtController.getInstance().readTxt();
        //ms.setChoosenMap(1);
        map = new Map(21, 21);
        //Debug.Log(ms.getChoosenMap());
        if (ms.getChoosenMap() == 0)
        {
            map = new Prim(21, 21).createPrim();
        }
        else if (ms.getChoosenMap() == 1)
        {
            map = new BSP(21, 21).createBSP();
            generateTreasure();
        }
        else if (ms.getChoosenMap() == 2)
        {
            map = new Map(21, 21);
            scanMap("Assets/Resources/Map1.txt");
            if (ms.getToPlay() == true)
            {
                generateEnemy();
                generateTreasure();
            }
        }
        else if (ms.getChoosenMap() == 3)
        {
            map = new Map(21, 21);
            scanMap("Assets/Resources/Map2.txt");
            if (ms.getToPlay() == true)
            {
                generateEnemy();
                generateTreasure();
            }

        }
        else if (ms.getChoosenMap() == 4)
        {
            map = new Map(21, 21);
            scanMap("Assets/Resources/Map3.txt");
            if (ms.getToPlay() == true)
            {
                generateEnemy();
                generateTreasure();
            }

        }

        //
        map.printMap();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
