using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    string currentSceneToLoad;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else if(Instance!= this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public string CurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
    public void TransitionToScene(string name)
    {
        UIFade.Instance.FadeOut();

        currentSceneToLoad = name;

        Invoke("LoadCurrentScene", 0.5f);
    }

    void LoadCurrentScene()
    {
        SceneManager.LoadScene(currentSceneToLoad);
    }
}
