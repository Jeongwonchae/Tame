using UnityEngine;
using System.Collections;

public class UIFigureManager : MonoBehaviour
{

    private static UIFigureManager instance;
    public static UIFigureManager GetInstance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(UIFigureManager)) as UIFigureManager;
            if (!instance)
                Debug.LogError("There needs to be one active MyClass script on a GameObject in your scene.");
        }

        return instance;
    }

    UIController c_UI;

    int[] anArray;

    int[] List;

	// Use this for initialization
	public void Init () {
        anArray = UIController.GetInstance().getChapterFigureList(UIController.GetInstance().getCurChapter());
    }

    public void printArray()
    {
        for (int i = 0; i < anArray.Length; i++)
        {
            print(anArray[i]);
        }
    }
}
