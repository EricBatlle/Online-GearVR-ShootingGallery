using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateButton : GearButton {

    protected override void HandleDown()
    {
        m_lmm.OnClickCreateMatchmakingGame();
    }
}
