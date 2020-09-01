﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController: MonoBehaviour{
    public float pressLength;
    public GameObject textGO;
    private TextMesh text;    
    private bool pressed;
    private Vector3 startPos;
    private Rigidbody rb;
    private int count = 0;

    private void ButtonAction(){
    	text.text = (++count) + " button presses!";
    }
    
    void Start() {
        startPos = transform.position;
	rb = GetComponent<Rigidbody>();
	text = textGO.GetComponent<TextMesh>();
	text.text = "moar cubes!";
    }

    void Update() {
        float distance = Mathf.Abs(transform.position.y - startPos.y);
	if(distance >= pressLength){
	    transform.position = new Vector3(transform.position.x,
	    	    startPos.y - pressLength, transform.position.z);
            if (!pressed) {
                pressed = true;
		ButtonAction();
            }
        } else {
            pressed = false;
        }
        if (transform.position.y > startPos.y) {
            transform.position = new Vector3(transform.position.x,
	    	startPos.y, transform.position.z);
        }
    }
}
