using System;
using System.Collections.Generic;
using System.Text;

class CheckMap
{
    Map map;
    int height, width;

    public const int OBSTACLE_LION = -2;
    public const int OBSTACLE_BOX = -3;
    public const int OBSTACLE_PLUS = -4;
    public const int WALL = -1;
    public const int FLOOR = 0;
    public const int DOOR = 1;
    public const int ENEMY = 99;

    Boolean isEmpty()
    {
        for (int row = 1; row < height-1; row++)
        {
            for (int col = 1; col < width-1; col++)
            {
                if (map.coor[row, col].value == WALL || map.coor[row, col].value == OBSTACLE_BOX || map.coor[row, col].value == OBSTACLE_PLUS || map.coor[row, col].value == OBSTACLE_LION )
                    return false;
            }
        }
        return true;
    }

    public Boolean isMapAccepted() {
        DesignMap.printDebug();
        if (isEmpty())
        {
            Console.WriteLine("Error Can't Save Map [no object found]");
        }
        else
        {
            FloodFill ff = new FloodFill(map, height, width);
            if (ff.isFFAccepted()) return true;
        }
        return false;
    }

    public CheckMap(Map passingMap, int height, int width)
    {
        this.height = height;
        this.width = width;
        map = new Map(height, width);
        map = passingMap;
    }

}

