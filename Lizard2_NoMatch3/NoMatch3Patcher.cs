using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;

namespace NoMatch3;

[BepInPlugin(modGuid, "NoMatch3", "1.1.0")]
public class NoMatch3Patcher : BasePlugin
{
    private const string modGuid = "cf53df4f-3cd5-42a2-91bb-640606a25637";

    private readonly Harmony _harmony = new(modGuid);
    private static NoMatch3Patcher? Instance;
    private static ManualLogSource _logger;
    private static Config _config;

    public override void Load()
    {
        Instance = this;
        _logger = BepInEx.Logging.Logger.CreateLogSource("NoMatch3");
        _harmony.PatchAll(typeof(NoMatch3Patcher));
        _logger.LogInfo("Patched");
        _config = new Config(Config);
    }

    [HarmonyPatch(typeof(NovelTreeScreenController), nameof(NovelTreeScreenController.refreshStoryModeToggleAndSyncVariable))]
    [HarmonyPostfix]
    private static void MakeStoryModeAlwaysVisibleAndSetByDefault(NovelTreeScreenController __instance)
    {
        __instance.checkBoxToggle.Set(true);
        __instance.checkBoxToggle.isOn = _config.StoryModeEnabled;
        __instance.onToggleChange();
        _logger.LogInfo("Checkbox Enabled");
        _logger.LogInfo($"StoryMode is set to {_config.StoryModeEnabled}");
    }
}