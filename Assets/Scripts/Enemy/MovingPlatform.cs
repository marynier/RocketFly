using UnityEngine;
using UnityEngine.Events;
public enum Direction
{
    Left,
    Right
}

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform _leftTarget;
    [SerializeField] private Transform _rightTarget;

    [SerializeField] private float _speed;
    [SerializeField] private float _stopTime;

    private Direction _currentDirection;
    private bool _isStopped;    

    [SerializeField] private Vector2 _leftEuler;
    [SerializeField] private Vector2 _rightEuler;
    [SerializeField] private float _rotationSpeed;

    private Vector2 _targetEuler;

    private void Start()
    {
        _leftTarget.parent = null;
        _rightTarget.parent = null;
        RotateLeft();
    }
    void Update()
    {
        if (_isStopped == true)
        {
            return;
        }

        if (_currentDirection == Direction.Left)
        {
            transform.position -= new Vector3(Time.deltaTime * _speed, 0, 0f);
            if (transform.position.x < _leftTarget.position.x)
            {
                _currentDirection = Direction.Right;
                _isStopped = true;
                Invoke("ContinueWalk", _stopTime);
                RotateRight();
            }
        }
        else
        {
            transform.position += new Vector3(Time.deltaTime * _speed, 0, 0f);
            if (transform.position.x > _rightTarget.position.x)
            {
                _currentDirection = Direction.Left;
                _isStopped = true;
                Invoke("ContinueWalk", _stopTime);
                RotateLeft();
            }
        }      

        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(_targetEuler), Time.deltaTime * _rotationSpeed);
    }
    void ContinueWalk()
    {
        _isStopped = false;
    }
    public void RotateLeft()
    {
        _targetEuler = _leftEuler;
    }
    public void RotateRight()
    {
        _targetEuler = _rightEuler;
    }
}
