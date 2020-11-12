using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToRagdoll : MonoBehaviour {
    public Collider myCollider;
    public float respawnTime = 5f;
    public AnimationController aCon;
    
    private Rigidbody[] rigidbodies;
    private bool isRagdoll = false;

    void Start() {
	rigidbodies = GetComponentsInChildren<Rigidbody>();
	ToggleRagdoll(true);
	SetAnimation();
    }

    public void Die(){
	ToggleRagdoll(false);
	StartCoroutine(GetBackUp());
    }

    private void ToggleRagdoll(bool isAnimating){
	isRagdoll = !isAnimating;
	myCollider.enabled = isAnimating;
	foreach(Rigidbody ragdollBone in rigidbodies){
	    ragdollBone.isKinematic = isAnimating;
	}
	GetComponent<Animator>().enabled = isAnimating;
	if(isAnimating){
	    SetAnimation();
	}
    }

    private IEnumerator GetBackUp(){
	yield return new WaitForSeconds(respawnTime);
	ToggleRagdoll(true);
    }

    private void SetAnimation(){
	GetComponent<Animator>().runtimeAnimatorController =
	    aCon.RandomAnimation();
    }    
}
