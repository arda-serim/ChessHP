using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instnace;
    public static T Instance
    {
        get
        {
            return _instnace;
        }
    }

    void Awake()
    {
        _instnace = this as T;
    }
}
