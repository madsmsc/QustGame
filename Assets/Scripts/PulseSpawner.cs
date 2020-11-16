using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PulseSpawner : MonoBehaviour {
    public GameObject[] cubes;
    public Transform[] points;
    public float beat = 60.0f / 105.0f;
    public float speed = 2;
    
    private float timer;

    void Start() {
	
    }

    void Update() {
	if(timer > beat){
	    GameObject cube = Instantiate(cubes[Random.Range(0,2)],
					  points[Random.Range(0,4)]);
	    cube.GetComponent<PulseCube>().speed = speed;
	    cube.transform.localPosition = Vector3.zero;
	    cube.transform.Rotate(transform.forward,
				  90 * Random.Range(0,4));
	    timer -= beat;
	}
	timer += Time.deltaTime;
    }
}
