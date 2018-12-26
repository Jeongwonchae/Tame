using UnityEngine;
using System.Collections;

class Chapter1_2 : Modal
{
    GameObject[] middleTarget;

    public override void Init()
    {
        UIFigureManager.GetInstance().Init();

        CameraManager.GetInstance().getCamera().GetComponent<Camera>().fieldOfView = 11;
        dist = PlayerManager.GetInstance().getPostion() - CameraManager.GetInstance().getPostion();

        pBeginPos = PlayerManager.GetInstance().getPostion();

        mainCamera = GameObject.Find("Camera").transform;

        PlayerManager.GetInstance().getPlayerTransfrom().position = new Vector3(0.9970838f, 0.072f, 0.5027003f);

        PlayerManager.GetInstance().getPlayerTransfrom().rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);

        mainCamera.position = new Vector3(-15.765f, 13.955f, 13.561f);
        mainCamera.rotation = Quaternion.Euler(35.0632f, 134.1774f, 352.5446f);

        PlayerManager.GetInstance().getPlayerTransfrom().gameObject.SetActive(true);

        middleTarget = new GameObject[1];
        Vector3 pos = new Vector3(-0.012f, 0.172f + 0.484f, -0.038f);
        for (int i = 0 ; i < middleTarget.Length ; i ++)
        {
            middleTarget[i] = (GameObject)Instantiate(Resources.Load("MiddleEffect"));
            middleTarget[i].transform.position = pos;
            middleTarget[i].AddComponent<BoxCollider>().isTrigger = true;
            middleTarget[i].layer = 8+i;
            middleTarget[i].GetComponent<BoxCollider>().size = new Vector3(0.3f, 0.3f, 0.3f);
            middleTarget[i].GetComponent<BoxCollider>().center = new Vector3(0f, -0.3f, 0f);
        }

    }

    public override void Loop()
    {
        touchPlayer();
    }

}
