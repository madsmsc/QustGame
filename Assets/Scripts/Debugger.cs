using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debugger : MonoBehaviour{
    public bool rightHandWatch;
    public Transform startRay;
    public Gun gun;
    public GameObject chars;
    public bool swish;

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
    private PulseSpawner pulseSpawner;

    void Start() {
	sign = FindObjectOfType<Sign>();
	watch = FindObjectOfType<Watch>();
	player = GameObject.Find("OVRPlayerController").transform;
	rightHand = GameObject.Find("RightHandAnchor").transform;
	StartPulseSpawner();
	StartRay();
	StartWatch();
	StartSwish();
    }

    void Update() {
	CalculateFPS();
	HandleKeys();
	ShowRay();
	Swish();
    }

    private float swishRot = 182;
    private float swishSign = -90;

    private void StartSwish(){
	if(!swish){
	    return;
	}
	Transform left = GameObject.Find("LeftHandAnchor").transform;
	left.position += new Vector3(0, -1, 0);
    }

    private void Swish(){
	if(!swish){
	    return;
	}
	if(swishRot < 180 || swishRot > 270){
	    swishSign *= -1;
	}
	swishRot += Time.deltaTime * swishSign;
	rightHand.transform.rotation = Quaternion.Euler(swishRot, 0, 0);
    }

    private void StartPulseSpawner(){
	if(!swish){
	    return;
	}
	GameObject pulseSpawnerGO = GameObject.Find("PulseSpawner");
	if(pulseSpawnerGO == null){
	    return;
	}
	pulseSpawner = pulseSpawnerGO.GetComponent<PulseSpawner>();
	if(pulseSpawner == null){
	    return;
	}
	pulseSpawner.transform.position = new Vector3(0, 2.2f, -3);
	Transform[] ts = { pulseSpawner.transform };
	pulseSpawner.points = ts;
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
