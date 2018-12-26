using UnityEngine;
using System.Collections;

public class AniController : MonoBehaviour {

    private static AniController instance;
    public static AniController GetInstance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(AniController)) as AniController;
            if (!instance)
                Debug.LogError("There needs to be one active MyClass script on a GameObject in your scene.");
        }
        if (!playerAni)
        {
            playerAni = PlayerManager.GetInstance().getPlayerTransfrom().GetComponent<Animator>();
            playerAni.speed = 1.3f;
        }
        return instance;
    }

    static Animator playerAni;

    public void setAniParameter(bool _walk)
    {
        playerAni.SetBool("walk", _walk);
        if (!_walk)
        {
            //SoundManager.getInstance.StopLoopEvent();
        }
        else
        {
            //SoundManager.getInstance.ESWalk();
        }
    }
    public void setJAniParameter(bool _jump)
    {
        playerAni.SetBool("jump", _jump);
        setAniSpeed(1.1f);
    }

    public void setLookUpAniParameter(bool _lookup)
    {
        playerAni.SetBool("lookup", _lookup);
    }

    public void setLookDownAniParameter(bool _lookdown)
    {
        playerAni.SetBool("lookdown", _lookdown);
    }

    public bool checkAniComplete(string name)
    {
        if(playerAni.GetCurrentAnimatorStateInfo(0).IsName(name))
        {
            return playerAni.GetCurrentAnimatorStateInfo(0).length > playerAni.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }

        return false;
    }

    public bool checkAniPlaying(string name)
    {
        if (playerAni.GetCurrentAnimatorStateInfo(0).IsName(name))
        {
            return true;
        }

        return false;
    }

    public bool getBool()
    {
        return playerAni.GetBool("lookup");
    }

    public void setAniSpeed(float _aniSpeed) { playerAni.speed = _aniSpeed; }
    public void printAniSpeed() { print(playerAni.speed); }
}
