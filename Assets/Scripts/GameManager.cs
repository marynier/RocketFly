using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int _levelNumber;
    private void Start()
    {    
        _levelNumber = SceneManager.GetActiveScene().buildIndex;
    }
    public void Restart()
    {
        SceneManager.LoadScene(_levelNumber);
    }
}
