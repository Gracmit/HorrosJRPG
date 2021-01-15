using System.Collections.Generic;
using UnityEngine;

public class PartyPool : MonoBehaviour
{
    [SerializeField] private List<PartyMember> _members = new List<PartyMember>();
    public double MembersCount => _members.Count;
    public List<PartyMember> Members => _members;

    public void AddMember(PartyMember member)
    {
        if (_members.Contains(member))
            return;
        
        _members.Add(member);
    }

    public void RemoveMember(PartyMember member)
    {
        _members.Remove(member);
    }
}
