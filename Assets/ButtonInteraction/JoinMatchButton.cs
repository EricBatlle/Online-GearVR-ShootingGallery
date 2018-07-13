using Prototype.NetworkLobby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinMatchButton : GearButton {

    [SerializeField] private LobbyServerEntry m_lse;

    protected override void HandleDown()
    {        
        m_lse.JoinMatch(m_lse.match.networkId,m_lse.lobbyManager);
    }
}
