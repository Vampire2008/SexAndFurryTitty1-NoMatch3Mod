#if SAFT2
using SweetSugar.Scripts.Level;
#endif
using SweetSugar.Scripts.TargetScripts;

namespace NoMatch3;

public class CollectItemsTargetPatched(CollectItems originalTarget) : CollectItems
{
    public override int CountTargetSublevel()
    {
        return originalTarget.CountTargetSublevel();
    }

#if SAFT2
    
    public override void InitTarget(LevelData levelData)
    {
        originalTarget.InitTarget(levelData);
#else
    public override void InitTarget()
    {
        originalTarget.InitTarget();
#endif
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