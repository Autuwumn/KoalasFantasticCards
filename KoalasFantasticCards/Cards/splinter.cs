using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus; 
using UnboundLib;
using KFC.MonoBehaviors;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace KFC.Cards
{
    class splinter : CustomEffectCard<splinter_Mono>
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title       = "Splinter",
            Description = "Shoot unstoppable iron rods with massive amounts of strength",
            ModName     = KFC.ModInitials,
            Art         = KFC.ArtAssets.LoadAsset<GameObject>("C_Splinter"),
            Rarity      = RarityUtils.GetRarity("Divine"),
            Theme       = CardThemeColor.CardThemeColorType.TechWhite,
            Stats = new []
            {
                new CardInfoStat
                {
                    amount = "+1000%",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Damage"
                },
                new CardInfoStat
                {
                    amount = "+1000%",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Bullet Speed"
                },
                new CardInfoStat
                {
                    amount = "5 sec min",
                    positive = false,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Reload Time"
                },
                new CardInfoStat
                {
                    amount = "1 Max",
                    positive = false,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Ammo"
                }
            }
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            gun.damage = 10f;
            gun.projectileSpeed = 10f;
            gun.unblockable = true;
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            gunAmmo.reloadTime = 5;
            gunAmmo.maxAmmo = 1;
        }
    }
}
namespace KFC.MonoBehaviors
{
    public class splinter_Mono : CardEffect
    {
        private void Update()
        {
            if(gunAmmo.reloadTime < 5) gunAmmo.reloadTime = 5;
            if (gunAmmo.maxAmmo > 1) gunAmmo.maxAmmo = 1;
        }
    }
}