using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour {
    public enum COLOR { Red, Blue, Green };
    public COLOR color;	
    public float activationDist;
    public OVRInput.Controller controller;
    // OVRInput.Button.One = A - OVRInput.Button.Two = B
    public string toScene;
    // Platform - ShooterHooter - PulseStick

    private GameObject upper;
    private Transform player;
    private float dist;
    private bool canActivate = false;
    private bool tDown = false; // activate
    private bool aDown = false; // activate

    void Start() {
	player = GameObject.Find("OVRPlayerController").transform;
	upper = transform.GetChild(0).gameObject;
	Color tc = color2color(color);
	upper.GetComponent<MeshRenderer>().material.color = tc;

	ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        var main = ps.main;
        main.startColor = tc;

	HandleDist();
    }

    void Update() {
	HandleDist();
	HandleKeys();
    }

    private void HandleDist(){
	dist = Vector3.Distance(transform.position,
				player.transform.position);
	canActivate = dist < activationDist;
    }

    private void HandleKeys(){
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

    private Color color2color(COLOR c){
	if(c == COLOR.Red){
	    return Color.red;
	} else if (c == COLOR.Blue){
	    return Color.blue;
	} else if (c == COLOR.Green){
	    return Color.green;
	} else {
	    return Color.gray;
	}
    }
    
    private string color2string(COLOR c){
	if(c == COLOR.Red){
	    return "red";
	} else if (c == COLOR.Blue){
	    return "blue";
	} else if (c == COLOR.Green){
	    return "green";
	} else {
	    return "gray";
	}
    }

    private string Info(){
	return color2string(color) + " teleport to " + toScene;
    }

    public string GetText(){
	if(canActivate){
	    return "\n[A]: " + Info();
	} else {
	    return null;
	}
    }

    public void Activate(){
	if(dist < activationDist){
	    Debug.Log("Activating " + Info());
	    SceneManager.LoadScene(toScene);
	} else {
	    Debug.Log("Too far from teleporter!");
	}
    }
}
