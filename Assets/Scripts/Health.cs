using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour
{
    public float startHealth = 5;
    public float health;
    public Camera cam;
    private Slider slider;
    public bool initialized = false;

    void Start()
    {
        health = startHealth;
        slider = GetComponentInChildren<Slider>();
        slider.value = 1f;
        initialized = true;
    }

    private void Update()
    {
        slider.transform.LookAt(cam.transform);
    }

    public void takeDamage(int damage) {
        health -= damage;
        slider.value = health / startHealth;
        //Debug.Log(gameObject.name + " taking " + damage + " damage. " + health + " hp left.");
        if (health <= 0)
            die();
    }

    public void die() {
        gameObject.SetActive(false);
    }
}
