using TMPro;
using UnityEngine;

public class FuelCharger : MonoBehaviour
{
    [SerializeField] private int _fuelQuantity = 10;
    [SerializeField] private TMP_Text _valueText;
    private FuelManager _fuelManager;

    
    void Start()
    {
        _valueText.text = "+" + _fuelQuantity.ToString();
        _fuelManager = FindFirstObjectByType<FuelManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMove>())
        {
            _fuelManager.FuelCharging(_fuelQuantity);
            Destroy(gameObject);
        }
    }

}
