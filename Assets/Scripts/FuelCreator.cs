using UnityEngine;

public class FuelCreator : MonoBehaviour
{
    [SerializeField] private FuelCharger _fuelPrefab;
    [SerializeField] private Transform[] _spawns;
    void Start()
    {
        int spawnIndex = Random.Range(0, _spawns.Length);
        FuelCharger newCharger = Instantiate(_fuelPrefab, _spawns[spawnIndex].position, Quaternion.identity);
        newCharger.transform.parent = this.transform;
    }

}
