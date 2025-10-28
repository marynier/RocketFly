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
    [SerializeField] private int _firstSegmentCount = 10;
    [SerializeField] private int _secondSegmentCount = 20;
    private readonly Queue<Transform> _active = new();
    private Vector3 _lastEndPos;
    private int _createdChunkCount = 1;

    void Awake()
    {
        _lastEndPos = _startChunk.Find("EndPoint").position;
        _active.Enqueue(_startChunk);

        for (int i = 0; i < _prewarmChunks; i++)
            SpawnNext();
    }

    void Update()
    {
        if (_player)
        {
            // ��������� �����
            if (Vector3.Distance(_player.position, _lastEndPos) < _playerDistToSpawn)
                SpawnNext();
        }

        // �������/������� � ��� ������ ������
        while (_active.Count > _maxActiveChunks)
            DespawnOldest();
    }

    void SpawnNext()
    {
        int chunkIndex;
        if (_createdChunkCount < _firstSegmentCount)
        {
            chunkIndex = Random.Range(0, 0);
        }
        else if (_createdChunkCount > _firstSegmentCount && _createdChunkCount < _secondSegmentCount)
        {
            chunkIndex = Random.Range(0, 1);
        }
        else
        {
            chunkIndex = Random.Range(0, _chunkPool.Count);
        }
        //var prefab = _chunkPool[Random.Range(0, _chunkPool.Count)];
        var prefab = _chunkPool[chunkIndex];
        var next = Instantiate(prefab, _lastEndPos, Quaternion.identity);
        _active.Enqueue(next);
        _lastEndPos = next.Find("EndPoint").position;
        _createdChunkCount++;
    }

    void DespawnOldest()
    {
        var oldest = _active.Dequeue();
        if (oldest == null) return;
        Destroy(oldest.gameObject); // �������� �� ReturnToPool ��� pooling
    }
}
