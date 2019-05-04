using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class testescreen : MonoBehaviour
{

    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;

    public void checkScreen()
    {
        var screenRes = Screen.currentResolution;
        text1.text = screenRes.ToString();
        var screenOr = Screen.orientation;
        text2.text = screenOr.ToString();
        var screenSize = new Vector2(Screen.width, Screen.height);
        text3.text = screenSize.ToString();
    }

    private void Update()
    {
        checkScreen();
    }
}
