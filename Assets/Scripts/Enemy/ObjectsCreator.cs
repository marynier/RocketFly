using UnityEngine;

public class ObjectsCreator : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform[] _spawns;
    [SerializeField] private float _creationDelay = 3f;
    private float _currentTime;


    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime > _creationDelay)
        {
            Create();
            _currentTime = 0;
        }
    }
    public void Create()
    {
        for (int i = 0; i < _spawns.Length; i++)
        {
            Instantiate(_prefab, _spawns[i].position, _spawns[i].rotation);
        }
    }
}
