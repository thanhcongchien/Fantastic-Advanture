using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ServiceLocator.Get<GameUIManager>().GetComponentInChildren<Canvas>().worldCamera = Camera.main;
    }

}
