using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour {
    GameObject ground;

    // Use this for initialization
    void Start () {
        ground = GameObject.FindGameObjectWithTag("GroundTag");
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(ground.transform.position, Vector3.up, Time.deltaTime*10f);
        

    }
}
