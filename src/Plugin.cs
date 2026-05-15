using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace NpcShopReroll
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log = null!;

        void Awake()
        {
            Log = Logger;
            new Harmony(PluginInfo.GUID).PatchAll();
            Log.LogInfo($"{PluginInfo.Name} v{PluginInfo.Version} loaded.");
        }
    }
}
