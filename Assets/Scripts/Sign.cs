using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour{
    public GameObject cubePile;
    public GameObject cubeParent;

    private TextMesh text;    
    private int count = 0;

    void Start() {
	text = GetComponentInChildren<TextMesh>();
	text.text = "shoot me for \nmoar cubes!";
    }

    public void Action(){
    	text.text = "button presses:\n" + (++count);
        Instantiate(cubePile, new Vector3(0,0,0),
		    Quaternion.identity, cubeParent.transform);
    }
}
