using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus;
using UnboundLib;
using KFC.MonoBehaviors;
using KFC.Cards;

namespace KFC.Cards
{
    public class scp_500 : CustomEffectCard<scp_500_Mono>
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "SCP-500",
            Description = "A true clensing",
            ModName = KFC.ModInitials,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_SCP_500"),
            Rarity = RarityUtils.GetRarity("Divine"),
            Theme = CardThemeColor.CardThemeColorType.TechWhite
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            statModifiers.health = 1.1f;
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {

        }
    }
}

namespace KFC.MonoBehaviors
{
    public class scp_500_Mono : CardEffect
    {

    }
}