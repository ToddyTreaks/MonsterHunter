using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private GameObject _optionsPanel;
    [SerializeField] private GameObject _creditsPanel;
    
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Slider loadingSlider;
    
    [SerializeField] private GameObject logo;
    [SerializeField] private GameObject buttonsMainMenu;

    
    
    void Start()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            logo.GetComponent<Animator>().SetTrigger("OnAnyKeyDown");
            buttonsMainMenu.GetComponent<Animator>().SetTrigger("OnAnyKeyDown");
        }
    }

    public void PlayGame()
    {
        StartCoroutine(LoadSceneAsync("_Main"));
    }
    
    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        _loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingSlider.value = progress;
            
            yield return null;
        }
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    
    
        
}
