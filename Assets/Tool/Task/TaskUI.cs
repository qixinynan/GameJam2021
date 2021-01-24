using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskUI : MonoBehaviour
{

    public InputField input;
    public Button acceptBtn;
    public Button finishBtn;
    
    // Start is called before the first frame update
    void Start()
    {
        acceptBtn.onClick.AddListener(() =>
        {
            int id = int.Parse(input.text.Trim());
            GameController.manager.taskMan.AcceptTask(id);
        });
        
        finishBtn.onClick.AddListener(() =>
        {
            int id = int.Parse(input.text.Trim());
            GameController.manager.taskMan.FinishTask(id);
        });
    }

}
