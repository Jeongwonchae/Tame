using UnityEngine;
using System.Collections;

class Chapter2_1 : Modal
{
    GameObject[] middleTarget;

    public override void Init()
    {
        CameraManager.GetInstance().getCamera().GetComponent<Camera>().fieldOfView = 11;

        UIFigureManager.GetInstance().Init();

        dist = PlayerManager.GetInstance().getPostion() - CameraManager.GetInstance().getPostion();

        pBeginPos = PlayerManager.GetInstance().getPostion();

        mainCamera = GameObject.Find("Camera").transform;

        PlayerManager.GetInstance().getPlayerTransfrom().position = new Vector3(0.4980837f, 0.072f, -0.5002995f);

        PlayerManager.GetInstance().getPlayerTransfrom().eulerAngles = new Vector3(0, 270.0f, 0);

        mainCamera.position = new Vector3(13.78f, 11.19f, 10.75f);
        mainCamera.rotation = Quaternion.Euler(29.6236f, 237.9086f, 3.7804f);

        PlayerManager.GetInstance().getPlayerTransfrom().gameObject.SetActive(true);

        middleTarget = new GameObject[2];
        Vector3[] pos = {
                            new Vector3(-0.467f, 0.108f  + 0.484f, 1.53f),
                            new Vector3(-3.49f, 0.108f  + 0.484f, 0.01f),
        };
        for (int i = 0; i < middleTarget.Length; i++)
        {
            middleTarget[i] = (GameObject)Instantiate(Resources.Load("MiddleEffect"));
            middleTarget[i].transform.position = pos[i];
            middleTarget[i].AddComponent<BoxCollider>().isTrigger = true;
            middleTarget[i].GetComponent<BoxCollider>().size = new Vector3(0.3f, 0.3f, 0.3f);
            middleTarget[i].GetComponent<BoxCollider>().center = new Vector3(0f, -0.3f, 0f);
            middleTarget[i].layer = 8 + i;
        }

    }

    public override void Loop()
    {
        touchPlayer();
    }

}
