using UnityEngine;

public class UserSettingsManager : MonoBehaviour
{
    public static UserSettingsManager Instance;

    [Header("Opciones del usuario")]
    public bool useSnapTurn;
    public bool useContinuousTurn;
    public bool useContinuousMove;
    public bool useDistanceGrab;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // ¡Esto hace que no se destruya al cambiar de escena!
    }
}
