using UnityEngine;
using System.Collections;

class Chapter3_1 : Modal
{
    GameObject[] middleTarget;

    public override void Init()
    {
        UIFigureManager.GetInstance().Init();

        dist = PlayerManager.GetInstance().getPostion() - CameraManager.GetInstance().getPostion();

        pBeginPos = PlayerManager.GetInstance().getPostion();

        mainCamera = GameObject.Find("Camera").transform;

        PlayerManager.GetInstance().getPlayerTransfrom().position = new Vector3(0.9480836f, 3.071688f, -0.3492994f);

        PlayerManager.GetInstance().getPlayerTransfrom().rotation = Quaternion.Euler(0.0f, 270.0f, 0.0f);

        mainCamera.position = new Vector3(16.4f, 17.7f, 16.3f);
        mainCamera.rotation = Quaternion.Euler(35.58765f, 230.2001f, 356.4632f);

        PlayerManager.GetInstance().getPlayerTransfrom().gameObject.SetActive(true);

        Vector3[] pos = {new Vector3(0.959f, 3.112f + 0.484f, 1.661f),
                            new Vector3(1.49f, 0.105f + 0.484f, -0.831f),
                            new Vector3(1.992f, 0.118f + 0.484f, 1.674f),
                            new Vector3(3.459f, -0.797f + 0.484f, -0.846f)
        };
        middleTarget = new GameObject[pos.Length];
        for (int i = 0; i < middleTarget.Length; i++)
        {
            middleTarget[i] = (GameObject)Instantiate(Resources.Load("MiddleEffect"));
            middleTarget[i].transform.position = pos[i];
            middleTarget[i].AddComponent<BoxCollider>().isTrigger = true;
            middleTarget[i].layer = 8 + i;
            middleTarget[i].GetComponent<BoxCollider>().size = new Vector3(0.3f, 0.3f, 0.3f);
            middleTarget[i].GetComponent<BoxCollider>().center = new Vector3(0f, -0.3f, 0f);
        }

        middleTarget[3].SetActive(false);

    }

    bool live = false;

    public override void Loop()
    {
        touchPlayer();

        if (MiddleTarget.GetInstance().getPlaying())
        {
            live = true;
        }

        if (live)
        {
            if (!MiddleTarget.GetInstance().getPlaying())
            {
                middleTarget[3].SetActive(true);
                live = false;
            }
        }

    }

}
