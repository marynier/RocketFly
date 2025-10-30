using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private Vector2 _velocity = new Vector2(0, 10f);
    [SerializeField] private float _maxRotationSpeed;    
    [SerializeField] private Rigidbody2D _rigidbody;

    void Start()
    {
        _rigidbody.AddRelativeForce(_velocity, ForceMode2D.Impulse);
        _rigidbody.angularVelocity = Random.Range(-_maxRotationSpeed, _maxRotationSpeed);
    }
    private void Update()
    {
        Vector2 velocity = _rigidbody.linearVelocity;
        if (velocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg - 90f;
            _rigidbody.rotation = angle;
        }
        Invoke(nameof(Die),5f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Invoke(nameof(Die), 0);
    }
    private void Die()
    {
        Destroy(gameObject);
    }    
}
