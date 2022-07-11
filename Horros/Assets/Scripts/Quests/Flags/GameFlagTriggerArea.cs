using UnityEngine;

public class GameFlagTriggerArea : MonoBehaviour
{
    [SerializeField] GameFlag _gameFlag;

    private void OnTriggerEnter(Collider other)
    {
        _gameFlag.Set(true);
    }
}
