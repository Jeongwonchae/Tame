using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class UIButton : MonoBehaviour {

    Button myButton;

    bool visible;

    GameObject BgSound;

    // Use this for initialization
    void Awake () {
        myButton = GetComponent<Button>(); // <-- you get access to the button component here

        myButton.onClick.AddListener(myFunctionForOnClickEvent);
    }
	
    void myFunctionForOnClickEvent()
    {


        if (visible)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;

        visible = !visible;

        //if (visible)
            //SoundManager.getInstance.BGSPause();
        //else
            //SoundManager.getInstance.BGSIngame();
    }
}
