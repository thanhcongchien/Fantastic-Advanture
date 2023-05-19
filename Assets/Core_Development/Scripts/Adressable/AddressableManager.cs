using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Events;

public class AddressableManager : MonoBehaviour
{
    public static AddressableManager Instance;
    private AsyncOperationHandle<SceneInstance> currentLoadSceneAsyncOperationHandle;
    private SceneInstance currentSceneInstance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

    }



    #region Addressable Load Scene

    private void DoLoadSingleScene(string keyScene, UnityAction onLoadedScene = null)
    {
        currentLoadSceneAsyncOperationHandle = Addressables.LoadSceneAsync(keyScene, UnityEngine.SceneManagement.LoadSceneMode.Single);
        currentLoadSceneAsyncOperationHandle.Completed += (asyncOperationHandle) =>
        {
            if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                currentSceneInstance = asyncOperationHandle.Result;
                onLoadedScene?.Invoke();
            }
        };
    }

    public static void LoadSingleScene(string keyScene, UnityAction onLoadedScene = null)
    {
        Instance.LoadSingleScenePrivate(keyScene, onLoadedScene);
    }

    private void LoadSingleScenePrivate(string keyScene, UnityAction onLoadedScene = null)
    {
        RemoveCurrentSingleScene(() =>
        {
            DoLoadSingleScene(keyScene, onLoadedScene);
        });
    }

    private void RemoveCurrentSingleScene(UnityAction callBack = null)
    {
        AsyncOperationHandle<SceneInstance> asyncOperationHandle = Addressables.UnloadSceneAsync(currentSceneInstance,UnityEngine.SceneManagement.UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        asyncOperationHandle.Completed += (asyncHandle) =>
        {
            callBack?.Invoke();
        };

        if (currentLoadSceneAsyncOperationHandle.IsValid())
        {
            Addressables.Release(currentLoadSceneAsyncOperationHandle);
        }
    }

    #endregion
}
