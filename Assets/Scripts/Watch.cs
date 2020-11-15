using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watch : MonoBehaviour{
    private TextMesh text;
    private GameObject teleporters;
    private Debugger debug;
    private Teleport[] tps;
    private int hp = 100;
    private int pp = 20;

    void Start () {
	tps = FindObjectsOfType<Teleport>();
	debug = FindObjectOfType<Debugger>();
	text = transform.GetComponentInChildren<TextMesh>();
    }

    void Update() {
	text.text = "hp: " + hp + "\npp: " + pp;
	if(debug != null){
	    text.text += debug.GetText();
	}
	if(tps != null){
	    foreach (Teleport tp in tps){
		string ts = tp.GetText();
		if(ts != null){
		    text.text += ts;
		}
	    }
	}
    }

}
