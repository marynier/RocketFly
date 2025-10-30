using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int _levelNumber;
    [SerializeField] private GameObject _loseWindow;
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
}
