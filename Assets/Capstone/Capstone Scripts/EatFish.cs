using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EatFish : MonoBehaviour {

	// Handles eating of Fish by Kittens, and handles Tiger transforming.

	// top 2 assigned via inspector
	public GameObject tiger;
	public Material goldenMat;
	public GameObject[] allKittens;

	//public kittenMove kittenMoveReference;

	// Despite inferring this works with any fish, it only applies to non-golden fish
	public int fishFed = 0;

	public bool tigerIsSpawnable = true;

	void Start () {		
		GetComponentInChildren<UnityEngine.UI.Text>().text = "";
		allKittens = GameObject.FindGameObjectsWithTag("Kitten");
	}

	public void OnTriggerEnter(Collider col) {
		
		// Unparent fish from controller before deactivating it, add fish currently fed to cat overhead
		if (col.gameObject.name == "Fish") {
			if (col.gameObject.transform.parent != null) {
				col.gameObject.transform.parent = null;	// just in case
				col.gameObject.SetActive(false);

				fishFed++;
				GetComponentInChildren<UnityEngine.UI.Text>().text = fishFed.ToString();
			}


			// If a kitten gets fed 3 fish, and no tigers are active, it transforms into a tiger.
			// (TEXT UI does not need its count reset (to " ") since it's attached to the kitten and the kitten gets disabled)
			if (  (fishFed == 3)  &&  (tigerIsSpawnable)  ) {				
				tiger.transform.parent = null;		// just in case
				tiger.SetActive(true);
				tiger.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z);
				tiger.transform.rotation = gameObject.transform.rotation;

				gameObject.SetActive(false);

				// Then, prevent all remaining kittens from becoming tigers.
				foreach (GameObject kitten in allKittens) {
					//kitten.GetComponent<kittenMove>().tigerSpawnable = false;
					kitten.GetComponent<EatFish>().tigerIsSpawnable = false;
				}							
			}
		}


		// Same as before, except it can't assist in making Tigers, and instead just makes a cat golden
		else if (col.gameObject.name == "Golden Fish") {			
			if (col.gameObject.transform.parent != null) {
				col.gameObject.transform.parent = null;	 // just in case
				col.gameObject.SetActive(false);
				gameObject.GetComponent<Renderer>().material = goldenMat;
			}
		}
	}

}