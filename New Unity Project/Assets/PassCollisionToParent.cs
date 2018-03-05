using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCollisionToParent : MonoBehaviour {
	private Rigidbody rb;

	void Start() {
	}

	void OnCollisionEnter() {
		Debug.Log ("AAAH");
		//rb.SendMessage ("OnCollisionEnter", col);
		gameObject.GetComponent<BoxCollider> ().attachedRigidbody.SendMessage ("OnCollisionEnter");
	}

	void OnTriggerEnter() {
		Debug.Log ("sfs");
	}
}
