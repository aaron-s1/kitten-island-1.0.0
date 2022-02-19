using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFish : MonoBehaviour {

	// This goes on the edge of the Hook. Spawns fish on edge of Hook, given conditions.

	public GameObject pole;
	public GameObject hookEnd;

	[Space(10)]
	public GameObject fish;
	public GameObject goldenFish;

	//[Space(10)]
	//public int fishFed = 0;	

	[Space(10)]
	public AchievementsUnlock achievementMenuUnlock;
	public GameObject achievementOverlay;

	[Space(10)]
	// public Grab hapticLeft;
	// public Grab hapticRight;


	private int timeCount = 0;
	private int fishFished = 0;
	private bool alreadyFished = false;


	void Start () {		
		//spawnedFish = Instantiate(fish, transform.position, transform.rotation).SetActive(true);
		//InvokeRepeating("Taco", 5f, 3f);
	}
	

	void OnTriggerStay(Collider other) {
		if (!alreadyFished) {
			
			if (other.tag == "boundary") {
				timeCount++;

				if (timeCount <= 300) {		// ADJUST TIME
					fishFished++;

					if (fishFished == 5) {			
						Instantiate(goldenFish, hookEnd.transform).SetActive(true);
						// hapticLeft.TriggerHaptic();
						// hapticRight.TriggerHaptic();
						UnlockFish();

					}

					else {
						Instantiate(fish, hookEnd.transform).SetActive(true);
						// hapticLeft.TriggerHaptic();
						// hapticRight.TriggerHaptic();
					}
					
					alreadyFished = true;	// prevents additional spawns until Hook leaves water
				}
			}
		}
	}

	public void UnlockFish() {		
		achievementMenuUnlock.FishAchievementUnlocked();
		achievementOverlay.SetActive(true);
		Invoke("HideOverlay", 3f);
	}

	public void HideOverlay() {
		achievementOverlay.SetActive(false);
	}


	void OnTriggerEnter (Collider other) {
		
	}

	
	void OnTriggerExit (Collider other) {
		if (other.tag == "boundary") {			
			timeCount = 0;
			alreadyFished = false;


		}
	}

	/*
	public void Taco() {
		Instantiate(fish, hookEnd.transform).SetActive(true);
	}*/




}
