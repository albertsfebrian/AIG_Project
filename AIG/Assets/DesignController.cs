using System;
using System.Collections.Generic;
using System.Text;


class DesignController
{
    Map map;
    private static DesignController designMap = new DesignController();
    public const int OBSTACLE_LION = -2;
    public const int OBSTACLE_BOX = -3;
    public const int OBSTACLE_PLUS = -4;
    public const int TREASURE = -5;
    public const int WALL = -1;
    public const int FLOOR = 0;
    public const int DOOR = 1;
    public const int ENEMY = 99;
    public const int PLAYER = 999;

    public DesignController()
    {
        
    }

    public Map getMap() { return map; }
    public int getCoor(int row,int col)
    {
        return map.coor[row, col].value;
    }
    public void generateMap() {
        map = new Map(21, 21);
    }
    public void changeValue(int row, int col, int type)
    {
        map.coor[row, col].value = type;
    }

    public static DesignController getInstance()
    {
        return designMap;
    }

}

