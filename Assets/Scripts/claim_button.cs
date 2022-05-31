using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class claim_button : MonoBehaviour
{
    public GameObject claimMenu;
    public GameObject spinButton;
    public void ButtonDown()
    {
        claimMenu.SetActive(false);
        spinButton.SetActive(true);
    }
}
