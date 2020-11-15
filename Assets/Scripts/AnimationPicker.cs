using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPicker : MonoBehaviour {
    public RuntimeAnimatorController[] controllers;

    void Start() {
    }

    public RuntimeAnimatorController RandomAnimation(){
	int r = Random.Range(0, controllers.Length);
	// Debug.Log("my random num: " + r);
	return controllers[r];
    }    
}
