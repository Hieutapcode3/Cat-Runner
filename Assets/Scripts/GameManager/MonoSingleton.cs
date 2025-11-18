using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    public static T Instance;

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = FindAnyObjectByType<T>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
