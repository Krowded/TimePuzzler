using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour {
	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player") {
			Debug.Log ("You win!");
		}
	}
}
