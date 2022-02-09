using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class littleKittenMove: MonoBehaviour {
	
	// Little Kitten movement behavior. Basically just "kittenMove" with a lemon twist.
	// Colliders for being pet are on "neck" and "Spine1"

	private Animator anim;
	private GameObject goal;
	private Transform leaf;

	private NavMeshAgent agent;

	private bool chasingAfterTarget = false;
	private bool catIsIdle = false;

	void Start () {
		anim = gameObject.GetComponent<Animator>();
		goal = GameObject.Find("Target_LittleKitten");
		leaf = GameObject.Find("Little_Kitten_Leaf").transform;
		agent = gameObject.GetComponent<NavMeshAgent>();
	}

	void FixedUpdate() {
		//  If Kitten's appropriate Target is not close by, and it is not already chasing it, chase it
		if (Mathf.Abs	(goal.transform.position.x - gameObject.transform.position.x) > 0.1f) {
			if (Mathf.Abs	(goal.transform.position.z - gameObject.transform.position.z) > 0.1f) {

				if (agent.enabled == true) {
					agent.destination = goal.transform.position;
				}

				if (!chasingAfterTarget) {
					ChaseTarget();
				}
			}
		}

		// ... Otherwise, make it idle, and disable its agent.
		else {			
			if (!catIsIdle) {	
				CatIdle();
			}
		}


		// Little Kitten moves if the Leaf it is hiding under is moved.
		if ( (agent.enabled == false) && (!catIsIdle) ) {				
				if (  (leaf.transform.position.x < -15f) || (leaf.transform.position.x > -14f) ||
					(leaf.transform.position.y > 7f) ||
					(leaf.transform.position.z > 6f) || (leaf.transform.position.z < 4f) ) {

					agent.enabled = true;
					anim.enabled = true;
					}
		}

	}

	// Just some bools and animations.
	void ChaseTarget() {
		chasingAfterTarget = true;
		catIsIdle = false;
		anim.Play("walk");
	}

	void CatIdle() {	
		
		chasingAfterTarget = false;	
		agent.enabled = false;
		catIsIdle = true;
		goal.SetActive(false);

		anim.Play("kitt_IdleSit2");

	}
}