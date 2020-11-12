using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportController : MonoBehaviour {
    public Transform player;
    public WatchController watch;
    public float activationDist;
    public OVRInput.Controller controller;
    /* OVRInput.Button.One = A
       OVRInput.Button.Two = B */
    public GameObject upper;
    public Color color;
    public float dist = 0;
    public string toScene;
    /* ShooterHooter
       Platform */
    private bool tDown = false; // activate
    private bool aDown = false;
    
    void Start() {
	upper.GetComponent<MeshRenderer>().material.color = color;
	GetComponentInChildren<ParticleSystem>().startColor = color;
    }

    void Update() {
	dist = Vector3.Distance(transform.position,
				player.transform.position);
	if(dist < activationDist){
	    string s = "\nActivate portal [A]";
	    watch.AddText(s);
	}

	if (!aDown && OVRInput.Get(OVRInput.Button.One, controller)){
	    aDown = true;
	    Activate();
	} if (aDown && !OVRInput.Get(OVRInput.Button.One, controller)){
	    aDown = false;
	}

	if (!tDown && Input.GetKey("t")){
	    tDown = true;
	    Activate();
	} if (tDown && !Input.GetKey("t")){
	    tDown = false;
	}	
    }

    public void Activate(){
	if(dist < activationDist){
	    Debug.Log("Activating teleport!");
	    SceneManager.LoadScene(toScene);
	} else {
	    Debug.Log("Too far from teleporter!");
	}
    }
}
