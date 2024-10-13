using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus;
using UnboundLib;
using KFC.MonoBehaviors;

namespace KFC.Cards
{
    public class failure : SimpleCard
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "Rejection",
            Description = "You are not worthy",
            ModName = KFC.ModInitials,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_Failure"),
            Rarity = RarityUtils.GetRarity("Trinket"),
            Theme = CardThemeColor.CardThemeColorType.DestructiveRed
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            statModifiers.health = 0.9f;
        }
        public override bool GetEnabled()
        {
            return false;
        }
    }
}