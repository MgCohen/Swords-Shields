using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Camera.main.orthographicSize *= 1.1f;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Camera.main.orthographicSize *= 0.9f;
        }
    }
}
