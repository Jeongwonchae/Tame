using UnityEngine;
using System.Collections;

class Chapter2_2 : Modal
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

        mainCamera.position = new Vector3(8.41f, 11.72f, 12.27f);
        mainCamera.rotation = Quaternion.Euler(38.95237f, 216.5022f, 353.8739f);

        PlayerManager.GetInstance().getPlayerTransfrom().gameObject.SetActive(true);

        middleTarget = new GameObject[4];
        Vector3[] pos = {
                            new Vector3(-0.968f, 0.108f + 0.484f, 0.983f),
                            new Vector3(-2f, 0.965f + 0.484f, -0.524f),
                            new Vector3(-1.455f, 0.108f + 0.484f, 2.496f),
                            new Vector3(-4.479f, 0.057f + 0.484f, 2.502f)            
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

        middleTarget[1].SetActive(false);

    }

    public override void Loop()
    {
        touchPlayer();
    }

}
