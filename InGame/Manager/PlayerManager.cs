using UnityEngine;
using System.Collections;

enum CharState
{
    idle,
    walk,
    jump,
    lookup
}

public class PlayerManager : MonoBehaviour {

    private static Transform playerTr;
    static CharState charState;

    private static PlayerManager instance;
    public static PlayerManager GetInstance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(PlayerManager)) as PlayerManager;
            if (!instance)
                Debug.LogError("There needs to be one active MyClass script on a GameObject in your scene.");
            if (!playerTr)
            {
                playerTr = GameObject.Find("Character").transform.FindChild("fox_walk3");
            }
        }

        return instance;
    }


    void Awake()
    {
        charState = CharState.idle;

    }

    Vector3 moveDirection;

    CharacterController charController;

    GameObject PlayerMove;

    public void playerMove()
    {
        
        playerTr.GetComponent<Movement>().MoveUpdate();
    }

    public int getCharState() { return (int)charState; }
    public void setCharState(int _charState) { charState = (CharState)_charState;}

    public void useGravity()
    {
        float gravity = 0.7f;

        moveDirection -= transform.up * gravity * Time.deltaTime;

        // Move the controller    
        charController.Move(moveDirection * Time.deltaTime); 
    }

    public Vector3 getPostion()
    {
        return playerTr.position;
    }

    public Transform getPlayerTransfrom()
    {
        //print(playerTr.localEulerAngles);
        return playerTr;
    }

    public CharacterController getPlayerCharController()
    {
        return charController;
    }
}
