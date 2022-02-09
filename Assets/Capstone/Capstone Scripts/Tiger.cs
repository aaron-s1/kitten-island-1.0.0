using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tiger : MonoBehaviour {

	// Script for the Tiger that players spawn and ride to escape the island.

	// INVOKE MOUNT TIGER FROM SOMEWHERE ELSE

	// for moving and repositioning
	public GameObject rig;
	public GameObject tigerTarget;

	// for escaping area; publically set
	[Space(10)]
	public GameObject leaveMenu;
	public GameObject escapedScreen;
	public GameObject teleportArea;
	public Fade endGame;


	public bool mountable = false;
	public bool mounted = false;


	private Animator anim;
	private NavMeshAgent agent;

	private bool centerReached = false;
	//private int count = 0;


	public void Start() {
		agent = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
		//tigerTarget = GameObject.Find("Target_Tiger");
		//rig = GameObject.Find("[CameraRig]");

		//tigerTarget.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
		agent.destination = tigerTarget.transform.position;
		agent.enabled = false;
		Invoke("SpawnAndCenter", 3f);
	}


	void FixedUpdate() {

		// Keep updating position until the center has been reached
		if (!centerReached) {
			if (Vector3.Distance(transform.position, agent.destination) <= 0.5f) {
				Reposition();
			}
		}

		// Player has to be next to Tiger to mount it
		if (Vector3.Distance(transform.position, rig.transform.position) <= 1.5f) {
			if (!mountable) {
				leaveMenu.SetActive(true);
				mountable = true;
				// Debug.Log(mountable);
			}
		}

		else if (Vector3.Distance(transform.position, rig.transform.position) >= 1.5f) {
			if (mountable) {
				leaveMenu.SetActive(false);
				mountable = false;
				// Debug.Log(mountable);
			}
		}

	}


	public void SpawnAndCenter() {
		anim.SetTrigger("roarTrigger");
		agent.enabled = true;
		agent.destination = tigerTarget.transform.position;
	}


	void Reposition() {
		
		// Stop Update calls, resposition tiger closer to center
		centerReached = true;
		tigerTarget.transform.position = new Vector3 (1.07f, 7.45f, 8f);
		agent.SetDestination(tigerTarget.transform.position);

		//agent.destination = tigerTarget.transform.position;
		Invoke("PrepForMount", 1.5f);

	}

	void PrepForMount() {

		// Set idle animation and indicate that the tiger is now ready 
		anim.SetTrigger("idleTrigger");
		agent.enabled = false;
		transform.GetChild(3).gameObject.SetActive(true);	// turn on small green aura

		mountable = true;

		// TEMP FOR TESTING. SET TRIGGER ANOTHER WAY.
		//Invoke("MountTiger", 5f);

		//mountable = true;



	}
		

	public void MountTiger() {

		// ref. Update() -- must be near tiger for "mountable" to be True
		if (mountable) {
			// prevent teleport movement, turn off aura
			leaveMenu.SetActive(false);
			mounted = true;
			Destroy(teleportArea);
			transform.GetChild(3).gameObject.SetActive(false);	


			// mount camera to tiger
			rig.transform.parent = gameObject.transform;
			rig.transform.localPosition =  new Vector3 (0f, -0.16f, -0.32f);
			rig.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);


			anim.SetTrigger("idleRoarTrigger");
			InvokeRepeating("ZoomZoomImATiger", 2.2f, 0.01f);	// numbers were selected carefully
			Camera.main.GetComponent<AudioSource>().time = 0.5f;
			Camera.main.GetComponent<AudioSource>().Play();
		}
		
	}


	public void ZoomZoomImATiger() {
		// Tiger/player start running, fly to safety
		// Do not change any numbers here. They make sure the player does not see outside the boundary area when the game ends.

		anim.SetTrigger("runTrigger");


		transform.Translate(Vector3.left * 0.001f);  // 0.00104
		transform.Translate(Vector3.forward * 0.04f);

		if (transform.position.z >= 25f) {
			transform.Translate(Vector3.up * 0.003f); // 0.00385
		}

		endGame.Invoke("EndGame", 15f);

	}
}

