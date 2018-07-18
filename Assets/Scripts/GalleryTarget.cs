using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using VRStandardAssets.Utils;

public class GalleryTarget : NetworkBehaviour
{

    [SerializeField] private float m_lifetime = 1.5f;

    [ServerCallback]
    void Start()
    {
        Invoke("DestroyObject", m_lifetime);
    }

    public void DestroyObject()
    {
        NetworkServer.Destroy(this.gameObject);
    }    
}
