using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMovement : MonoBehaviour
{

    public Transform sprite;

    public void GoTo(Transform parent, bool withAnimation)
    {
        if (withAnimation)
        {
            sprite.SetParent(transform.parent.parent);
            transform.SetParent(parent);
            //Swap_Animation(swapTween, Sprite.transform.position, transform.position);
        }
        else
        {
            transform.SetParent(parent);
        }
    }

    private void Update()
    {
        var dist = transform.position - sprite.position;
        if (dist.magnitude > 0.5f)
        {
            sprite.Translate((transform.position - sprite.position) * Time.deltaTime * 10);
        }
        else
        {
            sprite.SetParent(transform);
        }
    }

    private void OnDisable()
    {
        if(sprite.transform.parent != transform)
        {
            sprite.gameObject.SetActive(false);
            sprite.transform.position = transform.position;
        }
    }

    private void OnEnable()
    {
        if(sprite.transform.parent != transform)
        {
            sprite.gameObject.SetActive(true);
            sprite.transform.position = transform.position;
        }
    }

}
