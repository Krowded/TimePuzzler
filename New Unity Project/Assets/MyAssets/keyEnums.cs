using UnityEngine;
using System.Collections;

namespace InputControl
{
	[System.Flags] public enum keyEnum {
		None = 0,
		W = (1 << 0),
		A = (1 << 1),
		S = (1 << 2),
		D = (1 << 3),
		Space = (1 << 4),
	};
}
