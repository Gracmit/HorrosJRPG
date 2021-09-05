public class BuffCounter
{
    private float _multiplier;
    private int _timeRemaining;

    public float Multiplier => _multiplier;
    public int RemainingTime => _timeRemaining;

    public BuffCounter(float multiplier, int length)
    {
        _multiplier = multiplier;
        _timeRemaining = length;
    }

    public bool ModifyBuff(BuffSkillData buff)
    {
        if (_multiplier == buff.Multiplier)
        {
            _timeRemaining += buff.Lenght;
            return true;
        }
        else
        {
            _multiplier *= buff.Multiplier;
            _timeRemaining = buff.Lenght;
            return true;
        }

        return false;
    }

    public void DecreaseRemainingTime() => _timeRemaining--;
}