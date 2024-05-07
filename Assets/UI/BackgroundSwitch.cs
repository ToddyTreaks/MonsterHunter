using UnityEngine;

public class BackgroundSwitch : MonoBehaviour
{
    [SerializeField] private CanvasGroup[] _backgrounds;
    private int _currentBackground = 0;
    public float _speedDown = 0.1f;
    public float _speedLeft = 0.1f;
    
    [SerializeField] private float CountDown = 5f;
    private float _currentTime = 0f;
    
    

    private void Start()
    {
        _currentTime = CountDown;
        _backgrounds[_currentBackground].GetComponent<Animator>().SetTrigger("Open");   
    }

    void Update()
    {
        if (_currentTime <= 0)
        {
            _currentTime = CountDown;
            SwitchBackground();
        }
        else
        {
            _currentTime -= Time.deltaTime;
        }
        TranslationBackground();
        
    }
    
    private void TranslationBackground()
    {
        _backgrounds[_currentBackground].transform.Translate(Vector3.down * _speedDown);
        _backgrounds[_currentBackground].transform.Translate(Vector3.left * _speedLeft);
    }
    
    private void SwitchBackground()
    {
        _backgrounds[_currentBackground].GetComponent<Animator>().SetTrigger("Close");
        _currentBackground++;
        if (_currentBackground >= _backgrounds.Length)
        {
            _currentBackground = 0;
        }
        
        _backgrounds[_currentBackground].GetComponent<Animator>().SetTrigger("Open");
    }
}
