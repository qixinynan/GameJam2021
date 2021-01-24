using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController manager = null;
    public DialogManager dialogMan = new DialogManager();
    public TaskManager taskMan = new TaskManager();

    public bool disableInput = false;
    
    private void Awake()
    {
        if (manager == null)
        {
            manager = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (manager != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        dialogMan.ParseDialogInfo();
        taskMan.ParseTaskInfo();
    }
}
