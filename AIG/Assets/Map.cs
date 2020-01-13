using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

    public class Map
    {
        public GameObject wall;
        public GameObject enemy;
        public GameObject box;
        public GameObject lion;
        public GameObject treasure;
        public GameObject ground = null;

        public Spot[,] coor;
        public static int height,width;
    
        public void printMap()
        {
            Enemy.count = 0;
            wall = GameObject.FindGameObjectWithTag("WallTag");
            enemy = GameObject.FindGameObjectWithTag("EnemyTag");
            lion = GameObject.FindGameObjectWithTag("LionTag");
            box = GameObject.FindGameObjectWithTag("BoxTag");
            treasure = GameObject.FindGameObjectWithTag("TreasureTag");
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                //if (coor[row, col].value == FLOOR || coor[row, col].value == DOOR)
                //      Console.Write(" ");
                if (coor[row, col].value == DesignController.WALL)
                    GameObject.Instantiate(wall, new Vector3((float)row, 2f, (float)col), Quaternion.identity);
                else if (coor[row, col].value == DesignController.ENEMY)
                {
                    GameObject.Instantiate(enemy, new Vector3((float)row, 0.5f, (float)col), Quaternion.identity);
                    Enemy.count++;
                    Debug.Log("count : " + Enemy.count);
                }
                else if (coor[row, col].value == DesignController.TREASURE)
                    GameObject.Instantiate(treasure, new Vector3((float)row, 0.5f, (float)col), Quaternion.identity);
                else if (coor[row, col].value == DesignController.OBSTACLE_LION)
                    GameObject.Instantiate(lion, new Vector3((float)row+1, 0.5f, (float)col+1), Quaternion.identity);
                else if (coor[row, col].value == DesignController.OBSTACLE_BOX)
                    GameObject.Instantiate(box, new Vector3((float)row - 0.02f, 0.8f, (float)col - 1.5f), Quaternion.Euler(new Vector3(90, 0, 0)));

            }
                //Console.WriteLine();
            }
        }

        private void initializeMap()
        {
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    coor[row, col] = new Spot();
                    if (row == 0 || col == 0 || row == height - 1 || col == width - 1)
                        coor[row, col].value = DesignController.WALL;
                    else 
                        coor[row, col].value = DesignController.FLOOR;
                }
            }
        }

        public Map(int h, int w)
        {
            height = h;
            width = w;
            coor = new Spot[height, width];
            initializeMap();
        }

    }
