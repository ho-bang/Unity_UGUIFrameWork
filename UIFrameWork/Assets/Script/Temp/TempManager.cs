using System.Collections;
using Assets.Script.Scene;
using UnityEngine;

public class TempManager : MonoBehaviour
{
    public Canvas uiCan;
    private SceneLoader _sceneLoader;

    void Awake()
    {
        _sceneLoader = gameObject.AddComponent<SceneLoader>();
        _sceneLoader.LoadUIScene("UIScene", (a) =>
        {
            Debug.Log("Success");
        });
    }
}
