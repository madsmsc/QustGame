using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseSaber : MonoBehaviour {

    public LayerMask layer;
    private Vector3 previousPosition;

    void Start() {
	
    }

    void Update() {
	RaycastHit hit;
	if(Physics.Raycast(transform.position,
			   transform.forward,
			   out hit, 1, layer)){
	    float angle = Vector3.Angle(transform.position-previousPosition,
					hit.transform.up);
	    if(angle > 130){
		GameObject go = hit.transform.gameObject;
		Destroy(go);
		/*
		PulseCube cube = go.GetComponent<PulseCube>();
		cube.Destroy();
		*/
	    }
	}
	previousPosition = transform.position;
    }
}
