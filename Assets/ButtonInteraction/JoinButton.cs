using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinButton : GearButton {

    protected override void HandleDown()
    {
        m_lmm.OnClickJoin();
    }
}
