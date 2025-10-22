using UnityEngine;

public class FuelCharge : MonoBehaviour
{
    [SerializeField] private float _fuelQuantity = 10f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMove>())
        {
            collision.GetComponent<PlayerMove>().FuelCharging(_fuelQuantity);
            Destroy(gameObject);
        }
    }
}
