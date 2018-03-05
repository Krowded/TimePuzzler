using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Player
{
	public class PlayerList : MonoBehaviour
	{
		public GameObject PlayerContainer;

		public List<PlayerMovement> Players { get { return players; } }
		private List<PlayerMovement> players;

		private bool initialized = false;

		public void Start() {
			if (!initialized) {
				Initialize();
			}
		}

		//Externally initialized for synch
		public void Initialize() {
			initialized = true;
			this.players = new List<PlayerMovement>();
			foreach(PlayerMovement player in PlayerContainer.GetComponentsInChildren<PlayerMovement>()) {
				this.players.Add(player);
			}
		}

		public void AddPlayer(PlayerMovement player) {
			this.players.Add(player);
		}

		public bool RemovePlayer() {
			//TODO: Implement this shit
			return false;
		}
	}
}

