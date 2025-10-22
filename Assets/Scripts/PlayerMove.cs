using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    void Update()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
    }
}
