using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Display
{
	[RequireComponent(typeof(Level.LevelClass))]
	public class DisplayTimer : MonoBehaviour 
	{
		public Text timer;
		private Level.LevelClass level;

		void Start() {
			level = gameObject.GetComponent<Level.LevelClass>();
			timer.text = "0.0";
		}

		void Update() {
			if (level.TimeOn) {
				UpdateTimer();
			}
		}

		void UpdateTimer() {
			timer.text = (level.MaxTime - (Time.fixedTime - level.TimeStart)).ToString("0.00");
		}
	}
}

