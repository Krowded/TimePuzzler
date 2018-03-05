using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputControl;

namespace Player
{	
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(ParticleSystem))]
	public class PlayerMovement : MonoBehaviour {
		public bool Selected = false;
		public bool TimeOn = false;

		private Rigidbody rb;
		private Transform tf;

		public Color color;

		public Vector3 StartingPosition;
		public float Speed;
		public float RotationSpeed;
		public float JumpStrength;
		public float LandedVelocity;
		public float LandedDistance;
		public float MarginForJumpReset;
		public float JumpVelocityMargin;

		public InputStruct CurrentInput;
		public InputStruct[] InputArray;
		[HideInInspector]
		public Vector3[] MovementArray;
		private Quaternion[] RotationArray;

		private float LastYVelocity;

		[HideInInspector]
		public int index;
		[HideInInspector]
		public int maxIndexSoFar;

		private int maxSize;
		private const int margin = 100;

		private ParticleSystem partic;

		//Need to initialize this from outside
		public void InitializeTime(float maxTime) {
			this.maxSize = (int)System.Math.Ceiling((1.0 / Time.fixedDeltaTime) * maxTime) + margin;
		}
			
		void Start () {
			InputArray = new InputStruct[maxSize];
			MovementArray = new Vector3[maxSize];
			RotationArray = new Quaternion[maxSize];

			//Empty input to start with
			CurrentInput = new InputStruct (0, 0, keyEnum.None);

			//Set variables
			Speed = 5;
			RotationSpeed = 1;
			JumpStrength = 5;
			LandedVelocity = 0.1f;
			LandedDistance = 0.6f;
			MarginForJumpReset = 0.3f;
			JumpVelocityMargin = 0.05f;

			//Initialize index
			index = 0;
			maxIndexSoFar = 0;

			//Get and set transformables
			rb = gameObject.GetComponent<Rigidbody>();
			tf = gameObject.GetComponent<Transform>();
			StartingPosition = tf.position;
			MovementArray [0] = StartingPosition;
			RotationArray [0] = tf.rotation;


			//Set color of model
			for (int i = 0; i < transform.childCount; ++i) {
				Renderer temp = transform.GetChild (i).GetComponent<Renderer>();
				if (temp != null) {
					temp.material.color = color;
				}
				ParticleSystem tempP = transform.GetChild (i).GetComponent<ParticleSystem> ();
				if (tempP != null) {
					partic = tempP;
					ParticleSystem.MainModule tempMain = partic.main;
					tempMain.startColor = color;
				}
			}
		}

		void FixedUpdate() {
			if (TimeOn) {
				if (Selected) {
					if (rb.isKinematic) {
						rb.isKinematic = false;
					}
					if (partic.isStopped) {
						partic.Play();
					}

					//If this is the selected unit, store current input keys
					maxIndexSoFar = Mathf.Max(maxIndexSoFar, index);
					InputArray[index] = CurrentInput;
					index++;
					UpdateMovement();
				} else {
					if (!rb.isKinematic) {
						rb.isKinematic = true;
					}
					if (!partic.isStopped) {
						partic.Stop();
					}

					if (index < maxIndexSoFar) {
						tf.position = MovementArray [index];
						tf.rotation = RotationArray [index];
					}

					//If not selected, extract stored keys
					CurrentInput = InputArray[index];
					index++;
				}
			}
		}

		private void UpdateMovement() {
			if ((CurrentInput.keys & keyEnum.Space) != 0) {
				Jump();
			}

			//Update position
			Vector3 direction = Vector3.zero;
			direction += Vector3.right * CurrentInput.horizontal;
			direction += Vector3.forward * CurrentInput.vertical;
			LastYVelocity = rb.velocity.y;
			rb.velocity = direction * Speed + rb.velocity.y * Vector3.up; //Preserve upward velocity

			//Update position array
			MovementArray[index-1] = tf.position;
			RotationArray[index-1] = tf.rotation;
		}

		private bool jumpInitialized = false;
		private void Jump() {
			if (!jumpInitialized || Mathf.Abs(LastYVelocity - rb.velocity.y) < 0f) {
				rb.velocity += Vector3.up * JumpStrength;
				jumpInitialized = true;
			}
		}

		private void OnCollisionEnter(Collision col) {
			foreach(ContactPoint point in col.contacts) {
				Debug.Log (point.normal);
				bool isBelow = Mathf.Abs (Vector3.Dot (point.normal, Vector3.up) - 1.0f) < MarginForJumpReset;
				if (isBelow) {
					jumpInitialized = false;
					break;
				}
			}
		}

		public void Reset() {
			this.index = 0;
			this.rb.velocity = Vector3.zero;
			this.tf.position = StartingPosition;
			this.MovementArray[0] = StartingPosition;
			if (Selected) {
				maxIndexSoFar = 0;
			}
		}

		public void ResetInputList() {
			System.Array.Clear(InputArray, 0, maxSize);
		}
	}
}