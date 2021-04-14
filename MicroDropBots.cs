using System.Linq;
using BepInEx;
using R2API.Utils;
using RoR2;
using ItemCatalog = On.RoR2.ItemCatalog;

namespace Arimah
{
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("com.arimah.MicroDropBots", "MicroDropBots", "1.0.0")]
    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync, VersionStrictness.DifferentModVersionsAreOk)]
    public class MicroDropBots : BaseUnityPlugin
    {
        public void Awake()
        {
            ItemCatalog.SetItemDefs += (orig, items) => {
                var microbots = items.First(i => i.name == "CaptainDefenseMatrix");
                if (microbots)
                {
                    microbots.tags = new[] {
                        ItemTag.Any,
                        ItemTag.AIBlacklist,
                        ItemTag.Utility,
                    };
                    Logger.LogMessage("Defensive Microbots is now findable");
                }
                else
                {
                    Logger.LogWarning("Could not locate Defensive Microbots item");
                }
                orig(items);
            };
        }
    }
}
