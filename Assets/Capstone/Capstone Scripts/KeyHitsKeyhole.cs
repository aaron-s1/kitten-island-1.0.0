using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHitsKeyhole : MonoBehaviour {

	//

	public GameObject keyhole;
	public GameObject lid;

	private int count = 0;
	private int lidCount = 0;


	private int topCollider;
	private bool inserted = false;
	private Collider[] chestColliders;


	void Start() {
		keyhole = GameObject.Find("Keyhole");
		lid = GameObject.Find("TreasureChest_Lid");
		chestColliders = GameObject.Find("TreasureChest").GetComponents<BoxCollider>();
	}



	void OnTriggerEnter(Collider col) {
			if (!inserted) {
				if (col.gameObject.tag == "Keyhole") {
					Debug.Log("collider fired");
					KeyInserted();								
			}

		}
	}



	void KeyInserted() {

		// Find top inner containment collider of Chest and delete it to ensure objects can be grabbed
		foreach(BoxCollider topOfBox in chestColliders) {
			topCollider++;

			if (topCollider == 7) {
				topOfBox.enabled = false;
			}

		}

		// Key: Remove physics, remove from player's hand, lock it into chest, and prevent it from being grabbed
		GetComponent<Rigidbody>().isKinematic = true;
		inserted = true;
		transform.parent = null;
		tag = "Untagged";
		gameObject.transform.SetParent(keyhole.transform);


		// ATTEMPT AT +(90 DEGREE ANGLE OPENING
		//gameObject.transform.position = new Vector3 (keyhole.transform.position.x - 0.015f, keyhole.transform.position.y - 0.026f, keyhole.transform.position.z - 0.01f);
		gameObject.transform.position = new Vector3 (keyhole.transform.position.x - 0.01f, keyhole.transform.position.y - 0.01f, keyhole.transform.position.z + 0.021f);
		transform.localRotation = Quaternion.Euler(-10f, -90f, 200f);

		InvokeRepeating("OpenChest", 1f, 0.02f);
	}
	


	void OpenChest() {
		// Rotate key, begin opening lid
		GetComponent<AudioSource>().Play();

		if (count < 40) {			
			transform.localRotation	*= Quaternion.Euler(0.8f, 2f, -0.7f);
			transform.localPosition = new Vector3 (transform.localPosition.x + 0.005f, transform.localPosition.y + 0.0012f, 
								transform.localPosition.z + 0.035f);
			count++;
		}

		else {
			CancelInvoke();
			InvokeRepeating("OpenLid", 0, 0.04f);
		}			
	}
	

	void OpenLid() {		
		// Rotate lid
		if (lidCount < 40) {
			lid.transform.localRotation *= Quaternion.Euler(-2f, 0f, 0f);
			lidCount++;
		}

		else {
			CancelInvoke();
		}
	}

}

/*
  
  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHitsKeyhole : MonoBehaviour {

	private bool inserted = false;
	public GameObject keyhole;
	public GameObject lid;
	private int count = 0;
	private int lidCount = 0;
	public float tomato;


	void Start() {
		keyhole = GameObject.Find("Keyhole");
		lid = GameObject.Find("TreasureChest_Lid");

	}
										// if key collides with something
	void OnTriggerEnter(Collider col) {
	//void OnCollisionEnter(Collision col) {
		//if (gameObject.transform.parent != null) {			// ... and it is being held by controller

			
			if (!inserted) {
				if (col.gameObject.tag == "Keyhole") {		// ... and that thing is a keyhole					
					Debug.Log("collider fired");
					KeyInserted();								
			}

		}
		//}
	}



	void KeyInserted() {		

		GetComponent<Rigidbody>().isKinematic = true;			// key physics can not be accidentally altered
		inserted = true;						
		transform.parent = null;					// remove key from player's hand
		tag = "Untagged";						// key can no longer be grabbed
		//GetComponent<Rigidbody>().useGravity = false;

		//gameObject.transform.parent = keyhole.transform;		// key position is properly 'locked' into keyhole
		gameObject.transform.SetParent(keyhole.transform);


		// ATTEMPT AT +(90 DEGREE ANGLE OPENING
		//gameObject.transform.position = new Vector3 (keyhole.transform.position.x - 0.015f, keyhole.transform.position.y - 0.026f, keyhole.transform.position.z - 0.01f);
		gameObject.transform.position = new Vector3 (keyhole.transform.position.x - 0.01f, keyhole.transform.position.y - 0.01f, keyhole.transform.position.z + 0.021f);
		transform.localRotation = Quaternion.Euler(-10f, -90f, 200f);



		//transform.localRotation = Quaternion.Euler(5f, 70f, -60f);



		//tomato = transform.rotation.x;

		//gameObject.transform.localRotation = Quaternion.Euler(200f, 90f, 20f);



		//transform.localPosition = new Vector3(0f, -5000f, 0f);
		//transform.localposition = new Vector3(-0.4f, -155.82f, -12.84f);


		//transform.eulerAngles = new Vector3(200f, 93f, 20f);
		//transform.rotation = new Vector3(200f, 93f, 20f);
		//transform.localRotation = Quaternion.Euler(200f,93f,20f);
		//transform.LookAt(thing.transform);



		//transform.rotation = Quaternion.Euler(380f, 185f, 30f);



		InvokeRepeating("OpenChest", 4f, 0.02f);

		//Debug.Log("key inserted fired");
	}



	void OpenChest() {
		// rotate key, begin opening lid
		GetComponent<AudioSource>().Play();

		if (count < 40) {			
			//transform.localRotation *= Quaternion.Euler(0.6f, 2f, -0.6f);
			//transform.rotation *= Quaternion.Euler(-2.625f, -1.75f, 0f); //2.75f, 1.25f); 
			//transform.Rotate(Vector3.left * Time.deltaTime * 10);
			//transform.localRotation *= Quaternion.Euler(1.3125f, 0f, 0f); // 1.375f, 0.625f);
			//transform.localRotation *= Quaternion.Euler(1.3125f, 0f, 0f);
			//transform.localRotation *= Quaternion.Euler (1.3125f, 0f, 0f);
			//-10  -90  200
			//-19.684	-176.384  190.628
			transform.localRotation	*= Quaternion.Euler(0.8f, 2f, -0.7f);
			//transform.localPosition	*= Quaternion.Euler(0f, 0f, 0f);
			//transform.localPosition -= 

			transform.localPosition = new Vector3 (transform.localPosition.x + 0.005f, transform.localPosition.y + 0.0012f, 
								transform.localPosition.z + 0.035f);
			/*
			transform.localPosition = new Vector3 (transform.localPosition.x + 0.006f, transform.localPosition.y + 0.0016f, 
								transform.localPosition.z + 0.022f);/**/

// rotation

//tomato -= 2f;
//tomato--;
//transform.rotation = new Vector3(transform.rotation.x - 2f, 0f, 0f);
//transform.position.x -= 0.00025f;
//count++;
//}

//else {
	//CancelInvoke();
	//InvokeRepeating("OpenLid", 0, 0.04f);
//}			
//}


//void OpenLid() {		
	// rotate lid

//	if (lidCount < 40) {
//		lid.transform.localRotation *= Quaternion.Euler(-2f, 0f, 0f);
//		lidCount++;
//	}
//
//	else {
//		CancelInvoke();
//	}
//}

//}


