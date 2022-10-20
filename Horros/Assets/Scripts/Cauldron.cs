using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Cauldron : MonoBehaviour
{
    [SerializeField] private List<TimelineAsset> _timelines;
    
    float _bloodAmount;
    PlayableDirector _director;
    bool _firstAnimation = true;
    bool _done;

    void Awake()
    {
        _director = GetComponent<PlayableDirector>();
    }

    // ToDo: Delete this
    void Update()
    {
        if (InputHandler.Instance.Controls.Player.Test.WasPressedThisFrame())
        {
            _director.playableAsset = _timelines[0];
            _director.Play();
        }
    }

    public void PlayAnimation()
    {
        if (_firstAnimation && !_done)
        {
            _director.playableAsset = _timelines[0];
            _director.Play();
            _firstAnimation = false;
        }

        if (!_firstAnimation && _bloodAmount >= 5 && !_done)
        {
            _director.playableAsset = _timelines[1];
            _director.Play();
        }
    }

    public void AddBlood(int amount)
    {
        _bloodAmount += amount;
    }
}
