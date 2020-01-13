using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    private int row, col;


    public void fill(int row, int col)
    {
        this.row = row;
        this.col = col;
    }

    public void removeWall()
    {
        DesignController.getInstance().changeValue(row, col, DesignController.FLOOR);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
