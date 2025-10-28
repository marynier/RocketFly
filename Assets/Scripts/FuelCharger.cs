using TMPro;
using UnityEngine;

public class FuelCharger : MonoBehaviour
{
    [SerializeField] private float _fuelQuantity = 10f;
    [SerializeField] private TMP_Text _valueText;
    private FuelManager _fuelManager;

    
    void Start()
    {
        _valueText.text = "+" + _fuelQuantity.ToString();
        _fuelManager = FindFirstObjectByType<FuelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMove>())
        {
            _fuelManager.FuelCharging(_fuelQuantity);
            Destroy(gameObject);
        }
    }

}
