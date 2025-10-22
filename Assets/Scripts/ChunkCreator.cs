using System.Collections.Generic;
using UnityEngine;

public class ChunkCreator : MonoBehaviour
{
    private const float _playerDistToSpawn = 50f; // ������ ����������
    private const int _prewarmChunks = 6;         // ��������� �����
    private const int _maxActiveChunks = 12;      // ����� ��������

    [SerializeField] private Transform _player;            // �����/�������� ���
    [SerializeField] private Transform _startChunk;        // ������ ���� � �����
    [SerializeField] private List<Transform> _chunkPool;   // �������� ������ (prefab)

    private readonly Queue<Transform> _active = new();
    private Vector3 _lastEndPos;

    void Awake()
    {
        _lastEndPos = _startChunk.Find("EndPoint").position;
        _active.Enqueue(_startChunk);

        for (int i = 0; i < _prewarmChunks; i++)
            SpawnNext();
    }

    void Update()
    {
        // ��������� �����
        if (Vector3.Distance(_player.position, _lastEndPos) < _playerDistToSpawn)
            SpawnNext();

        // �������/������� � ��� ������ ������
        while (_active.Count > _maxActiveChunks)
            DespawnOldest();
    }

    void SpawnNext()
    {
        var prefab = _chunkPool[Random.Range(0, _chunkPool.Count)];
        var next = Instantiate(prefab, _lastEndPos, Quaternion.identity);
        _active.Enqueue(next);
        _lastEndPos = next.Find("EndPoint").position;
    }

    void DespawnOldest()
    {
        var oldest = _active.Dequeue();
        if (oldest == null) return;
        Destroy(oldest.gameObject); // �������� �� ReturnToPool ��� pooling
    }
}
