using UnityEngine;

public class Projection : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _projectionTransform;
    
    //[SerializeField] private Rigidbody2D _rigidbody;
    //public float RotationSpeed = 5f;
    //private bool _isStopped = false;

    //public void ResetAndLaunch(Vector2 startPos, Vector2 launchVelocity)
    //{
    //    _isStopped = false;

    //    // Безопасно меняем состояние через отключение симуляции
    //    _rigidbody.simulated = false;
    //    transform.position = startPos;
    //    transform.rotation = Quaternion.identity;

    //    _rigidbody.bodyType = RigidbodyType2D.Dynamic;
    //    _rigidbody.linearVelocity = launchVelocity;
    //    _rigidbody.angularVelocity = 0f;
    //    _rigidbody.simulated = true;

    //    if (launchVelocity != Vector2.zero)
    //        RotateTowardsVelocity();
    //}
    //void RotateTowardsVelocity(bool instant = false)
    //{
    //    if (_rigidbody.linearVelocity != Vector2.zero)
    //    {
    //        // Вычисляем угол на основе вектора скорости
    //        float angle = Mathf.Atan2(_rigidbody.linearVelocity.y, _rigidbody.linearVelocity.x) * Mathf.Rad2Deg;
    //        Quaternion target = Quaternion.AngleAxis(angle, Vector3.forward);
    //        transform.rotation = instant ? target :
    //            Quaternion.Slerp(transform.rotation, target, RotationSpeed * Time.deltaTime);
    //    }
    //}
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("Столкнулся");
    //    _isStopped = true;
    //    _rigidbody.linearVelocity = Vector2.zero;
    //    _rigidbody.angularVelocity = 0f;
    //    _rigidbody.bodyType = RigidbodyType2D.Static;
    //}
}
