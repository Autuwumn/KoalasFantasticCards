using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus;
using UnboundLib;
using KFC.MonoBehaviors;
using KFC.Cards;

namespace KFC.Cards
{
    public class blackholegun : CustomEffectCard<blackholegun_Mono>
    {
        internal static CardInfo card = null;
    public override CardDetails Details => new CardDetails
    {
        Title = "Blackhole Gun",
        Description = "Bro stole it from stickfight\n:skull:",
        ModName = KFC.ModInitials,
        Art = KFC.ArtAssets.LoadAsset<GameObject>("C_Blackhole"),
        Rarity = RarityUtils.GetRarity("Mythical"),
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
    public class blackholegun_Mono : CardEffect
    {

    }
}