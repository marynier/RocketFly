using TMPro;
using UnityEngine;

public class FuelCharge : MonoBehaviour
{
    [SerializeField] private float _fuelQuantity = 10f;
    [SerializeField] private TMP_Text _valueText;
    private void Start()
    {
        _valueText.text = "+" + _fuelQuantity.ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMove>())
        {
            collision.GetComponent<PlayerMove>().FuelCharging(_fuelQuantity);
            Destroy(gameObject);
        }
    }
}
