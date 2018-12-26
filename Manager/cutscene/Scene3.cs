using UnityEngine;
using System.Collections;

class Scene3 : Modal
{
    bool middleEvent = false;
    float lateTime = 0;

    public override void Init()
    {
        PlayerManager.GetInstance().getPlayerTransfrom().position = new Vector3(0.064004f, 1.894688f, 0.4312458f);

        PlayerManager.GetInstance().getPlayerTransfrom().rotation = Quaternion.Euler(0.0f, 180f, 0.0f);

        CameraManager.GetInstance().setPostion(new Vector3(2.800406f, 3.344613f, 21.24796f));
        CameraManager.GetInstance().setAngle(Quaternion.Euler(359.8625f, 184.7513f, 0f));
    }
    public override void Loop()
    {
        PlayerManager.GetInstance().setCharState((int)CharState.lookup);
        AniController.GetInstance().setAniSpeed(0.20f);
        AniController.GetInstance().setLookUpAniParameter(true);

        if (AniController.GetInstance().checkAniComplete("lookup") && middleEvent != true)
        {
            Manager.GetInstance().setGameState((int)GameState.Destroy);
            middleEvent = true;
        }
    }
}
