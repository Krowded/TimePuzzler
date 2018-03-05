using UnityEngine;
using System.Collections;

namespace InputControl {
	[System.Serializable]
	public struct InputStruct {
		public float horizontal, vertical;
		public keyEnum keys;

		public InputStruct(float _horizontal, float _vertical, keyEnum _keys) {
			horizontal = _horizontal;
			vertical = _vertical;
			keys = _keys;
		}
	}
}

