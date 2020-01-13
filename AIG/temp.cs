using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
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
        private int limit,height,width;
        Random rand= new Random();

        private bool splitRoom(Room newRoom)
        {
            if (newRoom.width >= newRoom.height && newRoom.width >= limit * 2 + 4)
            {
                int randLimit = newRoom.width - (limit * 2 + 2);

                int verticalCoord;
                do
                {
                    verticalCoord = rand.Next(randLimit) + limit + 2;
                } while (map.coor[newRoom.row,newRoom.col + verticalCoord - 1].value == Map.DOOR || map.coor[newRoom.row + newRoom.height - 1,newRoom.col + verticalCoord - 1].value == Map.DOOR);

                int wallRand = rand.Next(newRoom.height - 2) + 1;
                map.coor[newRoom.row + wallRand,newRoom.col + verticalCoord - 1].value = Map.DOOR;

                Room leftRoom = createRoom(newRoom.col, newRoom.row, verticalCoord, newRoom.height);
                Room rightRoom = createRoom(newRoom.col + verticalCoord - 1, newRoom.row, newRoom.width - verticalCoord + 1, newRoom.height);

                if (leftRoom.isLeaf)
                    rooms.Add(leftRoom);
                if (rightRoom.isLeaf)
                    rooms.Add(rightRoom);
            }
            else if (newRoom.height > newRoom.width && newRoom.height >= limit * 2 + 4)
            {
                int randLimit = newRoom.height - (limit * 2 + 2);
                int horiWall;

                do
                {
                    horiWall = rand.Next(randLimit) + limit + 2;
                } while (map.coor[newRoom.row + horiWall - 1,newRoom.col].value == Map.DOOR || map.coor[newRoom.row + horiWall - 1,newRoom.col + newRoom.width - 1].value == Map.DOOR);

                int wallRand = rand.Next(newRoom.width - 2) + 1;
                map.coor[newRoom.row + horiWall - 1,newRoom.col + wallRand].value = Map.DOOR;

                Room topRoom = createRoom(newRoom.col, newRoom.row, newRoom.width, horiWall);
                Room bottomRoom = createRoom(newRoom.col, horiWall + newRoom.row - 1, newRoom.width, newRoom.height - horiWall + 1);

                if (topRoom.isLeaf)
                    rooms.Add(topRoom);
                if (bottomRoom.isLeaf)
                    rooms.Add(bottomRoom);

            }
            else
                return false;

            return true;
        }


        private Room createRoom(int row, int col, int width, int height)
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
                        if (map.coor[i + row,j + col].value == Map.FLOOR)
                            map.coor[i + row,j + col].value = Map.WALL;
                    }
                }
            }

            return newRoom;
        }

        /// <summary>
        /// return a Map with BSP Algorithm
        /// </summary>
        /// <returns></returns>
        public Map createBSP()
        {
            createRoom(0, 0, width, height);
            return map;
        }

        public BSP(int height,int width)
        {
            this.height = height;
            this.width = width;
            // the limit is 27% of Width
            this.limit = width * 27 / 100;
            map = new Map(height, width);
            rooms = new List<Room>();
        }


    }
}
