using UnityEngine;
using System.Collections;

class Scene2 : Modal
{
    bool middleEvent = false;
    Vector3[] pbezier = null;
    Vector3[] abezier = null;
    int cnt = 0;

    float lateTime = 0;

    public override void Init()
    {
        PlayerManager.GetInstance().getPlayerTransfrom().position = new Vector3(3.228084f, 1.894688f, 0.4327002f);

        PlayerManager.GetInstance().getPlayerTransfrom().rotation = Quaternion.Euler(0.0f, 180f, 0.0f);

        CameraManager.GetInstance().setPostion(new Vector3(12.74f, 23.6f, 16.68f));
        CameraManager.GetInstance().setAngle(Quaternion.Euler(45.36051f, 216.8188f, 3.58576f));
        PlayerManager.GetInstance().setCharState((int)CharState.walk);
    }
    public override void Loop()
    {

        if (PlayerManager.GetInstance().getCharState() == (int)CharState.walk)
        {
            PlayerManager.GetInstance().getPlayerTransfrom().GetComponent<Movement>().characterMove();
            AniController.GetInstance().setAniParameter(true);
            PlayerManager.GetInstance().getPlayerTransfrom().GetComponent<Movement>().setMoveSpeed(0.4f);
            AniController.GetInstance().setAniSpeed(1.2f);
        }
        else
        {
            if (!middleEvent)
            {
                AniController.GetInstance().setAniParameter(false);

                if (pbezier == null)
                {
                    Vector3 pos = CameraManager.GetInstance().getPostion();
                    Vector3 tar = new Vector3(2.7f, 3.14f, 19.09f);
                    pbezier = PlayerManager.GetInstance().getPlayerTransfrom().GetComponent<Movement>().CameraB(CameraManager.GetInstance().getPostion(), tar, 100);
                }
                else
                {
                    lateTime += Time.deltaTime;
                    if (cnt < pbezier.Length)
                    {
                        if (lateTime >= 0.03f)
                        {
                            CameraManager.GetInstance().getTransfrom().LookAt(PlayerManager.GetInstance().getPlayerTransfrom().position + (Vector3.left * -1.0f) + (Vector3.up * 1.5f));
                            CameraManager.GetInstance().setPostion(pbezier[cnt++]);
                            lateTime = 0;
                        }
                    }
                    else
                    {
                        middleEvent = true;
                        cnt = 0;
                        pbezier = null;
                    }
                }
            }
            else
            {
                if (pbezier == null)
                {
                    Vector3 pos = CameraManager.GetInstance().getPostion();
                    Vector3 tar = new Vector3(2.800406f, 3.344613f, 21.27f);
                    pbezier = PlayerManager.GetInstance().getPlayerTransfrom().GetComponent<Movement>().CameraB(CameraManager.GetInstance().getPostion(), tar, 100);
                }
                else
                {
                    lateTime += Time.deltaTime;
                    if (cnt < pbezier.Length)
                    {
                        if (lateTime >= 0.03f)
                        {
                            CameraManager.GetInstance().getTransfrom().LookAt(PlayerManager.GetInstance().getPlayerTransfrom().position + (Vector3.left * -1.0f) + (Vector3.up * 1.5f));
                            CameraManager.GetInstance().setPostion(pbezier[cnt++]);
                            lateTime = 0;
                        }
                    }
                    else
                    {
                        //Manager.GetInstance().setGameState((int)GameState.Destroy);
                    }
                }
            }
        }
    }

}
