using UnityEngine;
using System.Collections;

public class CutSceneManager : MonoBehaviour {
    private static CutSceneManager instance;
    public static CutSceneManager GetInstance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(CutSceneManager)) as CutSceneManager;
            if (!instance)
                Debug.LogError("There needs to be one active MyClass script on a GameObject in your scene.");
        }

        return instance;
    }
}
