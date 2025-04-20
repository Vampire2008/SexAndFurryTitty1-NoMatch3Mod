using BepInEx.Configuration;

namespace NoMatch3;

internal class Config
{
    private ConfigEntry<bool> _storyModeEnabled;

    internal bool StoryModeEnabled => _storyModeEnabled.Value;

    internal Config(ConfigFile cfg)
    {
        _storyModeEnabled = cfg.Bind(
            new ConfigDefinition("General", "StoryMode"),
            true,
            new ConfigDescription("Should enable story mode")
        );
    }
}