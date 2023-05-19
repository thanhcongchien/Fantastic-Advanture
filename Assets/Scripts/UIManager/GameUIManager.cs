using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameUIManager : MonoBehaviour, IGameUIManager
{
    [SerializeField] BaseUIView[] baseUIViews;
    private Dictionary<string, BaseUIView> poolBaseUIViews;
    private BaseUIView _currentUIView;

    private void Awake()
    {
        
    }

    private void Start()
    {
        if (ServiceLocator.Get<GameUIManager>() == null)
        {
            ServiceLocator.Set(this);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (ServiceLocator.Get<GameUIManager>() != this)
            {
                Destroy(this.gameObject);
                return;
            }
        }

        poolBaseUIViews = new Dictionary<string, BaseUIView>();
        AddBaseUIView();
    }

    private void OnDestroy()
    {
       
    }
    public void AddBaseUIView()
    {
        BaseUIView baseUIView;
        for (int i = 0; i < baseUIViews.Length; i++)
        {
            baseUIView = baseUIViews[i];
            poolBaseUIViews.Add(baseUIView.GetType().ToString(), baseUIView);
        }
    }

    public void CloseAllUIView()
    {
        if (poolBaseUIViews == null) return;
        for (int i = 0; i < poolBaseUIViews.Count; i++)
        {
            poolBaseUIViews.ElementAt(i).Value.Close();
        }
        _currentUIView = null;
    }

    public void CloseCurrentUIView()
    {
        if (_currentUIView == null) return;
        _currentUIView.Close();
        _currentUIView = null;
    }

    public void CloseUIView<T>()
    {
        if (poolBaseUIViews.ContainsKey(typeof(T).Name))
        {
            poolBaseUIViews[typeof(T).Name].Close();
        }
    }

    public void RemoveUIView<T>()
    {
        if (poolBaseUIViews.ContainsKey(typeof(T).Name))
        {
            poolBaseUIViews.Remove(typeof(T).Name);
        }
    }

    public void ShowUIView<T>(params object[] obj)
    {
        if (poolBaseUIViews.ContainsKey(typeof(T).Name))
        {
            poolBaseUIViews[typeof(T).Name].Open(obj);
            _currentUIView = poolBaseUIViews[typeof(T).Name];
        }
    }
    public void ClearCurrentPopup()
    {
        _currentUIView = null;
    }

    public BaseUIView GetBaseUiView<T>()
    {
        if (poolBaseUIViews.ContainsKey(typeof(T).Name))
        {
            return poolBaseUIViews[typeof(T).Name];
        }
        return null;
    }
}
