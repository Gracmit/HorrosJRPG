using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Cauldron : MonoBehaviour
{
    [SerializeField] private List<TimelineAsset> _timelines;
    [SerializeField] private CauldronData _data;
    [SerializeField] private int _requiredBloodAmount = 5;
    
    PlayableDirector _director;

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
        if (_data.FirstAnimation && !_data.Done)
        {
            _director.playableAsset = _timelines[0];
            _director.Play();
            _data.FirstAnimation = false;
        }

        if (!_data.FirstAnimation && _data.BloodAmount >= _requiredBloodAmount && !_data.Done)
        {
            _director.playableAsset = _timelines[1];
            _director.Play();
        }
    }

    public void AddBlood(int amount)
    {
        _data.BloodAmount += amount;
    }
}