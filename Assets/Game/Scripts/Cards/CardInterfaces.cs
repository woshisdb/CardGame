public interface IBelongToSlot
{
    SlotView GetSlot();
}
public interface IAnimalCard:IBelongToSlot
{
    int GetHp();
    void SetHp(int hp);
    void ChangeHp(int hp);
    CardEnum GetCardType();

    int ProcessAttack(int val);
    void RegisterAttackProcess(AttackProcesser attackProcesser);
    void RemoveRegisterAttackProcess(AttackProcesser attackProcesser);
}