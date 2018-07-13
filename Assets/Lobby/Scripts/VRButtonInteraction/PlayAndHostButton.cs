using Prototype.NetworkLobby;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VRStandardAssets.Utils;

public class PlayAndHostButton : GearButton {

    protected override void HandleDown()
    {
        m_lmm.OnClickHost();
    }
}
