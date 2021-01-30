using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerIniter : MonoBehaviour
{
    public GameObject boyPrefab;
    public GameObject girlPrefab;
    public int roomId;

    public List<Transform> posList;

    private IEnumerator Start()
    {
        yield return new WaitUntil(() =>
        {
            return GameController.manager != null;
        });
        // init player
        int index = 0;
        if (GameController.manager.boyRoomId == roomId)
        {
            GameObject boy = Instantiate(boyPrefab);
            boy.transform.position = posList[index].position;
            index += 1;
            if (GameController.manager.isControllBoy)
            {
                GameController.manager.player = boy;
            }

            GameController.manager.boy = boy;
        }
        if (GameController.manager.girlRoomId == roomId)
        {
            GameObject girl = Instantiate(girlPrefab);
            girl.transform.position = posList[index].position;
            index += 1;
            if (!GameController.manager.isControllBoy)
            {
                GameController.manager.player = girl;
            }

            GameController.manager.girl = girl;
        }
    }
}
