using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getHit : MonoBehaviour
{
    public GameObject enemy;
    // public GameObject particles; though this could be in enemy move 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.tag);

        //might want to rework this so that the collider is sized in the enemyMove for speed when weapon

        if (collision.gameObject.tag == "weapon")
        {
            Debug.Log("hit");
            Destroy(enemy);
        }
            
    }
}
