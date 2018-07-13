using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListServersButton : GearButton {
    protected override void HandleDown()
    {
        m_lmm.OnClickOpenServerList();
    }
}
