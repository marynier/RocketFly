using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FuelManager : MonoBehaviour
{
    [SerializeField] private Image _fuelImage;
    [SerializeField] private TMP_Text _fuelText;
    public float _fuelReserve = 100f; //Остаток топлива
    private float _maxFuel = 100f;

    private void Start()
    {

        UpdateFuel();
    }

    public void UpdateFuel()
    {
        _fuelImage.fillAmount = _fuelReserve / 100f;
        _fuelText.text = _fuelReserve.ToString("00");
    }
    public void FuelCharging(float value)
    {
        if (_fuelReserve < _maxFuel)
        {
            float possibleAdding = _maxFuel - _fuelReserve;
            if (possibleAdding >= value)
                _fuelReserve += value;
            else
                _fuelReserve += possibleAdding;

            if (_fuelReserve > _maxFuel)
                _fuelReserve = _maxFuel;

            UpdateFuel();
        }
    }
}
