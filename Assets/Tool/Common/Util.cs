using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class GameObjectExtension {
    public static void SetActiveFast(this GameObject go, bool active) {
        if (go.activeInHierarchy != active)
            go.SetActive(active);
    }
}

public static class Util
{

    // data
    public static Dictionary<string, int> objectInitCountDict = new Dictionary<string, int>
    {
        {ObjectItemNameCollection.demo, 10}
    };

    #warning  .......... -1
    public static Dictionary<int, int> roomToSceneDict = new Dictionary<int, int> {{-1, 3},{0, 0},{1, 1}};

    //type
    public delegate void NoParmsCallBack();
    public delegate bool BoolParmsCallBack();
    
    public static class ObjectItemNameCollection
    {
        public static string demo = "demo";
    }
    
    public static class TagCollection
    {
        public static string player = "Player";
        public static string stopPos = "StopPos";
        public static string door = "Door";
    }

    // function
    public static System.Collections.IEnumerator DelayExecute(float time, NoParmsCallBack callback)
    {
        yield return new WaitForSecondsRealtime(time);
        callback?.Invoke();
    }

    public static System.Collections.IEnumerator DelayExecute(BoolParmsCallBack preFunc, NoParmsCallBack callback)
    {
        yield return new WaitUntil(() => { return preFunc(); });
        callback?.Invoke();
    }

    public static Color ColorFromString(string s, float a)
    {
        int R = int.Parse(s.Substring(1, 2),
            System.Globalization.NumberStyles.HexNumber);
        int G = int.Parse(s.Substring(3, 2),
            System.Globalization.NumberStyles.HexNumber);
        int B = int.Parse(s.Substring(5, 2),
            System.Globalization.NumberStyles.HexNumber);
        return new Color(R / 255f, G / 255f, B / 255f, a);
    }
    
    public static void DeleteChildren(Transform t, int startIndex = 0)
    {
        var children = new List<Transform>();
        for (var i = startIndex; i < t.childCount; i++)
        {
            children.Add(t.GetChild(i));
        }

        foreach (var child in children)
        {
            child.SetParent(null);
            UnityEngine.Object.Destroy(child.gameObject);
        }
    }
}
