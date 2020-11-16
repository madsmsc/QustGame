using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debugger : MonoBehaviour{
    public bool rightHandWatch;
    public Transform startRay;
    public Gun gun;
    public GameObject chars;
    public bool start0;

    private Sign sign;
    private Transform player;
    private Transform rightHand;
    private Watch watch;
    private bool qDown = false; // sign action
    private bool eDown = false; // move and fire gun
    private bool fDown = false; // char animation
    private bool gDown = false; // reload gun
    private float deltaTime = 0.0f;
    private float rotateSpeed = 1;
    private float moveSpeed = 0.01f;
    private string staticText =	
    	"\nf: chars" +
	"\nq: sign" + 
	"\ng/B: reload" +
	"\nt/A: activate" + 
	"\ne/trigger: fire" +
	"\nwasd/thumbstick: move";
    private int fps;

    void Start() {
	sign = FindObjectOfType<Sign>();
	watch = FindObjectOfType<Watch>();
	player = GameObject.Find("OVRPlayerController").transform;
	rightHand = GameObject.Find("RightHandAnchor").transform;
	StartRay();
	StartWatch();
	if(start0){
	    player.transform.position =
		new Vector3(0, 2, 0);
	}
    }

    void Update() {
	CalculateFPS();
	HandleKeys();
	ShowRay();
    }

    private void StartWatch(){	
    	if(rightHandWatch){
	    watch.transform.parent = rightHand;
	    watch.transform.rotation *= Quaternion.Euler(0, 180, 0);
	}
    }
    
    private void StartRay(){
	if(startRay == null){
	    return;
	}
	LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
	lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
	lineRenderer.widthMultiplier = 0.005f;
	lineRenderer.positionCount = 2;
	lineRenderer.startColor = Color.red;
	lineRenderer.endColor = Color.red;
    }	
    
    private void ShowRay(){
	if(startRay == null){
	    return;
	}
	LineRenderer lineRenderer = GetComponent<LineRenderer>();
	lineRenderer.SetPosition(0, startRay.position);
	lineRenderer.SetPosition(1, startRay.position +
				 startRay.forward * 5);
    }
    
    void FireGun(){
	gun.transform.position = startRay.position;
	gun.transform.rotation = startRay.rotation;	
	gun.Fire();
    }

    public string GetText(){
	return "\nfps: " + fps + staticText;
    }
    
    private void CalculateFPS(){
	deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
	fps = (int)(1.0f / deltaTime);
    }

    private void CharAnimation(){
	AnimationToRagdoll[] atrs =
	    chars.GetComponentsInChildren<AnimationToRagdoll>();
	foreach(AnimationToRagdoll atr in atrs){
	    atr.Die();
	}
    }

    private void HandleKeys(){	
	if (gun != null && !gDown && Input.GetKey("g")){
	    gDown = true;
	    gun.Reload();
	} if (gDown && !Input.GetKey("g")){
	    gDown = false;
	}

	if (sign != null && !qDown && Input.GetKey("q")){
	    qDown = true;
	    sign.Action();
	} if (qDown && !Input.GetKey("q")){
	    qDown = false;
	}

	if (gun != null && !eDown && Input.GetKey("e")){
	    eDown = true;
	    FireGun();
	} if (eDown && !Input.GetKey("e")){
	    eDown = false;    
	}

	if (chars != null && !fDown && Input.GetKey("f")){
	    fDown = true;
	    CharAnimation();
	} if (fDown && !Input.GetKey("f")){
	    fDown = false;    
	}

	if (Input.GetKey("w")){
	    player.position += player.forward * moveSpeed;
	} if (Input.GetKey("s")){
	    player.position -= player.forward * moveSpeed;
	} if (Input.GetKey("a")){
	    player.rotation *= Quaternion.Euler(0, -rotateSpeed, 0);
	} if (Input.GetKey("d")){
	    player.rotation *= Quaternion.Euler(0, rotateSpeed, 0);	    
	}
    }
}