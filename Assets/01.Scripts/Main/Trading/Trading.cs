using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Trading : MonoBehaviour
{
    public GameObject tradingScreen;

    private void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            tradingScreen.SetActive(false);
        }
    }

}
