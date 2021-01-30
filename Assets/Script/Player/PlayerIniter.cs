using System.Collections;
using System.Collections.Generic;
using Cinemachine;
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
        GameController.manager.virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        // init player
        GameController.manager.player = null;
        GameController.manager.boy = null;
        GameController.manager.girl = null;
        if (GameController.manager.boyRoomId == roomId)
        {
            GameObject boy = Instantiate(boyPrefab);
            boy.transform.position = GameController.manager.boyPos;
            if (GameController.manager.isControllBoy)
            {
                GameController.manager.player = boy;
                GameController.manager.virtualCamera.Follow = boy.transform;
            }

            GameController.manager.boy = boy;
        }
        if (GameController.manager.girlRoomId == roomId)
        {
            GameObject girl = Instantiate(girlPrefab);
            girl.transform.position = GameController.manager.girlPos;
            if (!GameController.manager.isControllBoy)
            {
                GameController.manager.player = girl;
                GameController.manager.virtualCamera.Follow = girl.transform;
            }

            GameController.manager.girl = girl;
        }
        
    }
}
