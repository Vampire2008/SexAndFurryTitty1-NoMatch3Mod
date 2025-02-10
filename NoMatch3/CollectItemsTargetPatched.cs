using SweetSugar.Scripts.TargetScripts;

namespace NoMatch3;

public class CollectItemsTargetPatched(CollectItems originalTarget) : CollectItems
{
    public override int CountTargetSublevel()
    {
        return originalTarget.CountTargetSublevel();
    }

    public override void InitTarget()
    {
        originalTarget.InitTarget();
        amount = 1;
        destAmount = 1;
    }

    public override void FulfillTarget<T>(T[] _items)
    {
        amount--;
    }

    public override int GetCount(string spriteName)
    {
        return amount;
    }
}