#pragma strict

enum keyEnum {
	W = 1,
	A = 2,
	S = 4,
	D = 8,
	Space = 16,
};

public var thrust: float;
public var rb: Rigidbody;
public var selected: boolean;
public var timeOn = true;
private var inputArray: int[];
private static var maxSize = 10000;
private var index: int;
private var keysDown: int;

function Start () {
	rb = GetComponent.<Rigidbody>();
	inputArray = new int[maxSize];
	keysDown = 0;
	index = 0;

	selected = true;
}

function Update() {
	keysDown = 0;
	/*
	if (Input.GetKey(KeyCode.W)) {
		keysDown |= keyEnum.W;
	}
	if (Input.GetKey(KeyCode.A)) {
		keysDown |= keyEnum.A;
	}
	if (Input.GetKey(KeyCode.S)) {
		keysDown |= keyEnum.S;
	}

	if (Input.GetKey(KeyCode.D)) {
		keysDown |= keyEnum.D;
	}
	if (Input.GetKey(KeyCode.space)) {
		keysDown |= keyEnum.space;
	}
	*/
}

function FixedUpdate () {
	if (timeOn) {
		TickUpdate();
	}
}

function TickUpdate() {
/*	var direction = Vector3.zero;
	if (selected) {
		//Listen to key inputs
		if (keysDown & keyEnum.W != 0) {
			direction += Vector3.forward;
		}
		if (keysDown & keyEnum.A != 0) {
			direction += Vector3.left;
		}
		if (keysDown & keyEnum.S != 0) {
			direction += Vector3.back;
		}
		if (keysDown & keyEnum.D != 0) {
			direction += Vector3.right;
		}
		if (keysDown & keyEnum.Space != 0) {
			Jump();
		}

		inputArray[index] = keysDown;
		index++;
		Debug.Log("AAAHA\n" + direction.x + " " + direction.y + " " + direction.z);
	} else {
		//Use stored inputs
	}

	rb.AddForce(normalize(direction)*thrust);
*/
}

function Jump() {
	//TODO: Implement jump
}