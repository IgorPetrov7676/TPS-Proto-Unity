﻿using UnityEngine;
using System.Collections;

public class DamagebleObject : MonoBehaviour {

	public float health;

	public bool destructableObject = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual void Damage(float damage){
		if (destructableObject){
			health-= damage;
			if(health<0){
				KillIt();

			}
		}
	}
	public virtual void KillIt(){
		Destroy(gameObject);

	}
}