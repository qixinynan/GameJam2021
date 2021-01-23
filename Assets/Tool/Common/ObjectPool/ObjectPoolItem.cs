using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolItem : MonoBehaviour
{
    public float destoryTime;
    public virtual void OnEnable() {
        StartCoroutine(Util.DelayExecute(destoryTime, () => {
            ObjectPool.instance.ReturnToPool(gameObject);
        }));
    }
}
