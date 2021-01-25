using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowManager : MonoBehaviour
{
    // WindowId
    public const int EggChoiceWindow = 0;
    public const int TipsWindow = 1;
    public const int WindowID2 = 2;
    public const int WindowID3 = 3;
    public const int WindowID4 = 4;


    public GameObject[] Window;
    public GameObject WindowContent;
    public GameObject OverRay;
    private List<GameObject> Windows;

    private static WindowManager instance;
    public static WindowManager Instance
    {
        get
        {
            if(instance == null)
            {
                Debug.Log("WindowManager Instance Error");
            }
            else
            {
                return instance;
            }
            return null;
        }
    }
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        Windows = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ウィンドウを開く
    public void OpenWindow(int WindowId)
    {
        if(!WindowContent.activeSelf)
        {
            WindowContent.SetActive(true);
        }

        if(Windows.Count != 0)
        {
            OverRay.transform.SetAsLastSibling();
        }
        var obj = GameObject.Instantiate(Window[WindowId],WindowContent.transform);
        Windows.Add(obj);
    }
    
    // 現在開いている最前面のウィンドウを閉じる
    public void CloseFrontWindow()
    {
        var tmpIndex = Windows.Count - 1;

        Destroy(Windows[tmpIndex]);
        Windows.Remove(Windows[tmpIndex]);

        tmpIndex = Windows.Count - 1;
        if(tmpIndex >= 0)
        {
            OverRay.transform.SetSiblingIndex(tmpIndex);
        }
        
        // ウィンドウがなくなったらContentを閉じる(構造的に必要ないかも)
        WindowContent.SetActive(Windows.Count != 0);
    }

    // 開いているウィンドウを全て閉じる
    public void AllCloseWindow()
    {
        for(var i = Windows.Count; i > 0; i--)
        {
            Destroy(Windows[i - 1]);
            Windows.Remove(Windows[i - 1]);
        }
        // ウィンドウがなくなったらContentを閉じる(構造的に必要ないかも)
        WindowContent.SetActive(false);
    }
}
