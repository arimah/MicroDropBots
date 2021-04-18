using System.Linq;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using R2API.Utils;
using RoR2;

#pragma warning disable IDE0051 // Remove unused private members

namespace Arimah
{
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("com.arimah.MicroDropBots", "MicroDropBots", "1.0.2")]
    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync, VersionStrictness.DifferentModVersionsAreOk)]
    public class MicroDropBots : BaseUnityPlugin
    {
        public void Awake()
        {
            new Harmony("com.arimah.MicroDropBots").PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(ItemCatalog))]
    internal class ItemCatalogPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("SetItemDefs")]
        private static void SetItemDefs(ItemDef[] newItemDefs)
        {
            using var logger = Logger.CreateLogSource("MicroDropBots");
            var microbots = newItemDefs.First(i => i.name == "CaptainDefenseMatrix");
            if (microbots)
            {
                microbots.tags = new[] {
                    ItemTag.Any,
                    ItemTag.AIBlacklist,
                    ItemTag.Utility,
                };
                logger.LogMessage("Defensive Microbots is now findable");
            }
            else
            {
                logger.LogWarning("Could not locate Defensive Microbots item");
            }
        }
    }
}
