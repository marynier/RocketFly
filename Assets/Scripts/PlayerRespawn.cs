using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public ChunkTrigger CurrentChunk { get; set; }

    public void Die()
    {
        Respawn();
    }

    private void Respawn()
    {
        if (CurrentChunk != null && CurrentChunk.respawnPoint != null)
        {
            transform.position = CurrentChunk.respawnPoint.position;
            // ����� �������� ����� ��������� ������ � ������
        }
        else
        {
            // ������� � ��������� ����� ��� ������ ������
        }
    }
}
