using UnityEngine;
using System.Collections;

class Scene1_2 : Modal
{
    bool middleEvent = false;
    Vector3[] pbezier = null;
    Vector3[] abezier = null;
    int cnt = 0;

    float lateTime = 0;

    bool a = false;

    public override void Init()
    {
        PlayerManager.GetInstance().getPlayerTransfrom().position = new Vector3(0.5030836f, 0f, -1.015299f);

        PlayerManager.GetInstance().getPlayerTransfrom().rotation = Quaternion.Euler(0.0f, 270f, 0.0f);

        CameraManager.GetInstance().setPostion(new Vector3(13.36f, 1.2f, 1.3f));
        CameraManager.GetInstance().setAngle(Quaternion.Euler(2.900039f, 270.7f, -9.617269e-07f));
        PlayerManager.GetInstance().setCharState((int)CharState.walk);
        print(PlayerManager.GetInstance().getCharState());
    }

    public override void Loop()
    {
        PlayerManager.GetInstance().setCharState((int)CharState.walk);
        if (PlayerManager.GetInstance().getCharState() == (int)CharState.walk)
        {
            PlayerManager.GetInstance().getPlayerTransfrom().GetComponent<Movement>().characterMove();
            AniController.GetInstance().setAniParameter(true);
            PlayerManager.GetInstance().getPlayerTransfrom().GetComponent<Movement>().setMoveSpeed(0.4f);
            AniController.GetInstance().setAniSpeed(1.2f);
        }
        //else
        //{
        //    if (!a)
        //    {
        //        AniController.GetInstance().setAniParameter(false);
        //        AniController.GetInstance().setAniSpeed(0.25f);
        //        AniController.GetInstance().setLookUpAniParameter(true);
        //    }
        //    if (AniController.GetInstance().checkAniComplete("lookup") && middleEvent == false)
        //    {
        //        a = true;
        //        if (pbezier == null)
        //        {
        //            pbezier = PlayerManager.GetInstance().getPlayerTransfrom().GetComponent<Movement>().CameraB(CameraManager.GetInstance().getPostion(), new Vector3(17.1f, 1.2f, 1.3f), 100);
        //            abezier = PlayerManager.GetInstance().getPlayerTransfrom().GetComponent<Movement>().CameraB(CameraManager.GetInstance().getAngle(), new Vector3(0.18f, 270.7f, -8.804584e-07f), 100);
        //        }
        //        else
        //        {
        //            lateTime += Time.deltaTime;
        //            if (cnt < pbezier.Length)
        //            {
        //                if (lateTime >= 0.01f)
        //                {
        //                    CameraManager.GetInstance().setAngle(Quaternion.Euler(abezier[cnt]));
        //                    CameraManager.GetInstance().setPostion(pbezier[cnt++]);
        //                    lateTime = 0;
        //                }
        //            }
        //            else
        //            {
        //                middleEvent = true;
        //            }
        //        }
        //    }
        //    else if (AniController.GetInstance().checkAniComplete("lookup") && middleEvent == true)
        //    {
        //        if (TextManager.GetInstance().IsNull())
        //        {
        //            string[] fox = { "" };
        //            string[] other = { "" };
        //            Vector3 startpos = new Vector3(0, 0, 0);
        //            TextManager.GetInstance().SetText(startpos, fox, other);
        //        }

        //    }
        //    else if (AniController.GetInstance().checkAniPlaying("idle") && PlayerManager.GetInstance().getCharState() == (int)CharState.idle)
        //    {
        //        PlayerManager.GetInstance().setCharState((int)CharState.walk);
        //        AniController.GetInstance().setAniParameter(true);
        //        AniController.GetInstance().setAniSpeed(1.2f);
        //    }

        //    else if (AniController.GetInstance().checkAniPlaying("lookup") && PlayerManager.GetInstance().getCharState() == (int)CharState.lookup)
        //    {
        //        //print("inner");
        //        //if (TextManager.GetInstance().complete())
        //        //{
        //        //    print("inner2");
        //        //    AniController.GetInstance().setLookUpAniParameter(false);
        //        //    PlayerManager.GetInstance().setCharState((int)CharState.idle);
        //        //}
        //    }

        //    //print(AniController.GetInstance().getBool());


        //    //if ()
        //}
    }

}
