using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour {
    public enum COLOR { Red, Blue, Green };
    public COLOR color;	
    public string toScene;
    // Platform - ShooterHooter - PulseStick
    public bool disableParticles;
    public Material buttonMatOverride = null;

    void Start() {
	StartUpper();
	StartParticleSystem();
	StartButton();
    }

    void Update() {

    }

    private void StartButton(){
	GameObject button = transform.Find("Button").gameObject;
	MeshRenderer mr = button.GetComponent<MeshRenderer>();
	if(buttonMatOverride == null) {
	    mr.material.color = Color.green;
	} else {
	    mr.material = buttonMatOverride;
	}
    }

    private void StartUpper(){
	GameObject upper = transform.Find("Upper").gameObject;
	upper.GetComponent<MeshRenderer>().material.color =
	    color2color(color);
    }

    private void StartParticleSystem(){
	ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
	if(disableParticles){
	    ps.gameObject.SetActive(false);
	} else {
	    var main = ps.main;
	    main.startColor = color2color(color);
	}
    }    

    public void Activate(){
	SceneManager.LoadScene(toScene);
	Debug.Log(color2string(color) + " teleport to " + toScene);
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
}
