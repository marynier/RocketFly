using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private PlayerMove _player;
    [SerializeField] private GameObject _loseWindow;
    [SerializeField] private TMP_Text _scoreText;
    private Vector2 _startPlayerPosition;
    private Vector2 _endPlayerPosition;
    private float _currentScore = 0;
    public float Score;

    void Start()
    {
        _startPlayerPosition = _player.transform.position;
    }
    private void Update()
    {
        UpdateScore();
    }
    public void EndGame()
    {

        UpdateScore();
        _loseWindow.SetActive(true);
    }
    private void UpdateText()
    {
        _scoreText.text = Score.ToString("00.0");
    }
    private void UpdateScore()
    {
        if (!_player) return;
        _endPlayerPosition = _player.transform.position;
        _currentScore = _endPlayerPosition.x - _startPlayerPosition.x;
        if (Score < _currentScore)
            Score = _currentScore;
        UpdateText();
    }
}
