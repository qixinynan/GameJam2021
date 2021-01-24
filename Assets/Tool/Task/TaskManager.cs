using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class TaskInfo {
    public int id;
    public TaskBaseInfo taskBaseInfo; 
}

public class TaskBaseInfo {
    public int type;
}

public class TaskCountInfo : TaskBaseInfo {
    public int id;
    public int count;
}

public class TaskSpotInfo : TaskBaseInfo {
    public int spotId;
} 

public class TaskManager
{
    public Dictionary<int, TaskInfo> taskInfoDict = new Dictionary<int, TaskInfo>();
    
    public List<TaskInfo> curTaskInfoList = new List<TaskInfo>();

    public void ParseTaskInfo()
    {
        JsonData jsonData = JsonMapper.ToObject(Resources.Load<TextAsset>("TaskData/TaskInfo").text);
        for (int i = 0; i < jsonData.Count; i++)
        {
            TaskInfo info = new TaskInfo();
            info.id = (int) jsonData[i]["id"];
            int type = (int) jsonData[i]["type"];
            switch (type)
            {
                // 数量
                case 0:
                    TaskCountInfo taskCountInfo = new TaskCountInfo();
                    taskCountInfo.type = 0;
                    taskCountInfo.id = (int) jsonData[i]["desId"];
                    taskCountInfo.count = (int) jsonData[i]["count"];
                    info.taskBaseInfo = taskCountInfo;
                    break;
                // 地点
                case 1:
                    TaskSpotInfo taskSpotInfo = new TaskSpotInfo();
                    taskSpotInfo.type = 0;
                    taskSpotInfo.spotId = (int) jsonData[i]["spotId"];
                    info.taskBaseInfo = taskSpotInfo;
                    break;
                default:
                    break;
            }
            taskInfoDict[info.id] = info;
            curTaskInfoList.Clear();
        }
    }

    public void AcceptTask(int id)
    {
        if (!taskInfoDict.ContainsKey(id))
        {
            Debug.LogError("Error task id : " + id);
            return;
        }

        bool isAccepted = false;

        for (int i = 0; i < curTaskInfoList.Count; i++)
        {
            if (curTaskInfoList[i].id == id)
            {
                isAccepted = true;
                break;
            }
        }

        if (isAccepted)
        {
            Debug.LogError("Task has been accepted : " + id);
            return;
        }
        
        curTaskInfoList.Add(taskInfoDict[id]);
        Debug.Log("Accept task : " + id);
    }

    public void FinishTask(int id)
    {
        if (!taskInfoDict.ContainsKey(id))
        {
            Debug.LogError("Error task id : " + id);
            return;
        }
        for (int i = 0; i < curTaskInfoList.Count; i++)
        {
            if (curTaskInfoList[i].id == id)
            {
                curTaskInfoList.RemoveAt(i);
                Debug.Log("Finish task : " + id);
                return;
            }
        }
        
        Debug.LogError("Task has not been accepted : " + id);
    }
}
