using Prototype.NetworkLobby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class GearButton : MonoBehaviour {

    [SerializeField] private VRInteractiveItem m_InteractiveItem;                    // Used to handle the user clicking whilst looking at the target.
    [SerializeField] protected LobbyMainMenu m_lmm;
    private void OnEnable()
    {
        m_InteractiveItem.OnDown += HandleDown;
    }


    private void OnDisable()
    {
        m_InteractiveItem.OnDown -= HandleDown;
    }

    protected virtual void HandleDown()
    {
    }
}
