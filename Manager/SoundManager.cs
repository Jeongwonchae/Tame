using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{

    private static SoundManager _instance = null;
    public static SoundManager getInstance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType(typeof(SoundManager)) as SoundManager;
            return _instance;
        }
    }
    void OnDestory() { _instance = null; }

    /* ********************************************************************** */

    private bool b_bg = true;
    private bool b_eff = true;

    private AudioSource asBGS;
    private AudioSource asES;
    private AudioSource loopEvent;

    // 배경음
    public AudioClip m_BGS_Title;
    public AudioClip m_BGS_Ingame;

    public AudioClip m_ES_Walk;

    public AudioClip m_ES_LoadMap;
    public AudioClip m_ES_MiddleTarget;

    /* ********************************************************************** */
    // ------------------------ //
    private int loadIndex;
    private static int loadCount;
    // ------------------------ //

    void Awake()
    {
        // ------------------------ //

        loadIndex = loadCount;
        loadCount++;

        if (loadIndex == 0)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // ------------------------ //

        asBGS = GetComponent<AudioSource>();
        asES = gameObject.AddComponent<AudioSource>();
        loopEvent = gameObject.AddComponent<AudioSource>();

        // ------------------------ //
    }

    public void BGSTitle()
    {
        if (b_bg)
        {
            asBGS.clip = m_BGS_Title;
            asBGS.loop = true;
            asBGS.Play();
        }
    }

    public void BGSStayStop()
    {
        asBGS.Stop();
    }

    public void BGSIngame()
    {
        if (b_bg)
        {
            asBGS.clip = m_BGS_Ingame;
            asBGS.loop = true;
            asBGS.Play();
        }
    }

    public void setBGSound(bool bg) {
        b_bg = bg;
        if (bg)
        {
            asBGS.loop = true;
            asBGS.Play();
        }
        else
        {
            asBGS.Stop();
        }
    }

    public void setEFFSound(bool eff)
    {
        b_eff = eff;
        if (!eff)
        {
            loopEvent.Stop();
        }
    }

    public void BGSStop() { asBGS.Stop(); }
    public void BGSPause() { asBGS.Pause(); }

    // ////////////////////////////////////////////////////


    public void ESWalk()
    {
        if (b_eff)
        {
            loopEvent.clip = m_ES_Walk;
            loopEvent.loop = true;
            loopEvent.Play();
        }
    }

    public void ESLoadMap()
    {
        if (b_eff)
        {
            loopEvent.clip = m_ES_LoadMap;
            loopEvent.loop = true;
            loopEvent.Play();
        }
    }

    public void StopLoopEvent()
    {
        loopEvent.Stop();
    }

    public void ESMiddleTarget() { asES.PlayOneShot(m_ES_MiddleTarget); }

    /* ********************************************************************** */
}
