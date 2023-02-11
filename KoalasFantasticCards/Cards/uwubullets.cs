using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus;
using UnboundLib;
using KFC.MonoBehaviors;
using KFC.Cards;

namespace KFC.Cards
{
    public class uwullets : SimpleCard
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "UwU",
            Description = "UwU, :3, rawr~",
            ModName = KFC.ModInitials,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_UwU"),
            Rarity = RarityUtils.GetRarity("Epic"),
            Theme = CardThemeColor.CardThemeColorType.TechWhite
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            var betterSprite = KFC.ArtAssets.LoadAsset<GameObject>("Uwullet");
            var sprite = betterSprite.transform.Find("Sprite").gameObject;
            gun.objectsToSpawn = new[]
            {
                new ObjectsToSpawn()
                {
                    AddToProjectile = sprite
                },
            };
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {

        }
    }
}