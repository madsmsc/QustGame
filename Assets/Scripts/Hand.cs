using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour {
    /* TODO
       implementer multiplayer https://xr.berkeley.edu/decal/tutorials/photon
       byg til steamvr og test at det virker med virtual desktop
       tjek VR tunnelling pro asset i unity store.
       'This API is obsolete, and should no longer be used. Please use XRManagerSettings in the XR Management package instead.'
    */
    
    public OVRInput.Controller controller;
    public Vector3 holdPosition = new Vector3(0, -0.025f, 0.03f);
    public Vector3 holdRotation = new Vector3(0, 0, 0);

    private float indexTriggerState = 0;
    private float handTriggerState = 0;
    private float oldIndexTriggerState = 0;
    private GameObject gun = null;
    private bool bDown = false;

    /* OVRInput.Button.One = A
       OVRInput.Button.Two = B */    

    void Start() {

    }

    void Update() {
    	oldIndexTriggerState = indexTriggerState;
    	indexTriggerState = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller);
    	handTriggerState = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller);	
	if (HoldingGun()) {
	    HandleGun();
	}
    }

    private void HandleGun(){
	Gun gunScript = gun.GetComponent<Gun>();
	if(gunScript.IsEmpty()){
	    return;
	}    
	bool pulledTrigger = indexTriggerState > 0.9f && oldIndexTriggerState < 0.9f;
	bool autoFiring = indexTriggerState > 0.9 && gunScript.FullAuto() && !gunScript.OnCD();
	if (pulledTrigger || autoFiring){
	    gunScript.Fire();
	}
	if (handTriggerState < 0.9f){
	    Release();
	}
	if (!bDown && OVRInput.Get(OVRInput.Button.Two, controller)){
	    bDown = true;
	    gunScript.Reload();
	} if (bDown && !OVRInput.Get(OVRInput.Button.Two, controller)){
	    bDown = false;
	}
    }

    bool HoldingGun() {
	return gun != null;
    }

    void Release() {
	gun.transform.parent = null;
	Rigidbody rigidbody = gun.GetComponent<Rigidbody>();
	rigidbody.useGravity = true;
	rigidbody.isKinematic = false;
	rigidbody.velocity = OVRInput.GetLocalControllerVelocity(controller);
	gun.GetComponent<Gun>().Disable();
	gun = null;	
    }

    void OnTriggerStay(Collider other) {
        if (other.CompareTag("Gun")) {
            if (handTriggerState > 0.9f && !HoldingGun()) {
                print("Grabbing gun.");
		Grab(other.gameObject);
            }
        }
    }
 
    void Grab(GameObject obj) {
    	gun = obj;
    	gun.transform.parent = transform;
	gun.transform.localPosition = holdPosition;
	gun.transform.localEulerAngles = holdRotation;
	gun.GetComponent<Rigidbody>().useGravity = false;
    	gun.GetComponent<Rigidbody>().isKinematic = true;
	gun.GetComponent<Gun>().Enable(); 
    }
}
