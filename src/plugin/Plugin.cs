using BepInEx;
using UnityEngine;

namespace ConsistentCycles
{
    [BepInPlugin("com.coder23848.consistentcycles", PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
#pragma warning disable IDE0051 // Visual Studio is whiny
        private void OnEnable()
#pragma warning restore IDE0051
        {
            // Plugin startup logic
            On.RainWorld.OnModsInit += RainWorld_OnModsInit;

            On.World.ctor += World_ctor;
        }

        private void World_ctor(On.World.orig_ctor orig, World self, RainWorldGame game, Region region, string name, bool singleRoomWorld)
        {
            if (game.IsStorySession)
            {
                Random.State state = Random.state;
                game.GetStorySession.SetRandomSeedToCycleSeed(10000);

                orig(self, game, region, name, singleRoomWorld);

                Random.state = state;
            }
            else
            {
                orig(self, game, region, name, singleRoomWorld);
            }
        }

        private void RainWorld_OnModsInit(On.RainWorld.orig_OnModsInit orig, RainWorld self)
        {
            orig(self);
            Debug.Log("Consistent Cycles config setup: " + MachineConnector.SetRegisteredOI(PluginInfo.PLUGIN_GUID, PluginOptions.Instance));
        }
    }
}