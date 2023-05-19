using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameUIManager 
{
    void ShowUIView<T>(params object[] obj);
    void AddBaseUIView();
    BaseUIView GetBaseUiView<T>();
    void CloseCurrentUIView();
    void CloseAllUIView();
    void CloseUIView<T>();
    void RemoveUIView<T>();
    void ClearCurrentPopup();
}
