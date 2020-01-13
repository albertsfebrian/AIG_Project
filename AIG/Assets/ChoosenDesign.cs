using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.IO;
using UnityEditor;

class ChoosenDesign
{
    private static ChoosenDesign choosenDesign = new ChoosenDesign();
    private int texture;
    public const int cancel = 77;
    

    public ChoosenDesign()
    {
        texture = 0;
    }

    public void changeToWall() {
        texture = DesignController.WALL;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
    public void changeToLion() {
        texture = DesignController.OBSTACLE_LION;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
    public void changeToCancel() {
        texture = cancel;
        Debug.Log("Cancel");
        Cursor.SetCursor(CurcorController.cursorImage, Vector2.zero, CursorMode.Auto);
    }
    public void changeToBox() {
        texture = DesignController.OBSTACLE_BOX;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
    public int getTexture() { return this.texture; }

    public static ChoosenDesign getInstance()
    {
        return choosenDesign;
    }

}
