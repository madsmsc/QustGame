using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToRagdoll : MonoBehaviour {
    public Collider myCollider;
    public float respawnTime = 5f;

    public RuntimeAnimatorController con1, con2, con3;
    
    private Rigidbody[] rigidbodies;
    private bool isRagdoll = false;

    void Start() {
	rigidbodies = GetComponentsInChildren<Rigidbody>();
	ToggleRagdoll(true);
	RandomAnimation();
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
	    RandomAnimation();
	}
    }

    private IEnumerator GetBackUp(){
	yield return new WaitForSeconds(respawnTime);
	ToggleRagdoll(true);
    }

    private void RandomAnimation(){
	int randomNum = Random.Range(0,3);
	//Debug.Log("my random num: " + randomNum);
	Animator animator = GetComponent<Animator>();
	//animator.Play("woman");

	if(randomNum == 0){
	    //animator.SetTrigger("tracer2");
	    animator.runtimeAnimatorController = con1;
	} else if(randomNum == 1){
	    //animator.SetTrigger("woman");
	    animator.runtimeAnimatorController = con2;
	} else {
	    //animator.SetTrigger("Ch46_nonPBR@Angry");
	    animator.runtimeAnimatorController = con3;
	}
    }    
}
