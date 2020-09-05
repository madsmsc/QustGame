using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGun : MonoBehaviour { 
    public int damage = 1;
    public float fireRate = 1;
    public ParticleSystem muzzleParticle;
    public AudioSource gunFire;

    private float timer = 0;

    void Update() {
        timer += Time.deltaTime;
        if (timer >= fireRate && Input.GetButton("Fire1"))
            fireGun();
    }

    private void fireGun() {
        timer = 0;
        fireEffects();
        checkRayIntersections();
    }

    private void checkRayIntersections()
    {
        Debug.DrawRay(Camera.main.transform.position,
        Camera.main.transform.forward * 100, Color.red, 2);
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            GameObject go = hitInfo.collider.gameObject;
            //Debug.Log("ray hit " + go.name);
            Health health = go.GetComponent<Health>();
            if (health != null)
                health.takeDamage(damage);
        }
    }

    private void fireEffects()
    {
        muzzleParticle.Play();
        gunFire.Play();
    }
}
