using UnityEngine;

public class DialogGiver : MonoBehaviour, IInteractable
{
    [SerializeField] private TextAsset _dialog;
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerMovementController>();
        if (player != null)
        {
            FindObjectOfType<DialogController>().StartDialog(_dialog);
            transform.LookAt(player.transform);
        }
    }


    public void Interact(GameObject player)
    {
        FindObjectOfType<DialogController>().StartDialog(_dialog);
        transform.LookAt(player.transform);
    }
}
