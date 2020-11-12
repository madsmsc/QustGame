using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchController : MonoBehaviour{
    public TextMesh text;
    private int hp = 100;
    private int pp = 20;

    void Start () {
	UpdateText();
    }

    void Update() {
    }

    public void AddText(string s){
	text.text += s;
    }

    public void UpdateText(){
	text.text = "hp: " + hp + "\npp: " + pp;
    }	
}
