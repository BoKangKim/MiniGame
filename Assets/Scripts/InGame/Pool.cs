using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pool
{
    private static Queue<GameObject> effects = new Queue<GameObject>();
    
    public static GameObject Instantiate(GameObject effect ,Vector3 pos, Quaternion rot)
    {
        GameObject inst = null;

        if(effects.Count == 0)
        {
            inst = MonoBehaviour.Instantiate(effect, pos, rot);
        }
        else
        {
            inst = effects.Dequeue();
            inst.transform.position = pos;
            inst.transform.rotation = rot;
            inst.transform.localScale = new Vector3(10, 10, 10);
        }

        return inst;
    }

    public static void Destory(GameObject effect)
    {
        if(effect == null)
        {
            Debug.LogError("Effect is null checking pool");
            return;
        }

        effect.gameObject.SetActive(false);

        effects.Enqueue(effect);
    }
}
