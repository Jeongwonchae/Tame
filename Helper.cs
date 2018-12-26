using UnityEngine;
using System.Collections;
using System;

public class Helper : MonoBehaviour {

    private static Helper instance;
    public static Helper GetInstance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(Helper)) as Helper;
            if (!instance)
                Debug.LogError("There needs to be one active MyClass script on a GameObject in your scene.");
        }

        return instance;
    }

    public bool IscTouch(RectTransform _rt)
    {
        if (Vector3.Distance(_rt.position, Input.GetTouch(0).position) <= _rt.rect.width / 2)
        {
            return true;
        }
        return false;
    }

    public bool IsrTouch(RectTransform _rt)
    {
        Vector2 touchPoint = Input.GetTouch(0).position;
        Vector2 leftTop = new Vector2(_rt.position.x - _rt.rect.width / 2, _rt.position.y - _rt.rect.height / 2);
        Vector2 rightBottom = new Vector2(_rt.position.x + _rt.rect.width / 2, _rt.position.y + _rt.rect.height / 2);

        if (touchPoint.x > leftTop.x && touchPoint.x < rightBottom.x && touchPoint.y > leftTop.y && touchPoint.y < rightBottom.y)
        {
            return true;
        }
        return false;
    }

    public object getClassType(string className)
    {
        Type elementType = Type.GetType(className);
        return Activator.CreateInstance(elementType);
    }
}
