using UnityEngine;
using System.Collections;
using System.Reflection;
using System;

public class MiddleTarget : MonoBehaviour {

    private static MiddleTarget instance;
    public static MiddleTarget GetInstance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(MiddleTarget)) as MiddleTarget;
            if (!instance)
                Debug.LogError("There needs to be one active MyClass script on a GameObject in your scene.");
        }

        return instance;
    }

    bool playingMiddle = false;

    public bool getPlaying() { return playingMiddle; }

    public void startAction(int layer)
    {
        playingMiddle = true;
        StartCoroutine(WaitIdle(layer));
    }

    private IEnumerator WaitIdle(int layer)
    {
        yield return new WaitForSeconds(1.0f);
        while (PlayerManager.GetInstance().getCharState() == (int)CharState.walk)
        {
            yield return null;
        }

        string name = "Action" + (UIController.GetInstance().getCurChapter()-Manager.GetInstance().getSceneCount() +1).ToString() + "_" + (layer - 7).ToString();
        MethodInfo _method = this.GetType().GetMethod(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        print(name);

        //if (_method != null)
        //{
        //    _method.Invoke(this, null);
        //}
        StartCoroutine(name + "_Corutine");

       

        yield break;
    }
    private IEnumerator Action3_1_Corutine()
    {
        PlayerManager.GetInstance().getPlayerTransfrom().GetComponent<Movement>().setMoveSpeed(0.85f);

        GameObject bridge = GameObject.Find("bridge");

        int[] val = { 1, 2, 0 };

        for (int i = 0; i < bridge.transform.childCount; i++)
        {
            yield return new WaitForSeconds(1.0f);
            Transform child = bridge.transform.GetChild(val[i]);
            child.gameObject.SetActive(true);
            for (int j = 0; j < child.childCount; j++)
            {
                child.GetChild(j).gameObject.SetActive(true);
            }
        }

        PlayerManager.GetInstance().getPlayerTransfrom().rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);

        PlayerManager.GetInstance().setCharState((int)CharState.walk);
        AniController.GetInstance().setJAniParameter(true);
        yield return new WaitForSeconds(3.5f);
        AniController.GetInstance().setJAniParameter(false);
        while (!AniController.GetInstance().checkAniPlaying("idle"))
        {
            yield return null;
        }
        ObjectController.GetInstance().setAlign();

        yield break;
    }

    private IEnumerator Action4_1_Corutine()
    {
        PlayerManager.GetInstance().getPlayerTransfrom().GetComponent<Movement>().setMoveSpeed(0.85f);

        GameObject bridge = GameObject.Find("bridge");

        for (int i = 0; i < bridge.transform.childCount; i++)
        {
            Transform child = bridge.transform.GetChild(i);
            child.gameObject.SetActive(true);
        }

        PlayerManager.GetInstance().getPlayerTransfrom().rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        ObjectController.GetInstance().setActive(true);
        ObjectController.GetInstance().setAlign();

        yield break;
    }

    private IEnumerator Action4_2_Corutine()
    {
        yield return new WaitForSeconds(.5f);

        PlayerManager.GetInstance().getPlayerTransfrom().position = new Vector3(-4.003916f, 1.044688f, 1.502701f);

        PlayerManager.GetInstance().getPlayerTransfrom().rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
        ObjectController.GetInstance().setActive(true);
        ObjectController.GetInstance().setAlign();

        yield break;
    }

    private IEnumerator Action5_1_Corutine()
    {
        yield return new WaitForSeconds(.5f);


        GameObject bridge = GameObject.Find("bridge");

        for (int i = 0; i < bridge.transform.childCount; i++)
        {
            Transform child = bridge.transform.GetChild(i);
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.0f);
        }

        PlayerManager.GetInstance().getPlayerTransfrom().rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        ObjectController.GetInstance().setActive(true);
        ObjectController.GetInstance().setAlign();

        yield break;
    }

    private IEnumerator Action5_2_Corutine()
    {
        yield return new WaitForSeconds(.5f);

        PlayerManager.GetInstance().getPlayerTransfrom().position = new Vector3(-0.5029158f, 0.072f, 2.9987f);

        PlayerManager.GetInstance().getPlayerTransfrom().rotation = Quaternion.Euler(0.0f, 180f, 0.0f);

        ObjectController.GetInstance().setActive(true);
        ObjectController.GetInstance().setAlign();

        yield break;
    }

    private IEnumerator Action5_3_Corutine()
    {
        yield return new WaitForSeconds(.5f);


        GameObject bridge = GameObject.Find("bridge1");

        for (int i = 0; i < bridge.transform.childCount; i++)
        {
            Transform child = bridge.transform.GetChild(i);
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.0f);
        }

        PlayerManager.GetInstance().getPlayerTransfrom().rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        ObjectController.GetInstance().setActive(true);
        ObjectController.GetInstance().setAlign();

        yield break;
    }

    private IEnumerator Action5_4_Corutine()
    {
        yield return new WaitForSeconds(.5f);


        PlayerManager.GetInstance().getPlayerTransfrom().position = new Vector3(-6.000916f, 0.9f, 2.500701f);

        PlayerManager.GetInstance().getPlayerTransfrom().rotation = Quaternion.Euler(0.0f, 90f, 0.0f);
        ObjectController.GetInstance().setActive(true);
        ObjectController.GetInstance().setAlign();

        yield break;
    }

    private IEnumerator Action6_1_Corutine()
    {
        PlayerManager.GetInstance().getPlayerTransfrom().GetComponent<Movement>().setMoveSpeed(0.85f);

        GameObject bridge = GameObject.Find("bridge");

        for (int i = 0; i < bridge.transform.childCount; i++)
        {
            yield return new WaitForSeconds(1.0f);
            Transform child = bridge.transform.GetChild(i);
            child.gameObject.SetActive(true);
            for (int j = 0; j < child.childCount; j++)
            {
                child.GetChild(j).gameObject.SetActive(true);
            }
        }

        PlayerManager.GetInstance().getPlayerTransfrom().rotation = Quaternion.Euler(0.0f, 270.0f, 0.0f);

        PlayerManager.GetInstance().setCharState((int)CharState.walk);
        AniController.GetInstance().setJAniParameter(true);
        yield return new WaitForSeconds(2.3f);
        AniController.GetInstance().setJAniParameter(false);
        while (!AniController.GetInstance().checkAniPlaying("idle"))
        {
            yield return null;
        }
        ObjectController.GetInstance().setAlign();

        yield break;
    }

    private IEnumerator Action6_2_Corutine()
    {

        GameObject island = GameObject.Find("island");

        while (island.transform.position.y <= -0.8210)
        {
            Vector3 pos = island.transform.position;
            float move = island.transform.position.y + 4 * Time.deltaTime;
            island.transform.position = new Vector3(pos.x, move, pos.z);
            yield return new WaitForSeconds(0.01f);
        }

        playingMiddle = false;

        yield break;

    }

    private IEnumerator Action6_3_Corutine()
    {
        yield return new WaitForSeconds(.5f);

        PlayerManager.GetInstance().getPlayerTransfrom().position = new Vector3(2.386f, -0.8053124f, 0.644f);

        PlayerManager.GetInstance().getPlayerTransfrom().rotation = Quaternion.Euler(0.0f, 0f, 0.0f);

        ObjectController.GetInstance().setActive(true);
        ObjectController.GetInstance().setAlign();

        yield break;

    }

    private IEnumerator Action6_4_Corutine()
    {
        yield return new WaitForSeconds(.5f);

        PlayerManager.GetInstance().getPlayerTransfrom().position = new Vector3(1.958f, 0.1286876f, 3.1387f);

        PlayerManager.GetInstance().getPlayerTransfrom().rotation = Quaternion.Euler(0.0f, 270f, 0.0f);

        ObjectController.GetInstance().setActive(true);
        ObjectController.GetInstance().setAlign();

        yield break;

    }

    private IEnumerator Action7_1_Corutine()
    {

        GameObject island = GameObject.Find("Island1");

        for (int i = 0; i < island.transform.childCount; i++)
        {
            Transform child = island.transform.GetChild(i);
            child.gameObject.SetActive(true);
            for (int j = 0; j < child.childCount; j++)
            {
                child.GetChild(j).gameObject.SetActive(true);
            }
        }

        yield break;

    }

    private IEnumerator Action7_2_Corutine()
    {

        GameObject bridge = GameObject.Find("bridge");

        for (int i = 0; i < bridge.transform.childCount; i++)
        {
            yield return new WaitForSeconds(1.0f);
            Transform child = bridge.transform.GetChild(i);
            child.gameObject.SetActive(true);
            for (int j = 0; j < child.childCount; j++)
            {
                child.GetChild(j).gameObject.SetActive(true);
            }
        }

        yield break;

    }

    private IEnumerator Action7_3_Corutine()
    {
        yield return new WaitForSeconds(.5f);

        PlayerManager.GetInstance().getPlayerTransfrom().position = new Vector3(-1.47f, 1.244688f, 9.965f);

        PlayerManager.GetInstance().getPlayerTransfrom().rotation = Quaternion.Euler(0.0f, 90f, 0.0f);

        ObjectController.GetInstance().setActive(true);
        ObjectController.GetInstance().setAlign();

        yield break;

    }

    private IEnumerator Action7_4_Corutine()
    {

        GameObject island = GameObject.Find("Island2");

        for (int i = 0; i < island.transform.childCount; i++)
        {
            Transform child = island.transform.GetChild(i);
            child.gameObject.SetActive(true);
            for (int j = 0; j < child.childCount; j++)
            {
                child.GetChild(j).gameObject.SetActive(true);
            }
        }


        yield break;

    }

    private IEnumerator Action7_5_Corutine()
    {

        GameObject island = GameObject.Find("Island3");

        for (int i = 0; i < island.transform.childCount; i++)
        {
            Transform child = island.transform.GetChild(i);
            child.gameObject.SetActive(true);
            for (int j = 0; j < child.childCount; j++)
            {
                child.GetChild(j).gameObject.SetActive(true);
            }
        }

        playingMiddle = false;

        yield break;

    }

    private IEnumerator Action7_7_Corutine()
    {

        GameObject bridge = GameObject.Find("bridge2");

        for (int i = 0; i < bridge.transform.childCount; i++)
        {
            Transform child = bridge.transform.GetChild(i);
            child.gameObject.SetActive(true);
            for (int j = 0; j < child.childCount; j++)
            {
                child.GetChild(j).gameObject.SetActive(true);
            }
        }


        yield break;

    }

    private IEnumerator Action7_8_Corutine()
    {

        GameObject bridge = GameObject.Find("bridge3");

        for (int i = 0; i < bridge.transform.childCount; i++)
        {
            Transform child = bridge.transform.GetChild(i);
            child.gameObject.SetActive(true);
            for (int j = 0; j < child.childCount; j++)
            {
                child.GetChild(j).gameObject.SetActive(true);
            }
        }


        yield break;

    }

    private IEnumerator Action7_9_Corutine()
    {

        GameObject island = GameObject.Find("Island4");

        for (int i = 0; i < island.transform.childCount; i++)
        {
            Transform child = island.transform.GetChild(i);
            child.gameObject.SetActive(true);
            for (int j = 0; j < child.childCount; j++)
            {
                child.GetChild(j).gameObject.SetActive(true);
            }
        }


        yield break;

    }

    private IEnumerator Action7_10_Corutine()
    {

        yield return new WaitForSeconds(.5f);

        PlayerManager.GetInstance().getPlayerTransfrom().position = new Vector3(-3.031f, 2.531688f, 2.531688f);

        PlayerManager.GetInstance().getPlayerTransfrom().rotation = Quaternion.Euler(0.0f, 180f, 0.0f);

        ObjectController.GetInstance().setActive(true);
        ObjectController.GetInstance().setAlign();

        yield break;

    }
}
