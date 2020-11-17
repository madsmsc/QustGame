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
    private int score;
    private int combo;

    void Start() {

    }

    void Update() {    	 
	if(timer > beat){
	    GameObject cube = Instantiate(RandomCube(), RandomPoint());
	    cube.GetComponent<PulseCube>().speed = speed;
	    cube.transform.localPosition = Vector3.zero;
	    cube.transform.Rotate(transform.forward, RandomRotation());
	    timer -= beat;
	}
	timer += Time.deltaTime;
    }

    private GameObject RandomCube(){
	return cubes[Random.Range(0, cubes.Length)];
    }

    private Transform RandomPoint(){
	return points[Random.Range(0, points.Length)];
    }

    private float RandomRotation(){
	return 90 * Random.Range(0, 4);
    }
 
    public string GetScore(){
	return "score: " + score + "\ncombo: " + combo;
    }

    public void Missed(){
	combo = 0;
    }

    public void Hit(GameObject go){
	go.transform.parent.GetComponent<PulseCube>().Hit();
	combo++;	
	score += combo;
    }
}
