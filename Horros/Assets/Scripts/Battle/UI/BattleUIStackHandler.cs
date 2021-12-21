using System.Collections.Generic;
using UnityEngine;

public class BattleUIStackHandler
{
    private Stack<GameObject> _uiStack = new Stack<GameObject>();

    public GameObject GetLastUIObject()
    {
        if(_uiStack.Count <= 0)
        {
            return null;
        }

        return _uiStack.Pop();
    }

    public void ClearStack() => _uiStack.Clear();

    public void PushToStack(GameObject objectToAdd)
    {
        _uiStack.Push(objectToAdd);
    }
}