using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lion : MonoBehaviour {
    private int row, col;

    public void changeValue(int row, int col, int dimension)
    {
        for (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                DesignController.getInstance().changeValue(row, col, DesignController.FLOOR);
            }
        }
    }

    public void fill(int row, int col)
    {
        changeValue(row, col,3);
    }

    public void removeLion()
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
