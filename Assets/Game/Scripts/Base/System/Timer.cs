using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private static Timer _instance;

    private static Timer Instance
    {
        get
        {
            if (_instance == null)
            {
                var go = new GameObject("Timer");
                DontDestroyOnLoad(go);
                _instance = go.AddComponent<Timer>();
            }
            return _instance;
        }
    }

    /// <summary>
    /// 延迟执行一个 Action，类似于 JavaScript 的 setTimeout
    /// </summary>
    public static void SetTimeout(float delaySeconds, Action action)
    {
        Instance.StartCoroutine(Instance.ExecuteAfterDelay(delaySeconds, action));
    }

    private IEnumerator ExecuteAfterDelay(float delaySeconds, Action action)
    {
        yield return new WaitForSeconds(delaySeconds);
        action?.Invoke();
    }
}

