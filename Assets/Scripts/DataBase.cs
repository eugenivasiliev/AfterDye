using UnityEngine;

public class DataBase : MonoBehaviour
{
    private static DataBase instance;
    public static DataBase Instance => instance;

    [SerializeField] private ColoursDB colours;
    public ColoursDB Colours => colours;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }
}
