using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class FloodFill
{
    Map map2;
    int height, width;

    public const int OBSTACLE_LION = -2;
    public const int OBSTACLE_BOX = -3;
    public const int OBSTACLE_PLUS = -4;
    public const int WALL = -1;
    public const int FLOOR = 0;
    public const int DOOR = 1;
    public const int ENEMY = 99;

    const int FILL = 100;

    void copy(Map scr, Map dest) {
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                dest.coor[row, col] = scr.coor[row, col];
            }
        }
    }

    void floodFill(int row,int col)
    {
        if (map2.coor[row,col].value == FLOOR || map2.coor[row,col].value == DOOR)
        {
            map2.coor[row,col].value = FILL;
            floodFill(row + 1, col);
            floodFill(row - 1, col);
            floodFill(row , col+1);
            floodFill(row , col-1);
        }
    }
    
    /// <summary>
    /// return true if empty space detected
    /// </summary>
    /// <returns></returns>
    Boolean FindEmptySpace()
    {
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                if (map2.coor[row, col].value == FLOOR)
                    return true;
            }
        }
        return false;
    }

    /// <summary>
    /// if point return 0,0 means error
    /// 
    /// </summary>
    /// <returns></returns>
    Point searchStartingPoin()
    {
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                if (map2.coor[row,col].value == FLOOR)
                    return new Point(row, col);
            }
        }
        return new Point(0,0);
    }

    public Boolean isFFAccepted() {
        DesignMap.printDebug();
        Point start = searchStartingPoin();
        if (start.X == 0 && start.Y == 0) Console.WriteLine("Error Can't Save Map [Can't Fill all map using wall/ obstacle]");
        else
        {
            floodFill(start.X, start.Y);
            DesignMap.printDebug();
            if (FindEmptySpace() == true) return false;
            else return true;
            //Console.WriteLine("Error Can't Save Map [each room must connected]");
            //else return true;//Console.WriteLine("Map Save");
        }
        return false;
    }

    public FloodFill(Map passingMap, int height, int width)
    {
        this.height = height;
        this.width = width;
        map2 = new Map(height, width);
        copy(passingMap, map2);
    }

}

