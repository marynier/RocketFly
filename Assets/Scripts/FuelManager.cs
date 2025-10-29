using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FuelManager : MonoBehaviour
{
    [SerializeField] private Image _fuelImage;
    [SerializeField] private TMP_Text _fuelText;
    public int _fuelReserve = 100; //������� �������
    private int _maxFuel = 100;

    private float _currentFillAmount; // ������� fillAmount
    private float _targetFillAmount;  // �������� fillAmount
    private float _lerpTime;          // ����� ������������
    private float _lerpDuration = 1f; // ������������ �������� �������� � ��������
    private float _fuelDelay = 0.2f;
    private float _timeWithoutCharging;


    private void Start()
    {
        _currentFillAmount = 1f; // ��� ������ ������� ������
        _targetFillAmount = 1f;
        _fuelImage.fillAmount = _currentFillAmount;
        _fuelText.text = _fuelReserve.ToString("00");
    }
    private void Update()
    {
        _timeWithoutCharging += Time.deltaTime;
        if (_currentFillAmount != _targetFillAmount)
        {
            _lerpTime += Time.deltaTime;
            float t = Mathf.Clamp01(_lerpTime / _lerpDuration);
            _currentFillAmount = Mathf.Lerp(_currentFillAmount, _targetFillAmount, t);
            _fuelImage.fillAmount = _currentFillAmount;
        }
    }
    public void UpdateFuel()
    {
        _targetFillAmount = _fuelReserve / 100f;
        _lerpTime = 0f; // ���������� ������ ��� ����� ������������

        _fuelText.text = _fuelReserve.ToString("00");
    }

    public void FuelBurst(int value)
    {
        _fuelReserve = Mathf.Max(0, _fuelReserve - value);
        UpdateFuel();
    }

    public void FuelCharging(int value)
    {
        if (_timeWithoutCharging >= _fuelDelay)
        {
            _fuelReserve = Mathf.Min(_maxFuel, _fuelReserve + value);
            UpdateFuel();
            _timeWithoutCharging = 0;
        }
    }
}
