using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    [SerializeField] private Transform _target;
    
    void Update()
    {
        transform.position = new Vector3(_target.position.x, transform.position.y, transform.position.z);
    }
}
