using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookAttach : MonoBehaviour {

	// Used to allow the player to attach the Hook to the fishing rod's rope.

	// publically set
	public GameObject pole;
	public GameObject ropeCollider;	

	private Collider[] selfColliders;

	void Start() {
		selfColliders = GetComponentsInChildren<Collider>();
	}

	void OnTriggerEnter(Collider col) {

		// If Hook has a parent, {and it is a vive controller,}  
		// and it collides with Rod's rope, parent it to the rod beside rope.
		// Then delete all its physics and make it ungrabbable.

		if (transform.parent == null) {
			//if (transform.parent.name != "TreasureChest") {
				if (col.gameObject == ropeCollider) {					
					transform.SetParent(pole.transform);
					transform.localPosition = new Vector3 (0.18058f, -0.00129f, 0.04827f);

					Destroy(GetComponent<Rigidbody>());
					Destroy(GetComponent<MeshCollider>());		// remove one of these later
					Destroy(GetComponent<BoxCollider>());		// remove one of these later
					tag = null;

					foreach (Collider colliders in selfColliders) {
						colliders.enabled = false;
					}
				}
			//}
		}
	}
}
