using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private PlayerMove _player;
    [SerializeField] private GameObject _loseWindow;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _scoreTextLoseWindow;
    [SerializeField] private TMP_Text _recordScoreText;
    private Vector2 _startPlayerPosition;
    private Vector2 _endPlayerPosition;
    private float _currentScore = 0;
    private float _score;
    private Saves _saves;

    void Start()
    {
        _startPlayerPosition = _player.transform.position;
        _saves = Saves.Instance;
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
        _scoreText.text = _score.ToString("00.0");
        _scoreTextLoseWindow.text = _score.ToString("00.0");
        _recordScoreText.text = _saves.MaxDistance.ToString("00.0");
    }
    private void UpdateScore()
    {
        if (!_player) return;
        _endPlayerPosition = _player.transform.position;
        _currentScore = _endPlayerPosition.x - _startPlayerPosition.x;
        if (_score < _currentScore)
        {
            _score = _currentScore;
            if(_saves.MaxDistance < _score)
            {
                _saves.MaxDistance = _score;
                _saves.Save();
            }
        }

        UpdateText();
    }
}
