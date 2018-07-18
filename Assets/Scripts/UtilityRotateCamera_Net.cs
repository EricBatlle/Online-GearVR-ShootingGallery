using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UtilityRotateCamera_Net : NetworkBehaviour {

    public float sensivility = 0.5f;

    private Vector2 m_mousePositionRef = Vector2.zero;

    private void Update()
    {
        //If is not the local player, disable other camera's stuff and update ends
        if (!isLocalPlayer)
        {
            DisableRemoteCameraStuff();
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            m_mousePositionRef = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            transform.localEulerAngles += new Vector3(m_mousePositionRef.y - Input.mousePosition.y, Input.mousePosition.x - m_mousePositionRef.x, 0) * sensivility;
            m_mousePositionRef = Input.mousePosition;
        }
    }

    //Disable anoying stuff from the other clients cameras that can negatively affect our scene
    private void DisableRemoteCameraStuff()
    {
        this.gameObject.tag = "CameraNoLocal";
        this.gameObject.GetComponent<Camera>().enabled = false;
        this.gameObject.GetComponent<AudioListener>().enabled = false;
    }
}
