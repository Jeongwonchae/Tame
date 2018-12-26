using UnityEngine;
using System.Collections;

public class TimeObjectController : MonoBehaviour {
    private static TimeObjectController instance;
    public static TimeObjectController GetInstance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(TimeObjectController)) as TimeObjectController;
            if (!instance)
                Debug.LogError("There needs to be one active MyClass script on a GameObject in your scene.");
        }

        return instance;
    }
    GameObject[] MoveingObject;

    float[] dist;

    void Awake()
    {
        //MoveingObject = GameObject.FindGameObjectsWithTag("Move");

        //for (int i = 0 ; i < MoveingObject.Length ; i++)
        //{
        //    dist[i] = Vector2.Distance(new Vector2(0, PlayerManager.GetInstance().getPostion().y), new Vector2(0, MoveingObject[0].transform.position.y));
        //}
    }

    public Rect BoundsToScreenRect(Bounds bounds)
    {
        Vector3 origin = Camera.main.WorldToScreenPoint(new Vector3(bounds.min.x, bounds.max.y, 0f));
        Vector3 extent = Camera.main.WorldToScreenPoint(new Vector3(bounds.max.x, bounds.min.y, 0f));

        return new Rect(origin.x, Screen.height - origin.y, extent.x - origin.x, origin.y - extent.y);
    }

    //void Update()
    //{
    //    for (int i = 0 ; i < MoveingObject.Length ; i++)
    //    {
    //        Transform tr = MoveingObject[i].transform;
    //        if (MoveingObject[i].GetComponent<Renderer>().bounds.max.y <= PlayerManager.GetInstance().getPostion().y)
    //            tr.Translate(Vector3.up * Time.deltaTime);
    //        //else
    //        //{
    //        //    tr.position = new Vector3(tr.position.x, PlayerManager.GetInstance().getPostion().y, tr.position.z);
    //        //}
    //    }
    //}
}
