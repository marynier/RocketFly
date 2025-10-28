using UnityEngine;

public class Saves : MonoBehaviour
{
    
    public float MaxDistance = 0;
    
    public static Saves Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Load();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Save()
    {
        PlayerPrefs.SetFloat("MaxDistance", MaxDistance);
        PlayerPrefs.Save();
    }
    public void Load()
    {
        MaxDistance = PlayerPrefs.GetFloat("MaxDistance", 0);
    }
}
