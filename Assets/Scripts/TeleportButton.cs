using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportButton : MonoBehaviour {
    private Teleport teleport;
    private Vector3 pushIn = new Vector3(0, -0.02f, 0);
    private float timeAfterPush = 0.5f;
    private float timer = -1;
    
    void Start(){	
	teleport = gameObject.GetComponentInParent<Teleport>();
    }

    void Update(){
	if(timer >= timeAfterPush){
	    timer = -1;
	    teleport.Activate();
	}
	if(timer >= 0){
	    timer += Time.deltaTime;
	}
    }

    private void OnTriggerEnter(Collider c){
	if(c.gameObject.tag == "Hand"){
	    Push();
	}
    }

    private void Push(){
	gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
	transform.position += pushIn;	
    }	
}

