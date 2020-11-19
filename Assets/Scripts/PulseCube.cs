using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PulseCube : MonoBehaviour {
    public float speed = 0.5f;
    public GameObject explosion;

    private PulseSpawner pulseSpawner;

    void Start() {
	pulseSpawner = FindObjectOfType<PulseSpawner>();
    }

    void Update() {
	transform.position += Time.deltaTime *
	    transform.forward * speed;
	if(transform.position.z > 16){
	    Remove("missed");
	    pulseSpawner.Missed();
	}
    }

    private void Remove(string msg){
	//Debug.Log("Remove: " + msg);
	Destroy(this.gameObject);
    }

    public void Hit(){	
	GameObject ex = Instantiate(explosion);
	ex.transform.position = transform.position;
	Remove("hit");
    }
}
