using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class Home : MonoBehaviour
{
    void Start()
    {
        MusicController.Music.BG_menu();

        DOVirtual.DelayedCall(1, () => 
        {
            AddressableManager.LoadSingleScene(Definition.SceneDefinition.ADDRESSABLE_MAP_SCENE);
        });
    }

    void Update()
    {
        // Exit game if click Escape key or back on mobile
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitOK();
        }
    }

    /// <summary>
    /// Exit game
    /// </summary>
    public void ExitOK()
    {
        Application.Quit();
    }

}
