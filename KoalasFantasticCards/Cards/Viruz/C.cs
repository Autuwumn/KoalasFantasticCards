using UnityEngine;
using ModsPlus;
using UnboundLib;
using KFC.MonoBehaviors;
using ClassesManagerReborn.Util;

namespace KFC.Cards
{
    public class Ccard : SimpleCard
    {
        internal static CardInfo card = null;
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = ViruzClass.name;
        }
        public override CardDetails Details => new CardDetails
        {
            Title = "c++;",
            Description = "Is this good, or bad?",
            ModName = KFC.ModInitials,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_CViruz"),
            Rarity = CardInfo.Rarity.Rare,
            Theme = CardThemeColor.CardThemeColorType.MagicPink,
            Stats = new[]
            {
                new CardInfoStat
                {
                    amount = "<#FFFF00>+1",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "c"
                }
            }
        };
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            player.GetComponent<FuckMe>().c++;
        }
        protected override void Removed(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            player.GetComponent<FuckMe>().c--;
        }
    }
}