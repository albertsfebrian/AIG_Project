using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



    class BSP
    {
        public class Room
        {
            public int col, row;
            public int width, height;
            public bool isLeaf;
        }

        private List<Room> rooms;
        private Map map;
        private int limit, height, width;
        Random rand = new Random();

    private void createDoor(Room newRoom, int doorRand, string coordinate, int coor)
    {
        if (coordinate.Equals("vertical"))
        {
            map.coor[newRoom.row + doorRand, newRoom.col + coor - 1].value = DesignController.DOOR;
            map.coor[newRoom.row + doorRand + 1, newRoom.col + coor - 1].value = DesignController.DOOR;
            map.coor[newRoom.row + doorRand + 2, newRoom.col + coor - 1].value = DesignController.DOOR;
        }
        else
        {
            map.coor[newRoom.row + coor - 1, newRoom.col + doorRand].value = DesignController.DOOR;
            map.coor[newRoom.row + coor - 1, newRoom.col + doorRand + 1].value = DesignController.DOOR;
            map.coor[newRoom.row + coor - 1, newRoom.col + doorRand + 2].value = DesignController.DOOR;
        }
    }


    private Boolean splitRoom(Room newRoom)
    {
        if (newRoom.width >= newRoom.height && newRoom.width > limit * 2 + 4)
        {
            int randLimit = newRoom.width - (limit * 2 + 2);

            int veriticalCoor;
            do
            {
                veriticalCoor = rand.Next(randLimit) + limit + 2;
            } while (map.coor[newRoom.row, newRoom.col + veriticalCoor - 1].value == DesignController.DOOR || map.coor[newRoom.row + newRoom.height - 1, newRoom.col + veriticalCoor - 1].value == DesignController.DOOR);

            int doorRand = rand.Next(newRoom.height - 4) + 1;
            createDoor(newRoom, doorRand, "vertical", veriticalCoor);
            //int yRand = rand.Next(newRoom.width - 2 - veriticalCoor) + 1;
            //int xRand = rand.Next(4) + 2;
            int yRand = rand.Next((newRoom.width - 3 - veriticalCoor)) + 1;
            int xRand = rand.Next(2) + 2;
            map.coor[newRoom.row + xRand, newRoom.col + veriticalCoor + yRand].value = DesignController.ENEMY;
            //map.coor[newRoom.row + doorRand, newRoom.col + veriticalCoor - 1].value = Map.DOOR;
            //map.coor[newRoom.row + doorRand + 1, newRoom.col + veriticalCoor - 1].value = Map.DOOR;
            //map.coor[newRoom.row + doorRand + 2, newRoom.col + veriticalCoor - 1].value = Map.DOOR;

            Room leftRoom = createRoom(newRoom.col, newRoom.row, veriticalCoor, newRoom.height);
            Room rightRoom = createRoom(newRoom.col + veriticalCoor - 1, newRoom.row, newRoom.width - veriticalCoor + 1, newRoom.height);

            if (leftRoom.isLeaf) rooms.Add(leftRoom);
            if (rightRoom.isLeaf) rooms.Add(rightRoom);
        }
        else if (newRoom.height >= newRoom.width && newRoom.height > limit * 2 + 4)
        {
            int randLimit = newRoom.height - (limit * 2 + 2);
            int horizontalCoor;

            do
            {
                horizontalCoor = rand.Next(randLimit) + limit + 2;
            } while (map.coor[newRoom.row + horizontalCoor - 1, newRoom.col].value == DesignController.DOOR || map.coor[newRoom.row + horizontalCoor - 1, newRoom.col + newRoom.width - 1].value == DesignController.DOOR);

            int doorRand = rand.Next(newRoom.width - 4) + 1;
            createDoor(newRoom, doorRand, "horizontal", horizontalCoor);
            //int yRand = rand.Next(newRoom.width - 2) + 1;
            //int xRand = rand.Next((newRoom.height - 2 - horizontalCoor)) + 1;
            int yRand = rand.Next((newRoom.width - 3) - 2 + 1) + 2;
            int xRand = rand.Next((newRoom.height - 3 - horizontalCoor)) + 1;
            map.coor[newRoom.row + horizontalCoor + xRand, newRoom.col + yRand].value = DesignController.ENEMY;
            //map.coor[newRoom.row + horizontalCoor - 1,newRoom.col + doorRand].value = Map.DOOR;
            //map.coor[newRoom.row + horizontalCoor - 1,newRoom.col + doorRand+1].value = Map.DOOR;
            //map.coor[newRoom.row + horizontalCoor - 1,newRoom.col + doorRand+2].value = Map.DOOR;

            Room topRoom = createRoom(newRoom.col, newRoom.row, newRoom.width, horizontalCoor);
            Room bottomRoom = createRoom(newRoom.col, horizontalCoor + newRoom.row - 1, newRoom.width, newRoom.height - horizontalCoor + 1);

            if (topRoom.isLeaf) rooms.Add(topRoom);
            if (bottomRoom.isLeaf) rooms.Add(bottomRoom);

        }
        else
            return false;

        return true;
    }


    private Room createRoom(int col, int row, int width, int height)
        {
            Room newRoom = new Room();
            newRoom.col = col;
            newRoom.row = row;
            newRoom.width = width;
            newRoom.height = height;

            newRoom.isLeaf = !splitRoom(newRoom);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == 0 || j == 0 || i == height - 1 || j == width - 1)
                    {
                        if (map.coor[i + row, j + col].value == DesignController.FLOOR)
                            map.coor[i + row, j + col].value = DesignController.WALL;
                    }
                }
            }

            return newRoom;
        }

        /// <summarrow>
        /// return a Map with BSP Algorithm
        /// </summarrow>
        /// <returns></returns>
        public Map createBSP()
        {
            createRoom(0, 0, width, height);
            return map;
        }

        public BSP(int height, int width)
        {
            this.height = height;
            this.width = width;
            // the limit is 30% of Width
            this.limit = width * 30 / 100;
            map = new Map(height, width);
            rooms = new List<Room>();
        }


    }
