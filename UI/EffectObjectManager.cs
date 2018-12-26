using UnityEngine;
using System.Collections;

public class EffectObjectManager : MonoBehaviour {

    private static EffectObjectManager instance;
    public static EffectObjectManager GetInstance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(EffectObjectManager)) as EffectObjectManager;
            if (!instance)
                Debug.LogError("There needs to be one active MyClass script on a GameObject in your scene.");
        }

        return instance;
    }

    Vector3 startPos;
    int? index = 0;

    GameObject[] effectGB;

    public void setEffectObject(GameObject[] _GB, Vector3 _startPos)
    {
        //if (index != 0)
        //    effectGB[(int)index].SetActive(true);
        float[] dist;

        effectGB = _GB;

        dist = new float[_GB.Length];

        startPos = _startPos;

        if (index != null)
        {
            effectGB[(int)index].AddComponent<BoxCollider>();
        }
        for (int i = 0; i < dist.Length; i++)
        {
            dist[i] = Vector3.Distance(startPos, effectGB[i].transform.position);
        }

        

        float min = dist[(int)index];
        for (int i = 0; i < dist.Length; i++)
        {
            if (min > dist[i])
            {
                min = dist[i];
                index = i;
            }
        }


        Destroy(effectGB[(int)index].GetComponent<BoxCollider>());
        //effectGB[(int)index].SetActive(false);
    }
    public void EffectDestroy()
    {
        Destroy(effectGB[(int)index].GetComponent<BoxCollider>());
    }
    public GameObject getEffect(int _index) { return effectGB[_index]; }
}
