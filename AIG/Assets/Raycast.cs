using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour {
    public ParticleSystem shootingAnimation;

	// Use this for initialization
	void Start () {

	}

    // Update is called once per frame
    float timeFiring = 0f;
	void Update () {  

        RaycastHit hit;

        Vector3 lookFront = transform.TransformDirection(Vector3.forward) * 100;

        if (Physics.Raycast(transform.position,(lookFront),out hit,Mathf.Infinity) )
        {

            //Debug.DrawRay(transform.position, lookFront, Color.red);
            
            //Debug.Log(hit.collider.name);
            if (Input.GetMouseButtonDown(0) && Time.time > timeFiring)
            {
                shootingAnimation.Play();
                timeFiring = Time.time + 1f;


                if (hit.collider.GetComponent<Head>() != null)
                {
                    Debug.Log("Headshot");
                    hit.collider.transform.parent.parent.GetComponent<Enemy>().headShoot();
                    Debug.Log(hit.collider.transform.parent.parent.GetComponent<Enemy>().getHealth());
                }
                else if (hit.transform.GetComponent<Enemy>() != null)
                {
                    Debug.DrawRay(transform.position, lookFront, Color.blue);
                    Debug.Log("Masuk");
                    
                    //else
                    //{
                        hit.transform.GetComponent<Enemy>().shooten();
                        Debug.Log(hit.transform.GetComponent<Enemy>().getHealth());
                    //}
                    //Debug.Log("Enemy : " + hit.collider.gameObject.GetComponent<Enemy>().tag);

                }
            }
            
        }
	}
}
