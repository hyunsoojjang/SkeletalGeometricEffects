using System;
using System.Collections.Generic;
using UnityEngine;

public class MainThreadDispatcher : MonoBehaviour
{
    private static MainThreadDispatcher _instance;

    public static MainThreadDispatcher Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MainThreadDispatcher>();
                if (_instance == null)
                {
                    var go = new GameObject("MainThreadDispatcher");
                    _instance = go.AddComponent<MainThreadDispatcher>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        // Awake 함수 내에서 FindObject를 호출합니다.
        _instance = FindObjectOfType<MainThreadDispatcher>();
        if (_instance == null)
        {
            Debug.Log("MainThreadDispatcher create");
            var go = new GameObject("MainThreadDispatcher");
            _instance = go.AddComponent<MainThreadDispatcher>();
            //DontDestroyOnLoad(_instance);
        }
    }
    public void DispatchToMainThread(Action action)
    {
        lock (this)
        {
            actionQueue.Enqueue(action);
        }
    }

    private Queue<Action> actionQueue = new Queue<Action>();

    private void Update()
    {
        lock (this)
        {
            while (actionQueue.Count > 0)
            {
                actionQueue.Dequeue().Invoke();
            }
        }
    }
}