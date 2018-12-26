using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{

    private static Transform mainCamera;

    private static CameraManager instance;
    public static CameraManager GetInstance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(CameraManager)) as CameraManager;
            if (!instance)
                Debug.LogError("There needs to be one active MyClass script on a GameObject in your scene.");
            if (!mainCamera)
            {
                mainCamera = GameObject.Find("Camera").transform;
            }
        }

        return instance;
    }

    bool Smooth = false;

    float Smoothing = 5;
    Vector3 offset;

    public Vector3 getPostion()
    {
        return mainCamera.position;
    }

    public Vector3 getAngle()
    {
        return mainCamera.eulerAngles;
    }

    public Transform getTransfrom()
    {
        return mainCamera;
    }

    public Transform getCamera()
    {
        return mainCamera;
    }

    void FixedUpdate()
    {
        Smooth = false;
        if (Smooth)
        {
            Vector3 targetCamPos = PlayerManager.GetInstance().getPlayerTransfrom().position + offset;

            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetCamPos, Smoothing * Time.deltaTime);
        }
    }


    public void setSmooth(bool _Smooth) { Smooth = _Smooth; offset = mainCamera.transform.position - PlayerManager.GetInstance().getPlayerTransfrom().position; }

    public void setPostion(Vector3 _pos) { mainCamera.position = _pos; }
    public void setAngle(Quaternion _angle) { mainCamera.rotation = _angle; }
}
