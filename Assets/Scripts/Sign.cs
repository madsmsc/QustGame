using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour{
    public GameObject textGO;
    public GameObject cubePile;
    public GameObject cubeParent;
    private TextMesh text;    
    private int count = 0;
    
    public void Action(){
    	text.text = "button presses:\n" + (++count);
        Instantiate(cubePile, new Vector3(0,0,0),
		    Quaternion.identity, cubeParent.transform);
    }
    
    void Start() {
	text = textGO.GetComponent<TextMesh>();
	text.text = "shoot me for \nMOAR CUBES!";
    }
}
