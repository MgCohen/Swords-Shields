using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class testHealth : MonoBehaviour
{

    public TextMeshProUGUI text;
    private void Update()
    {
        text.text = Player.instance.Hp.ToString();
    }
}
