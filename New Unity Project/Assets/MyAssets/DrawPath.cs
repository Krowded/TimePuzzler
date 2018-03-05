using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Player {
	[RequireComponent(typeof(PlayerMovement))]
	public class DrawPath : MonoBehaviour {
		private PlayerMovement player;
		private LineRenderer lineRenderer;

		public bool DrawOn = false;
		private int index;
		private bool initialized = false;

		void Initialize() {
			initialized = true;
			player = gameObject.GetComponent<PlayerMovement>();
			lineRenderer = gameObject.AddComponent<LineRenderer>();
			lineRenderer.material = new Material (Shader.Find ("Particles/Additive"));
			lineRenderer.widthMultiplier = 0.1f;
			lineRenderer.material.SetColor("_TintColor", player.color);
		}
		
		// Update is called once per frame
		void Update () {
			if (!initialized) {
				Initialize();
			}

			if (player.Selected) {
				DrawOn = false;
				lineRenderer.positionCount = 0;
			} else {
				DrawOn = true;
				index = player.index;
			}

			if (DrawOn) {
				this.Draw();
			}
 		}

		void Draw() {
			lineRenderer.positionCount = Mathf.Max(0,	player.maxIndexSoFar - this.index);
			lineRenderer.SetPositions (player.MovementArray.Skip(this.index).ToArray());
		}
	}
}