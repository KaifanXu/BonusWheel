using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin_button : MonoBehaviour
{

    public start_spin StartSpin;

    public void ButtonDown()
    {
        StartSpin.PreSpin();
    }
}
