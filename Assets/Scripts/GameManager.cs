using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int _levelNumber;
    [SerializeField] private GameObject _loseWindow;
    [SerializeField] private GameObject _startWindow;
    public UnityEvent StartGameEvent; 
    private void Start()
    {    
        _levelNumber = SceneManager.GetActiveScene().buildIndex;
    }
    public void Restart()
    {
        SceneManager.LoadScene(_levelNumber);
    }
    public void EndGame()
    {
        _loseWindow.SetActive(true);
    }
    public void Respawn()
    {
        _loseWindow.SetActive(false);
    }
    public void StartGame()
    {
        _startWindow.SetActive(false);
        StartGameEvent.Invoke();
    }
}
