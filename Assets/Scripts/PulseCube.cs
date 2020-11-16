using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PulseCube : MonoBehaviour {
    public float speed = 2;

    void Start() {
	
    }

    void Update() {
	transform.position += Time.deltaTime *
	    transform.forward * speed;
	if(transform.position.z > 16){
	    Destroy(this.gameObject);
	}
    }
}
