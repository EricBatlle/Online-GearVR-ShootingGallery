using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UtilityRotateCamera : NetworkBehaviour {

    public float sensivility = 0.5f;

    Vector2 mousePositionRef = Vector2.zero;

	
	void Update () {
        if (!isLocalPlayer)
        {
            this.gameObject.tag = "CameraNoLocal";
            this.gameObject.GetComponent<Camera>().enabled = false;
            this.gameObject.GetComponent<AudioListener>().enabled = false;

            return;
        }            

        if (Input.GetMouseButtonDown(1)) {

            mousePositionRef = Input.mousePosition;

        }

        if (Input.GetMouseButton(1)) {

            transform.localEulerAngles += new Vector3(mousePositionRef.y - Input.mousePosition.y, Input.mousePosition.x - mousePositionRef.x, 0) * sensivility;
            mousePositionRef = Input.mousePosition;

        }

	}
}
