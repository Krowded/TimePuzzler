using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Level {
	[RequireComponent(typeof(PlayerList))]
	public class LevelClass : MonoBehaviour {
		private List<Color> colorList = new List<Color>{Color.red, Color.green, Color.yellow, Color.cyan, Color.magenta, Color.white};

		public bool Restarted = false;
		public bool TimeOn = false;
		public float MaxTime = 0;

		[HideInInspector]
		public PlayerList playerList;
		public float TimeStart {get { return timeStart; } }
		private float timeStart;

		void Awake() {
			playerList = gameObject.GetComponent<PlayerList>();
			playerList.Initialize();

			TimeOn = true;
			UpdateTimeState();
			MaxTime = 10;
			timeStart = Time.fixedTime;

			int i = 0;
			foreach (PlayerMovement player in playerList.Players) {
				player.InitializeTime(MaxTime);
				player.color = colorList[i++];
			}
		}

		//Restart the cycle if the fixedTime is over the limit (This should guarantee the same number of frames each cycle?)
		void LateUpdate() {
			if (Time.fixedTime - timeStart >= MaxTime) {
				//RestartCycle();
				Restarted = true;
			} else {
				Restarted = false;
			}
		}

		void UpdateTimeState() {
			foreach (PlayerMovement player in playerList.Players) {
				player.TimeOn = TimeOn;
			}
		}

		public void RestartCycle() {
			timeStart = Time.fixedTime;
		}
	}
}