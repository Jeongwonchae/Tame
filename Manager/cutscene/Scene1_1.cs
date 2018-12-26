using UnityEngine;
using System.Collections;

class Scene1_1 : Modal
{
    bool a = false;
    public override void Init()
    {
        PlayerManager.GetInstance().getPlayerTransfrom().position = new Vector3(0.5030836f, 0f, -1.015299f);

        PlayerManager.GetInstance().getPlayerTransfrom().rotation = Quaternion.Euler(0.0f, 270f, 0.0f);

        CameraManager.GetInstance().getCamera().GetComponent<Camera>().fieldOfView = 11;

        CameraManager.GetInstance().setPostion(new Vector3(13.36f, 1.2f, 1.3f));
        CameraManager.GetInstance().setAngle(Quaternion.Euler(2.900039f, 270.7f, -9.617269e-07f));
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
            AniController.GetInstance().setLookUpAniParameter(false);
        }
        else
        {
            if (!a)
            {
                AniController.GetInstance().setAniParameter(false);
                AniController.GetInstance().setAniSpeed(0.25f);
                AniController.GetInstance().setLookUpAniParameter(true);
            }
            if (AniController.GetInstance().checkAniComplete("lookup"))
            {
                a = true;
                AniController.GetInstance().setLookUpAniParameter(false);
                PlayerManager.GetInstance().setCharState((int)CharState.idle);
            }
            else if (AniController.GetInstance().checkAniPlaying("idle") && PlayerManager.GetInstance().getCharState() == (int)CharState.idle)
            {
                PlayerManager.GetInstance().setCharState((int)CharState.walk);
                AniController.GetInstance().setAniParameter(true);
                AniController.GetInstance().setAniSpeed(1.2f);
            }

            //if ()
        }
    }

}
