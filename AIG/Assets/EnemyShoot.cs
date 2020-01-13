using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {

    public float speed = 3.0f;
    
    private bool _alive;
    private bool isPause;

	// Use this for initialization
	void Start () {
        _alive = true;
        isPause = false; 
	}
	
	// Update is called once per frame
	void Update () {

        if(_alive)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            //Debug.Log("alive");

            if (Physics.SphereCast(ray, 0.75f , out hit))
            {
                GameObject hitObj = hit.transform.gameObject;

                if (hitObj.GetComponent<PlayerCode>())
                {
                    this.GetComponent<Animator>().SetBool("isWalk", false);
                    if (!isPause)
                    {
                        PlayerCode player = hitObj.GetComponent<PlayerCode>();

                        if(player)
                        {
                            player.Damage();
                            StartCoroutine(Pause());
                        }
                    }
                }
            }
        }

        
	}

    private IEnumerator Pause()
    {
        isPause = true;
        yield return new WaitForSeconds(1f);
        isPause = false;

    }
}
