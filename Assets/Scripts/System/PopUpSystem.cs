using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpSystem : MonoBehaviour
{

    public static void ShowLocalText(Vector2 position, string text, Color textColor, bool FadeIn = false)
    {
        GameObject Pop = Instantiate((GameObject)Resources.Load("Dialogue/PopUp"));
        Pop.transform.position = position;
        var textMesh = Pop.GetComponentInChildren<TextMeshProUGUI>();
        textMesh.text = text;
        textMesh.color = textColor;
        var anim = Pop.GetComponentInChildren<Animator>();
        if (FadeIn)
        {
            anim.SetTrigger("FadeIn");
        }
        else
        {
            anim.SetTrigger("Idle");
        }
    }

    public static PopUpText DescriptionPop(string title, string description, float time = -1)
    {
        GameObject Pop = Instantiate((GameObject)Resources.Load("Dialogue/DescriptionPop"));
        var texts = Pop.GetComponent<PopUpText>();
        var anim = Pop.GetComponent<Animator>();
        anim.SetBool("Pop", true);
        if(time != -1)
        {
            texts.WaitForSeconds(time);
        }
        texts.titleText.text = title;
        texts.descriptionText.text = description;
        return texts;
    }
}
