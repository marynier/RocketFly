using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    public Transform respawnPoint; // точка респавна в сегменте

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMove>())
        {
            PlayerMove player = other.GetComponent<PlayerMove>();
            if (player != null)
            {
                player.CurrentChunk = this;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if ( other.GetComponent<PlayerMove>() )
        {
            PlayerMove player = other.GetComponent<PlayerMove>();

            if (player != null && player.CurrentChunk == this)
            {
                player.CurrentChunk = null;
            }
        }
    }    
}
