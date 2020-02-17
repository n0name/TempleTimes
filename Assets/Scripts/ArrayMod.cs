using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ArrayMod : MonoBehaviour
{

    [Serializable]
    public struct Item {
        public GameObject gameObject;
        public int cnt;
    }

    private struct Cache
    {
        public Vector3 pos;
        public int cnt;
    }

    public List<Item> items;
    private List<Cache> cacheItems = new List<Cache>();
    private List<GameObject> created = new List<GameObject>();

    void CacheItems()
    {
        cacheItems.Clear();
        foreach (Item i in items)
        {
            var p = i.gameObject.transform.position;
            cacheItems.Add(new Cache() { pos = new Vector3(p.x, p.y, p.z), cnt = i.cnt });
        }
    }

    bool CheckCache()
    {
        if (items.Count != cacheItems.Count)
            return false;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].gameObject.transform.position != cacheItems[i].pos)
                return false;
            if (items[i].cnt != cacheItems[i].cnt)
                return false;
        }
        return true;
    }

    void GenerateObjects()
    {
        Debug.Log("CreateItems");
        CacheItems();
        var center = transform.position;
        foreach (GameObject child in created)
        {
            DestroyImmediate(child);
        }

        foreach (Item i in items)
        {
            var curPos = i.gameObject.transform.position;
            var curRot = i.gameObject.transform.rotation;
            Vector3 temp = curPos - transform.position;
            float start_angle = Vector3.Angle(transform.right, temp);
            float dist = (transform.position - curPos).magnitude;
            float angleStep = Mathf.PI * 2f / (i.cnt);
            for (int k = 0; k < i.cnt; k++)
            {
                float angle = k * angleStep;
                Vector3 newPos = new Vector3(
                    center.x + dist * Mathf.Cos(angle), 
                    curPos.y, 
                    center.z + dist * Mathf.Sin(angle));

                var newGo = Instantiate(i.gameObject, newPos, curRot, transform);
                newGo.transform.Rotate(new Vector3(0f, start_angle - angle * Mathf.Rad2Deg, 0f), Space.World);
                created.Add(newGo);
            }
        }
    }

    void Update()
    {
        if (!CheckCache())
            GenerateObjects();
    }

    void OnGUI()
    {
        if (!CheckCache())
            GenerateObjects();
    }
}
