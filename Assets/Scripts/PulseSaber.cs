using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseSaber : MonoBehaviour {

    public LayerMask layer;

    private Vector3 prevTip;
    private PulseSpawner pulseSpawner;

    void Start() {
	pulseSpawner = FindObjectOfType<PulseSpawner>();
    }

    void Update() {
	Vector3 tip = transform.position + transform.forward;
	RaycastHit hit;
	if(Physics.Raycast(transform.position, transform.forward,
			   out hit, 1, layer)){
	    float angle = Vector3.Angle(tip - prevTip, hit.transform.up);
	    /*
	    Debug.Log("tip=" + tip + ", prevTip=" + prevTip +
		      ", hit.t.up=" + hit.transform.up);
	    Debug.Log(layer.value + " saber hit a cube with angle " + angle);
	    */
	    if(angle > 130){
		pulseSpawner.Hit(hit.transform.gameObject);
	    }
	}
	prevTip = tip;
    }

}
