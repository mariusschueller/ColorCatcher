using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    /*
    public float[] enemySizes;//all the enemy atribute list
    public Color[] enemyColors;//all the enemy colors

    public float[] colorArr; //put in the float values for size of here inner, middle, then out ring int groups of 3
    public Color[] colors;// put colors
    */
    public int spawnRate; //how fast the enemies are being created
    //public int[] enemyProb; //the chances of each enemy

    //public int place;

    private bool isPaused;
    public GameObject pauseMenu;


    //creates the enemies from prefabs

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        isPaused = false;


        //Instantiate(enemy, new Vector2(0, 0), Quaternion.identity);
        //Instantiate(enemy, new Vector2(5, 0), Quaternion.identity);


        spawn();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause();
        }
    }

    public void pause()
    {
        if (isPaused)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            isPaused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }



    private void spawn()
    {
        /*Debug.Log("spawning enemy");
        place = 0;
        
        int rand = Random.Range(1, 101);

        for (int i = 0; i < enemyProb.Length; ++i)
        {
            if (rand <= enemyProb[i])
            {
                place = i;
                
                colorArr[0] = enemySizes[place * 3];
                colorArr[1] = enemySizes[place * 3 + 1];
                colorArr[2] = enemySizes[place * 3 + 2];

                colors[0] = enemyColors[place * 3];
                colors[1] = enemyColors[place * 3 + 1];
                colors[2] = enemyColors[place * 3 + 2];
        */
                Vector3 pos = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f),0);
                Instantiate(enemy, pos.normalized * 12 + player.transform.position, Quaternion.identity);
        /*
                break;
            }
        }
        */
        

        //SYSTEM FOR SPAWNING

        // create spawn list 

        // enemy attribute list, just lays out values in 3s in a 1d array

        // in spawnEnemy
        // random number
        // for (... spawnlist)
        // if (spawnnumlist[i] < randomNum < spawnnumlist[i+1])
        // set colorArr from enemyAttributes[i * 3] enemyAttributes[i * 3 + 1] enemyAttributes[i * 3 + 2] 
        // Instantiate 
        // Invoke spawnEnemy 
        // update the place so that enemy move can know where in the array to look
        Invoke("spawn", spawnRate);
    }


}
