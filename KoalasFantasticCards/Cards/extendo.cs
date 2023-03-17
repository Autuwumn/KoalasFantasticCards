using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus;
using UnboundLib;
using KFC.MonoBehaviors;

namespace KFC.Cards
{
    public class Extendo : CustomEffectCard<BulletPusher>
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "Extended Gun",
            Description = "Bullets are launched from farther away from you",
            ModName = KFC.ModInitials,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_Extendo"),
            Rarity = RarityUtils.GetRarity("Rare"),
            Theme = CardThemeColor.CardThemeColorType.DestructiveRed
        };
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            KFC.instance.ExecuteAfterFrames(20, () =>
            {
                player.gameObject.GetComponentInChildren<BulletPusher>().dist += 5;
            });
        }
        protected override void Removed(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            if(player.gameObject.GetComponentInChildren<BulletPusher>() != null) player.gameObject.GetComponentInChildren<BulletPusher>().dist -= 5;
        }
    }
}
namespace KFC.MonoBehaviors
{
    public class BulletPusher : CardEffect
    {
        public float dist = 0;
        public override void OnShoot(GameObject projectile)
        {
            var offset = player.data.aimDirection.normalized * dist;
            projectile.transform.position += offset;
            base.OnShoot(projectile);
        }
    }
}