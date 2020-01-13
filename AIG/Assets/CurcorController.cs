using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurcorController : MonoBehaviour {
    public static bool cursorCon = false;
    [SerializeField]
    public Texture2D cur;
    public static Texture2D cursorImage;

    // Use this for initialization
    void Start () {
        cursorImage = cur;
	}
	
	// Update is called once per frame
	void Update () {
        Cursor.visible = cursorCon;
    }
}
