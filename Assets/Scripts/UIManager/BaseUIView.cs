using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUIView : MonoBehaviour
{
    public virtual void Open(params object[] obj)
    {
        gameObject.SetActive(true);
    }
    public virtual void Close()
    {
        gameObject.SetActive(false);
        ServiceLocator.GetUIViewManager.ClearCurrentPopup();
    }
}
