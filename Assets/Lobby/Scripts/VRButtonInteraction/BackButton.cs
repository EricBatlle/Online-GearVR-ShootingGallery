using Prototype.NetworkLobby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : GearButton {

    [SerializeField] LobbyManager m_lm;

    protected override void HandleDown()
    {
        m_lm.GoBackButton();
    }
}
