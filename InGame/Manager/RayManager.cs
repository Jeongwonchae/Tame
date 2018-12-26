using UnityEngine;
using System.Collections;

public class RayManager : MonoBehaviour {

    private static RayManager instance;
    public static RayManager GetInstance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(RayManager)) as RayManager;
            if (!instance)
                Debug.LogError("There needs to be one active MyClass script on a GameObject in your scene.");
        }

        return instance;
    }


    public bool MainCameraToRay()
    {
        RaycastHit hit;

        Ray _ray = Camera.allCameras[0].ScreenPointToRay(Input.GetTouch(0).position);
        if (Physics.Raycast(_ray, out hit, Mathf.Infinity))
        {
            return true;
        }

       return false;
    }

    public bool MainCameraToRay(out RaycastHit _hit)
    {
        Ray _ray = Camera.allCameras[0].ScreenPointToRay(Input.GetTouch(0).position);

        if (Physics.Raycast(_ray, out _hit, Mathf.Infinity))
        {
            return true;
        }

        return false;
    }

    public bool MainCameraToRay(string tagName)
    {
        RaycastHit hit;

        Ray _ray = Camera.allCameras[0].ScreenPointToRay(Input.GetTouch(0).position);

        if (Physics.Raycast(_ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.tag == tagName)
            {
                return true;
            }
        }
        return false;
    }

    public bool RayCastIsTag(Vector3 startPostion, Vector3 Direction, float dist,string tagName)
    {
        RaycastHit hit;
        if (Physics.Raycast(startPostion, Direction, out hit, dist))
        {
            if (hit.transform.tag == tagName)
            {
                return true;
            }
        }
        return false;
    }

    public bool RayCastIsTag(Vector3 startPostion, Vector3 Direction, float dist, string tagName, string tagName2)
    {
        RaycastHit hit;
        if (Physics.Raycast(startPostion, Direction, out hit, dist))
        {
            if (hit.transform.tag == tagName || hit.transform.tag == tagName2)
            {
                return true;
            }
        }
        return false;
    }

    public bool RayCastIsTag(Vector3 startPostion, Vector3 Direction, float dist, string tagName, string tagName2, string tagName3)
    {
        RaycastHit hit;
        if (Physics.Raycast(startPostion, Direction, out hit, dist))
        {
            if (hit.transform.tag == tagName || hit.transform.tag == tagName2 || hit.transform.tag == tagName3)
            {
                return true;
            }
        }
        return false;
    }

    public bool RayCastIsTag(Vector3 startPostion, Vector3 Direction, out RaycastHit hit, float dist, string tagName, string tagName2)
    {
        if (Physics.Raycast(startPostion, Direction, out hit, dist))
        {
            if (hit.transform.tag == tagName || hit.transform.tag == tagName2)
            {
                return true;
            }
        }
        return false;
    }

    public bool RayCastIsTag(Vector3 startPostion, Vector3 Direction, out RaycastHit hit, float dist, string tagName, string tagName2, string tagName3)
    {
        if (Physics.Raycast(startPostion, Direction, out hit, dist))
        {
            if (hit.transform.tag == tagName || hit.transform.tag == tagName2 || hit.transform.tag == tagName3)
            {
                return true;
            }
        }
        return false;
    }

    public bool RayCastIsTag(Vector3 startPostion, Vector3 Direction, out RaycastHit hit, float dist, string tagName, string tagName2, string tagName3, string tagName4)
    {
        if (Physics.Raycast(startPostion, Direction, out hit, dist))
        {
            if (hit.transform.tag == tagName || hit.transform.tag == tagName2 || hit.transform.tag == tagName3 || hit.transform.tag == tagName4)
            {
                return true;
            }
        }
        return false;
    }

    public bool RayCastIsTag(Vector3 startPostion, Vector3 Direction, out RaycastHit hit, float dist ,string tagName)
    {
        if (Physics.Raycast(startPostion, Direction, out hit, dist))
        {
            if (hit.transform.tag == tagName)
            {
                return true;
            }
        }

        return false;
    }

    public bool RayCastIsTag(Vector3 startPostion, Vector3 Direction, out RaycastHit hit, float dist)
    {

        if (Physics.Raycast(startPostion, Direction, out hit, dist))
        {
                return true;
        }
        return false;
    }
}
