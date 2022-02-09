using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class kittenMove: MonoBehaviour {

	// Handles Kitten movement.

	public Transform goal;
	public Animator anim;
	//public bool tigerSpawnable = true;

	private bool chasingAfterTarget = false;
	private bool catIsIdle = false;
	private NavMeshAgent agent;
	private string catNumber;


	void Start () {

		catNumber = gameObject.name.Substring(gameObject.name.Length - 1);

		agent = GetComponent<NavMeshAgent>();
		anim = gameObject.GetComponent<Animator>();
		goal = GameObject.Find("Target_" + catNumber).transform;

	}


	void FixedUpdate() {
		//  If Kitten's appropriate Target is not close by, and it is not already chasing it, chase it
		if (Mathf.Abs	(goal.transform.position.x - gameObject.transform.position.x) > 0.1f) {
			if (Mathf.Abs	(goal.transform.position.z - gameObject.transform.position.z) > 0.1f) {
				//if (agent.enabled == true) {
				if (!catIsIdle) {
					agent.destination = goal.position;
				}
				//}
				if (!chasingAfterTarget) {
					ChaseTarget();
				}
			}
		}

		// ... Otherwise, if it is not already Idle, it becomes Idle
		else {
			//agent.destination = goal.position;
			if (!catIsIdle) {	
				CatIdle();
			}
		}

	}

	// These are just some movement bools and animations.
	void ChaseTarget() {
		chasingAfterTarget = true;
		catIsIdle = false;
		anim.Play("walk");
	}

	void CatIdle() {	
		chasingAfterTarget = false;	
		catIsIdle = true;
		anim.Play("kitt_IdleSit");
	}
}