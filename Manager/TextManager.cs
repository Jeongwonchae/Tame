using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

    private static TextManager instance;
    public static TextManager GetInstance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(TextManager)) as TextManager;
            if (!instance)
                Debug.LogError("There needs to be one active MyClass script on a GameObject in your scene.");
        }

        return instance;
    }

    GameObject fox;
    GameObject other;

    int foxcnt;
    int othercnt;

    string[] foxString = null;
    string[] otherString = null;

    IEnumerator Fadeout(GameObject _text,int num)
    {

        Vector3 pos = _text.transform.position;
        for (float i = 1.0f; i >= 0; i -= 0.01f)
        {
            _text.transform.position = new Vector3(pos.x, pos.y - 70 * (1 - i), pos.z);

            Color color = new Vector4(0, 0, 0, i);
            _text.GetComponent<Text>().color = color;
            yield return new WaitForSeconds(0.01f);
        }

        pos.y += 70;

        if (num == 0)
        {
            if (foxcnt < foxString.Length)
                StartCoroutine(Fadein(_text, pos, num));
            else
            {
                foxcnt++;
            }
        }
        else
            if (othercnt < otherString.Length)
                StartCoroutine(Fadein(_text, pos, num));
            else
            {
                othercnt++;
            }

        print(foxcnt);
        print(othercnt);
    }

    IEnumerator Fadein(GameObject _text, Vector3 _startPos, int num)
    {
        if (num == 0)
            _text.GetComponent<Text>().text = foxString[foxcnt++];
        else
            _text.GetComponent<Text>().text = otherString[othercnt++];

        _text.gameObject.transform.position = new Vector3(_startPos.x, _startPos.y + 70, _startPos.z);
        
        for (float i = 0.0f; i <= 1; i += 0.01f)
        {
            _text.gameObject.transform.position = new Vector3(_startPos.x, _startPos.y - 70 * i, _startPos.z);
            Color color = new Vector4(0, 0, 0, i);
            _text.GetComponent<Text>().color = color;
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Fadeout(_text, num));
    }

    public void Init()
    {
        fox = GameObject.Find("UI").transform.FindChild("SceneText").transform.FindChild("fox").gameObject;
        other = GameObject.Find("UI").transform.FindChild("SceneText").transform.FindChild("other").gameObject;

        fox.GetComponent<Text>().color = new Vector4(0f, 0f, 0f, 0.0f);
        other.GetComponent<Text>().color = new Vector4(0f, 0f, 0f, 0.0f);
    }

    public bool complete() {
        if (foxcnt == foxString.Length+1 && othercnt == otherString.Length+1)
        {
            foxString = null; otherString = null;
            return true;
        }
        return false ;
    }
    public bool IsNull() { return (foxString == null && foxString == null); }

    public void SetText(Vector3 StartPos, string[] _foxString, string[] _otherString)
    {
        foxString = _foxString;
        otherString = _otherString;
        foxcnt = 0;
        othercnt = 0;
        StartPos.x += 200;

        Vector3 identity = new Vector3(451, 282, 0);

        StartCoroutine(Fadein(fox, identity + StartPos, 0));
        StartPos.x -= 400;
        StartCoroutine(Fadein(other, identity + StartPos, 1));
        print(StartPos);
    }
}