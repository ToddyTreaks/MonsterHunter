using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup _mainMenuPanel;
    [SerializeField] private CanvasGroup _optionsPanel;
    [SerializeField] private CanvasGroup _creditsPanel;
    
    [SerializeField] private CanvasGroup _blackerScreen;
    [SerializeField] private CanvasGroup _loadingScreen;
    [SerializeField] private Slider loadingSlider;
    
    void Start()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        _mainMenuPanel.gameObject.SetActive(true);
        _optionsPanel.gameObject.SetActive(false);
        _creditsPanel.gameObject.SetActive(false);
        _loadingScreen.gameObject.SetActive(false);
        _blackerScreen.gameObject.SetActive(false);
    }

    


    public void PlayGame()
    {
        StartCoroutine(LoadSceneAsync("_Main"));
    }
    
    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        _mainMenuPanel.gameObject.SetActive(false);
        _mainMenuPanel.alpha = 0;
        
        _loadingScreen.gameObject.SetActive(true);
        _loadingScreen.alpha = 1;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingSlider.value = progress;
            
            yield return null;
        }
    }
    
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void CreditMenu()
    {
        _mainMenuPanel.gameObject.SetActive(false);
        _blackerScreen.gameObject.SetActive(true);
        _creditsPanel.gameObject.SetActive(true);
        
    }
    
    public void OptionsMenu()
    {
        _mainMenuPanel.gameObject.SetActive(false);
        _blackerScreen.gameObject.SetActive(true);
        _optionsPanel.gameObject.SetActive(true);
    }

    public void OptionsMainMenuButton()
    {
        _optionsPanel.gameObject.SetActive(false);
        _blackerScreen.gameObject.SetActive(false);
        _mainMenuPanel.gameObject.SetActive(true);
    }
    
    public void CreditsMainMenuButton()
    {
        _creditsPanel.gameObject.SetActive(false);
        _blackerScreen.gameObject.SetActive(false);
        _mainMenuPanel.gameObject.SetActive(true);
    }
    
    
        
}
