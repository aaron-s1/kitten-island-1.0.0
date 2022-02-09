using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class Grab : MonoBehaviour
{

	[Space(10)]
	// Achievement menu control, plus achievement unlocks. Assigned via inspector.
	public GameObject achievementMenu;
	public AchievementsUnlock achievementMenuUnlock;
	public GameObject achievementOverlay;

	// Little kitten and for detecting if it is being pet or not.
	[Space(10)]
	public GameObject littleKitten;
	public GameObject kittenNeck;
	public GameObject kittenSpine;
	[Space(5)]
	public bool touchingNeck = false;
	public bool touchingSpine = false;

	// For poison bottle, 'you died' screen, and killing player
	[Space(10)]
	public Fade triggerFade;	// set via inspector
	public GameObject chest;
	public GameObject bottle;
	public GameObject cork;
	public bool bottleHeld = false;

	[Space(10)]
	public Tiger tiger;
	public GameObject leaveButton;

	
	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device device;
	// for making coconuts make sound when hitting island
	private Coconuts thump;			


	void Awake() {		
		littleKitten = GameObject.FindWithTag("LittleKitten");
		kittenNeck  = GameObject.FindWithTag("KittenNeck");
		kittenSpine = GameObject.FindWithTag("KittenSpine");	

		chest = GameObject.Find("TreasureChest");
		bottle = GameObject.FindWithTag("Bottle");
		cork = GameObject.FindWithTag("cork");

		UnlockKitten();
	}


	void OnEnable()
	{				
		trackedObj  = GetComponent<SteamVR_TrackedObject>();
		UnscrewCork();
	}


	void Update() {
		
		device = SteamVR_Controller.Input((int)trackedObj.index);

		// App button pauses/plays game, and turns on/off Achievement Menu
		if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu)) {
			if (achievementMenu.activeSelf == true) {				
				achievementMenu.SetActive(false);
				Time.timeScale = 1;
				System.GC.Collect();
			}

			if (achievementMenu.activeSelf == false) {
				Time.timeScale = 0;
				achievementMenu.SetActive(true);
			}
		}
	}


	// TRIGGER ENTER OR TRIGGER STAY?
	void OnTriggerStay(Collider col) {

		if (col.gameObject.CompareTag("Bottle")) {
			bottleHeld = true;
		}



		// Certain objects can be grabbed and thrown.
		if (col.gameObject.CompareTag("Grabbable") || (col.gameObject == chest) )
		{

			// Return normal physics to grabbable objects
			if (device.GetPressUp(SteamVR_Controller.ButtonMask.Grip))		// CHANGE TO TRIGGER?
			{
				// REMOVE LATER
				Debug.Log("You have released the trigger");

				col.transform.SetParent(null);
				Rigidbody rigidBodyUp = col.GetComponent<Rigidbody>();

				rigidBodyUp.isKinematic = false;
				rigidBodyUp.useGravity = true;
				rigidBodyUp.velocity = device.velocity * -1f;		// ?
				rigidBodyUp.angularVelocity = device.angularVelocity;	// ?
			}

			else if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))		// CHANGE TO TRIGGER?
			{
				// REMOVE LATER
				Debug.Log("You are touching down the trigger on an object");


				// If you are holding the bottle, and touch the cork, it pops off, killing you.
				if (col.gameObject.CompareTag("cork")) {
					if (bottleHeld) {
						UnscrewCork();
					}
				}

				// Change to grab mechanics
				else {
					Rigidbody rigidBodyDown = col.GetComponent<Rigidbody>();
					rigidBodyDown.isKinematic = true;
					rigidBodyDown.useGravity = false;

					col.transform.SetParent(gameObject.transform);

					//device.TriggerHapticPulse(500);
				}
			}
		}

		if (col.gameObject == leaveButton) {
			Invoke("tiger.MountTiger", 2f);
		}

	}



	// Just checks to see if Little Kitten is being pet properly.
	void OnTriggerEnter(Collider other) {
		// Check neck, *then* spine, to make sure cruel people aren't petting the Little Kitten backwards.
		// Collider locations: Little Kitten -> kittenRoot -> Spine -> 	{Spine1} and {Neck}
		if (other.name == kittenNeck.name) {
			touchingNeck = true;
		}
		else {
			touchingNeck = false;
		}

		if (other.name == kittenSpine.name) {
			touchingSpine = true;
		}
		else {
			touchingSpine = false;
		}


		if (touchingNeck) {
			if (touchingSpine) {
				UnlockKitten();
			}
		}




	}


	// ... If it is, these 2 cause it to meow, unlock its achievement, then show/hide an 'achievement unlocked' popup.
	public void UnlockKitten() {
		littleKitten.GetComponent<Animator>().Play("kitt_Meow");
		littleKitten.GetComponent<AudioSource>().Play();

		achievementMenuUnlock.CatAchievementUnlocked();
		achievementOverlay.SetActive(true);
		Invoke("HideOverlay", 3f);
	}

	public void HideOverlay() {
		achievementOverlay.SetActive(false);
	}



	// These 3 basically just make the Cork pop out, then make it a normal a grabbable object. 
	// Also, Bottle can only be opened while being held, and it gets dropped if it's opened (because you die).
	void UnscrewCork() {
		// Cork makes pop sound, poison puff plays, kills player
		cork.GetComponent<AudioSource>().Play();
		bottle.transform.GetChild(0).gameObject.SetActive(true);
		triggerFade.Fading();
		InvokeRepeating("CorkMove", 0, 0.01f);		
	}
		
	void CorkMove() {
		// Cork pops off and flies away because his people need him.
		if (cork.gameObject.transform.localPosition.z <= 15f) {
			cork.gameObject.transform.localPosition = new Vector3 (0f, 0f, cork.gameObject.transform.localPosition.z + 0.08f);			
		}


		// Make bottle be dropped (that tends to happen when you die). Make cork a normal grabbable object.
		else {
			CancelInvoke("CorkMove");

			bottle.transform.SetParent(null);
			bottle.GetComponent<Rigidbody>().useGravity = true;
			bottle.GetComponent<Rigidbody>().isKinematic = false;

			cork.transform.SetParent(null);
			cork.GetComponent<Rigidbody>().useGravity = true;
			cork.GetComponent<Rigidbody>().isKinematic = false;

			cork.gameObject.tag = "Grabbable";		
		}
	}
		
	void OnTriggerExit(Collider col) {
		bottleHeld = false;
	}


	// Called from SpawnFish.cs when a fish is spawned to tell the player a fish has been spawned
	public void TriggerHaptic() {
		//SteamVR_Controller.Input((int)trackedObj.index).TriggerHapticPulse(500);
		device.TriggerHapticPulse(500);
	}

}