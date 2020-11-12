using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugController : MonoBehaviour{
    public Sign sign;
    public WatchController watch;
    public Transform rightHand;
    public bool rightHandWatch;
    public Transform player;
    public Transform startRay;
    public Gun gun;
    public GameObject chars;
    public TeleportController teleport;
    
    private bool qDown = false; // sign action
    private bool eDown = false; // move and fire gun
    private bool fDown = false; // char animation
    private bool gDown = false; // reload gun

    private float deltaTime = 0.0f;
    private float rotateSpeed = 1;
    private float moveSpeed = 0.01f;

    void Start() {
	StartRay();
	StartWatch();
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
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.005f;
        lineRenderer.positionCount = 2;
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
    }	

    private void ShowRay(){
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

    void CalculateFPS(){
	deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
	int fps = (int)(1.0f / deltaTime);	
	watch.UpdateText();
	watch.AddText("\nfps:" + fps);
	watch.AddText("\ng: reload");
	watch.AddText("\nt: activate");
	watch.AddText("\nq: sign");
	watch.AddText("\ne: fire");
	watch.AddText("\nf: chars");
	watch.AddText("\nwasd: move");
	watch.AddText("\ntd: "+teleport.dist);
    }

    private void CharAnimation(){
	AnimationToRagdoll[] atrs =
	    chars.GetComponentsInChildren<AnimationToRagdoll>();
	foreach(AnimationToRagdoll atr in atrs){
	    atr.Die();
	}
    }

    private void HandleKeys(){	
	if (!gDown && Input.GetKey("g")){
	    gDown = true;
	    gun.Reload();
	} if (gDown && !Input.GetKey("g")){
	    gDown = false;
	}

	if (!qDown && Input.GetKey("q")){
	    qDown = true;
	    sign.Action();
	} if (qDown && !Input.GetKey("q")){
	    qDown = false;
	}

	if (!eDown && Input.GetKey("e")){
	    eDown = true;
	    FireGun();
	} if (eDown && !Input.GetKey("e")){
	    eDown = false;    
	}

	if (!fDown && Input.GetKey("f")){
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
