using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private PlayerMove _player;
    private Vector2 _startPlayerPosition;
    private Vector2 _endPlayerPosition;
    public float Score;
    [SerializeField] private GameObject _loseWindow;
    [SerializeField] private TMP_Text _scoreText;

    void Start()
    {
        _startPlayerPosition = _player.transform.position;
    }
    private void Update()
    {
        UpdateText();
    }
    public void EndGame()
    {
        _endPlayerPosition = _player.transform.position;
        Score = _endPlayerPosition.x - _startPlayerPosition.x;
        UpdateText();
        _loseWindow.SetActive(true);
    }
    private void UpdateText()
    {
        _scoreText.text = Score.ToString("00.0");
    }
}
