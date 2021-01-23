using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIKeyManager : MonoBehaviour
{ 
    void Update()
    {
        OpenPanel(gameObject, KeyCode.B);
    }
   
    public void OpenPanel(GameObject gameObject, KeyCode keyCode) {
        if (Input.GetKeyDown(keyCode))
            gameObject.SetActive(!gameObject.activeSelf);
    }
}
