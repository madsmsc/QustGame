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
    public int magSize;
    
    private int leftInMag;
    private int fireCD = 0;
    private AudioSource audioSource;    
    private TextMesh text;
    private ParticleSystem flash;
    private float reloading = 0;
    private Quaternion identity = new Quaternion(0f, 0f, 0f, 0f);
    
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

	Reload();
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
	DrawLaser();
	IncReload();
    }

    private void IncReload(){
	if(reloading > 0){
	    //Debug.Log("reloading="+reloading);
	    reloading += Time.deltaTime;
	    transform.GetChild(0).transform.rotation = identity;
	    transform.GetChild(0).transform.Rotate(new Vector3(reloading * 720.0f, 0, 0));
	    //Debug.Log("Gun Center= "+transform.GetChild(0).transform.rotation);
	}
	if(reloading >= 0.5f){
	    //Debug.Log("finished reloading");	    
	    reloading = 0;
	    transform.GetChild(0).transform.rotation.Set(0, 0, 0, 0);
	    leftInMag = magSize;
	    text.text = leftInMag.ToString();
	}
    }

    private void DrawLaser(){
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, muzzle.transform.position);
        lineRenderer.SetPosition(1, muzzle.transform.position +
				 muzzle.transform.forward * 5);
    }
    
    public bool OnCD() {
	return fireCD < fireRate;
    }

    public bool FullAuto() {
	return fireRate > 0;
    }

    public bool IsEmpty() {
	return leftInMag <= 0;
    }

    public void Reload() {
	//Debug.Log("Reloading!");
	reloading += 0.0001f;
    }

    private void Hit(GameObject target){
        if(target.CompareTag("Cube")){
	    Destroy(target);
	    GameObject ex1 = Instantiate(explosion);
	    ex1.transform.position = target.transform.position;
	} else if(target.CompareTag("Sign")){	
            //Debug.Log("sign.action()"); 
	    Sign sign = target.GetComponentInParent(typeof(Sign)) as Sign;
	    sign.Action();
	} else if(target.CompareTag("Woman")){
	    target.GetComponent<AnimationToRagdoll>().Die();
	} 
    }

    public void Fire() {
    	text.text = (--leftInMag).ToString();
        fireCD = 0;	
	flash.Play();
	audioSource.Play();
        RaycastHit hit;
        if (Physics.Raycast(muzzle.transform.position,
			    muzzle.transform.forward, out hit)){
	        GameObject target = hit.collider.gameObject;
		Hit(target);
	    }
    }
}
