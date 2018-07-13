using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using VRStandardAssets.Utils;

public class GalleryTarget : NetworkBehaviour
{

    // Use this for initialization
    [SerializeField] private float lifetime = 1.5f;
    [SerializeField] private VRInteractiveItem m_InteractiveItem;                    // Used to handle the user clicking whilst looking at the target.

    private void OnEnable()
    {
        m_InteractiveItem.OnDown += HandleDown;
    }
    private void OnDisable()
    {
        m_InteractiveItem.OnDown -= HandleDown;
    }

    [Server]
    void Start()
    {
        Invoke("DestroyObject", lifetime);
    }

    public void DestroyObject()
    {
        NetworkServer.Destroy(this.gameObject);
    }

    private void HandleDown()
    {

    }
}
