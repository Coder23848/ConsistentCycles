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
            On.World.ctor += World_ctor;
        }

        private void World_ctor(On.World.orig_ctor orig, World self, RainWorldGame game, Region region, string name, bool singleRoomWorld)
        {
            if (game != null && game.IsStorySession)
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
    }
}