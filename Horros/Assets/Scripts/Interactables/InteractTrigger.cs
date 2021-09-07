using System;
using TMPro;
using UnityEngine;

public class InteractTrigger : MonoBehaviour
{
    [SerializeField] TMP_Text _text;

    private IInteractable _interactable;
    private bool _canInteract;
    private GameObject _gameObject;

    private void Update()
    {
        if (_canInteract)
        {
            _text.gameObject.SetActive(true);
            if (PlayerInput.Instance.GetKeyDown(KeyCode.E))
            {
                _interactable.Interact(transform.parent.gameObject);
                _canInteract = false;
                _text.gameObject.SetActive(false);
            }
        }
        if(!_canInteract && _text.gameObject.activeInHierarchy)
            _text.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.gameObject.GetComponent<IInteractable>();
        if (interactable != null)
        {
            _interactable = interactable;
            _gameObject = other.gameObject;
            _canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _gameObject)
            _canInteract = false;
    }
}