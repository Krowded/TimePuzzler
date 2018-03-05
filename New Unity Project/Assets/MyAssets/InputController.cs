using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Player;
using Level;

namespace InputControl
{
	[RequireComponent(typeof(PlayerList))]
	[RequireComponent(typeof(LevelClass))]
	public class InputController : MonoBehaviour
	{
		[HideInInspector]
		public PlayerList playerList;

		private PlayerMovement currentPlayer;
		private LevelClass currentLevel;
		private int currentPlayerIndex = 0;

		// Use this for initialization
		void Start ()
		{
			playerList = gameObject.GetComponent<PlayerList>();
			currentLevel = gameObject.GetComponent<LevelClass>();

			if (playerList.Players.Count > 0) {
				currentPlayer = playerList.Players [currentPlayerIndex];
				currentPlayer.Selected = true;
			} else {
				throw new MissingReferenceException ("No players available");
			}
		}
		
		// Update is called once per frame
		void Update ()
		{
			//if (currentLevel.Restarted) {
				//SwitchPlayer();
			if (currentLevel.Restarted || Input.GetKeyDown(KeyCode.R)) {//R is the switch player button (dedicated player buttons later?)
				currentLevel.RestartCycle();
				SwitchPlayer();
			}

			UpdateInputKeysOfPlayer();
		}

		//Switch to designated player or just the next player in the list
		private void SwitchPlayer(int index = -1) {
			currentPlayer.CurrentInput = new InputStruct (0, 0, keyEnum.None);
			currentPlayer.Selected = false;

			try {
				if (index < 0) { //Default, next player chosen
					currentPlayerIndex = (currentPlayerIndex >= playerList.Players.Count-1) ? 0 : currentPlayerIndex + 1;
				} else { //Attempt to switch to specific player
					currentPlayerIndex = index;
				}
				currentPlayer = playerList.Players[currentPlayerIndex];
				currentPlayer.Selected = true;
			} catch(System.ArgumentOutOfRangeException) {
				Debug.LogError("Player with index " + currentPlayerIndex + " doesn't exist. Input: " + index + ".\nPlayerList Count: " + playerList.Players.Count);
			}

			ResetPlayers();
		}

		private void UpdateInputKeysOfPlayer() {
			//Listen to key inputs
			keyEnum keyCollector = keyEnum.None;
			if (Input.GetKey(KeyCode.Space)) { //Only saving space outside directional controls atm
				keyCollector |= keyEnum.Space;
			}
			
			float horizontal = Input.GetAxis("Horizontal");
			float vertical = Input.GetAxis("Vertical");
			currentPlayer.CurrentInput = new InputStruct(horizontal,vertical,keyCollector);
		}

		private void ResetPlayers() {
			int i = 0;
			foreach (PlayerMovement player in playerList.Players) {
				player.Reset();
				++i;
			}
				
			currentPlayer.ResetInputList();
		}
	}
}

