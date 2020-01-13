using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    class Point {
        public int X;
        public int Y;
        public Point(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
        public Point() { }
    }
    class Prim
    {
        Map map;
        int height, width;
        const int WALL = -1;
        const int FLOOR = 0;

        const int LEFT_CHK = 8;
        const int RIGHT_CHK = 4;
        const int UP_CHK = 2;
        const int DOWN_CHK = 1;
        Random rand = new Random();

        void generatePrim(int row,int col)
        {
            int directionChecker = 0;

            while(directionChecker != 15)
            {
                //Console.WriteLine("Loop : " + i);
                int direction = rand.Next(1, 5);
                //Console.WriteLine("direction : " + direction);

                Point open = new Point();
                Point open2 = new Point();
                if (direction == 1) { open = new Point(-2, 0); open2 = new Point(-1, 0); direction = LEFT_CHK    ; /*Console.WriteLine("new Direction : " + direction);*/ } //left 
                else if (direction == 2) { open = new Point(2, 0); open2 = new Point(1, 0); direction = RIGHT_CHK; /*Console.WriteLine("new Direction : " + direction);*/ } //right
                else if (direction == 3) { open = new Point(0, -2); open2 = new Point(0, -1); direction = UP_CHK ; /*Console.WriteLine("new Direction : " + direction);*/ } //up
                else if (direction == 4) { open = new Point(0, 2); open2 = new Point(0, 1); direction = DOWN_CHK ; /*Console.WriteLine("new Direction : " + direction);*/ } //down
                //Console.WriteLine("directionchck : " + directionChecker);
                //Console.Read();

                if ((directionChecker & direction) == direction)
                {
                //    Console.WriteLine("Not Go");
                    continue;
                }
                //else Console.WriteLine("Go");
                directionChecker = directionChecker | direction;


                if (row + open.Y > 0 && row + open.Y < height && col + open.X > 0 && col + open.X < width)
                {
                    //Console.WriteLine(row + " col : " + col + "dir : " + direction);
                    if (map.coor[row + open.Y, col + open.X].value == WALL)
                    {
                        //Console.WriteLine("Wall .. .destroy");
                        map.coor[row + open.Y, col + open.X].value = FLOOR;
                        map.coor[row + open2.Y, col + open2.X].value = FLOOR;
                        //map.printMap();
                        //Console.WriteLine();
                        generatePrim(row + open.Y, col + open.X);
                        //map.printMap();
                        //Console.WriteLine();
                    }
                   // else
                    //    Console.WriteLine("Skip");
                    
                } 
            }
        }

        public void fillMap()
        {
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    map.coor[row, col] = new Spot();
                    map.coor[row, col].value = WALL;
                }
            }
        }

        public Map createPrim()
        {
            //map.coor[1, 1].value = 0;
            //Console.Read();
            //map.printMap();
            generatePrim(1,1);
            //map.printMap();
            return map;
        }


        public Prim(int height, int width)
        {
            this.height = height;
            this.width = width;
            map = new Map(height, width);
            fillMap();
            
        }
    }

