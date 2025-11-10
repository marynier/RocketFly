using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private float _shootDelay = 5f;
    [SerializeField] private Animator _animator;
    private float _time = 0f;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _spawn;

    void Update()
    {
        _time += Time.deltaTime;
        if (_time >= _shootDelay)
        {
            _animator.SetTrigger("Shoot");
            _time = 0f;
        }
    }
    public void Create()
    {        
            Instantiate(_prefab, _spawn.position, _spawn.rotation);        
    }
}
