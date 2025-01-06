using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using SweetSugar.Scripts.Core;
using SweetSugar.Scripts.TargetScripts;
using SweetSugar.Scripts.TargetScripts.TargetSystem;

namespace NoMatch3
{
    [BepInPlugin(modGuid, "NoMatch3", "1.1.0")]
    public class NoMatch3Patcher : BaseUnityPlugin
    {
        private const string modGuid = "cf53df4f-3cd5-42a2-91bb-640606a25637";

        private readonly Harmony _harmony = new(modGuid);
        private static NoMatch3Patcher? Instance;
        private static ManualLogSource _logger;

        private void Awake()
        {
            Instance = this;
            _logger = BepInEx.Logging.Logger.CreateLogSource("NoMatch3");
            _harmony.PatchAll(typeof(NoMatch3Patcher));
            _logger.LogInfo("Patched");
        }

        [HarmonyPatch(typeof(NovelTreeScreenController), nameof(NovelTreeScreenController.refreshStoryModeToggleAndSyncVariable))]
        [HarmonyPostfix]
        private static void MakeStoryModeAlwaysVisibleAndSetByDefault(NovelTreeScreenController __instance)
        {
            __instance.checkBoxToggle.gameObject.SetActive(true);
            __instance.checkBoxToggle.isOn = true;
            __instance.onToggleChange();
            _logger.LogInfo("Checkbox Enabled");
        }

        [HarmonyPatch(typeof(LevelManager), nameof(LevelManager.LoadLevel), [])]
        [HarmonyPostfix]
        private static void MakeMatch3Simple(LevelManager __instance)
        {
            __instance.levelData.limit = int.MaxValue;
            _logger.LogInfo($"Fields count: {__instance.fieldBoards.Count}");

            __instance.levelData.star1 = 1;
            __instance.levelData.star2 = 1;
            __instance.levelData.star3 = 1;

            _logger.LogDebug($"Target type is {__instance.levelData.targetObject.GetType().Name}");

            __instance.levelData.targetObject = PatchTarget(__instance.levelData.targetObject);
            if (__instance.levelData.targetObjectExtra != null)
            {
                _logger.LogDebug($"Target extra type is {__instance.levelData.targetObjectExtra.GetType().Name}");
                __instance.levelData.targetObjectExtra = PatchTarget(__instance.levelData.targetObjectExtra);
            }
        }

        private static Target PatchTarget(Target target)
        {
            switch (target)
            {
                case CollectItems collectItems:
                    return new CollectItemsTargetPatched(collectItems);
                default:
                    target.destAmount = 1;
                    break;
            }

            return target;
        }
    }
}
