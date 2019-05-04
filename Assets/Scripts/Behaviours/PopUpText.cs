using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpText : MonoBehaviour
{

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;

    public void WaitForSeconds(float seconds)
    {
        StartCoroutine(waiting(seconds));
    }

    public void Close()
    {
        GetComponent<Animator>().SetBool("Pop", false);
    }

    IEnumerator waiting(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Close();
    }
}
