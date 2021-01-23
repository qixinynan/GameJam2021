using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance = null;
    private Dictionary<string, Queue<GameObject>> queueDict = new Dictionary<string, Queue<GameObject>>();
    private Dictionary<string, GameObject> prefabDict = new Dictionary<string, GameObject>();

    private void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else if(instance != this) {
            Destroy(gameObject);
        }
    }

    private void FillObjectPool(string name) {
        if(!prefabDict.ContainsKey(name)) {
            prefabDict[name] = Resources.Load<GameObject>("ObjectPool/" + name);
            queueDict[name] = new Queue<GameObject>();
        }
        var prefab = prefabDict[name];
        if(!Util.objectInitCountDict.ContainsKey(name)) {
            Debug.LogError("Can not find key : " + name);
            return;
        }
        for(int i = 0; i < Util.objectInitCountDict[name]; i++) {
            GameObject pd = Instantiate(prefab);
            pd.name = name;
            pd.transform.SetParent(transform, false);
            pd.gameObject.SetActive(false);
            queueDict[name].Enqueue(pd);
        }
    }

    public GameObject GetItem(string name) {
        if(!queueDict.ContainsKey(name) || queueDict[name].Count == 0) {
            FillObjectPool(name);
        }
        GameObject go = queueDict[name].Dequeue();
        go.SetActive(true);
        return go;
    }

    public void ReturnToPool(GameObject go) {
        go.SetActive(false);
        queueDict[go.transform.name].Enqueue(go);
    }
}
