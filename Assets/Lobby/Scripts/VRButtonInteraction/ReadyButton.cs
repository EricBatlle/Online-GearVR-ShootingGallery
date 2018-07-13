using Prototype.NetworkLobby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyButton : GearButton {
    
    [SerializeField] private LobbyPlayer lp;

    protected override void HandleDown()
    {
        lp.OnReadyClicked();
    }
}
