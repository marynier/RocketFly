using UnityEngine;

public class FuelCreator : MonoBehaviour
{
    [SerializeField] private FuelCharge _fuelPrefab;
    [SerializeField] private Transform[] _spawns;
    void Start()
    {
        int spawnIndex = Random.Range(0, _spawns.Length);
        Instantiate(_fuelPrefab, _spawns[spawnIndex].position, Quaternion.identity);
    }

}
