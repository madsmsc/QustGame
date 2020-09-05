using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour {
    public AudioClip clip;
    public GameObject textGO;
    public GameObject explosion;
    public GameObject flashGO;
    public GameObject muzzle;
    public int fireRate = 0; // how many 1/70s between shots
    
    private int fireCD = 0;
    private AudioSource audioSource;    
    private TextMesh text;
    private ParticleSystem flash;

    void Start () {
    	audioSource = gameObject.AddComponent<AudioSource>();
	    audioSource.clip = clip;
	    flash = flashGO.GetComponent<ParticleSystem>();
        text = textGO.GetComponent<TextMesh>();
	    text.text = "0";

        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.005f;
        lineRenderer.positionCount = 2;
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.green;

        Disable();
    }

    public void Disable() {
        textGO.SetActive(false);
	    flashGO.SetActive(false);
    }

    public void Enable() {
        textGO.SetActive(true);
        flashGO.SetActive(true);
    }

    void Update() {
	    if(FullAuto() && OnCD()){
	        fireCD++;
	    }
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, muzzle.transform.position);
        lineRenderer.SetPosition(1, muzzle.transform.position + muzzle.transform.forward * 5);
    }

    public bool OnCD() {
	    return fireCD < fireRate;
    }

    public bool FullAuto() {
	    return fireRate > 0;
    }

    private int shots = 0;

    public void Fire() {
    	text.text = (++shots).ToString();
        fireCD = 0;	
	    flash.Play();
	    audioSource.Play();
        RaycastHit hit;

        if (Physics.Raycast(muzzle.transform.position, muzzle.transform.forward, out hit)){
	        GameObject target = hit.collider.gameObject;
	        if(target.CompareTag("Cube")){
	            Destroy(target);
		        GameObject ex1 = Instantiate(explosion);
		        ex1.transform.position = target.transform.position;
	        }
	    }
    }
}
