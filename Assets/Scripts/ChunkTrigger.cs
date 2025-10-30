using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    public Transform respawnPoint; // точка респавна в сегменте

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerRespawn>())
        {
            PlayerRespawn playerRespawn = other.GetComponent<PlayerRespawn>();
            if (playerRespawn != null)
            {
                playerRespawn.CurrentChunk = this;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ( other.GetComponent<PlayerRespawn>() )
        {
            PlayerRespawn playerRespawn = other.GetComponent<PlayerRespawn>();

            if (playerRespawn != null && playerRespawn.CurrentChunk == this)
            {
                playerRespawn.CurrentChunk = null;
            }
        }
    }
}
