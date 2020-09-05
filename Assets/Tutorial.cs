using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject enemies;
    public GameObject player;
    public GameObject use;

    private Text text;
    private float INTERACT_DIST = 1;
    private bool enemiesDead = false;
    private string GOOD_JOB = "Good job!";
    private string KILL_THEM = "Kill them! Quick!";

    void Start()
    {
        text = GetComponentInChildren<Text>();
    }

    void Update()
    {
        updateKeyText();
        updateEnemiesText();
    }

    private void updateKeyText()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < INTERACT_DIST)
        {
            if (!enemies.activeSelf && !use.activeSelf)
            {
                use.SetActive(true);
            }
            if (Input.GetKeyUp("e"))
            {
                spawnEnemies();
                use.SetActive(false);
            }
        }
        else if (use.activeSelf)
        {
            use.SetActive(false);
        }
    }

    private void updateEnemiesText()
    {
        if (enemies.activeSelf && !enemiesDead)
        {
            bool setDead = true;
            foreach (Health health in enemies.GetComponentsInChildren<Health>())
            {
                //Debug.Log("hp: " + health.health+", initialized: "+health.initialized);
                if (!health.initialized || health.health > 0)
                {
                    setDead = false;
                }
            }
            if(setDead)
                enemiesDead = true;
        }

        if (enemiesDead && text.text != GOOD_JOB)
        {
            text.text = GOOD_JOB;
        }
    }

    private void spawnEnemies()
    {
        enemies.SetActive(true);
        text.text = KILL_THEM;
    }
}
