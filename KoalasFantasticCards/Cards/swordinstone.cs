using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus;
using UnboundLib;
using KFC.MonoBehaviors;

namespace KFC.Cards
{
    public class swordinstone : SimpleCard
    {
        internal static CardInfo card = null;
        private bool usedUp = false;
        public override CardDetails Details => new CardDetails
        {
            Title = "Sword in the stone",
            Description = "Atempt to pull the sword out",
            ModName = KFC.ModInitials,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_SwordInStone"),
            Rarity = RarityUtils.GetRarity("Rare"),
            Theme = CardThemeColor.CardThemeColorType.DestructiveRed
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            if (!player.data.view.IsMine) return;
            if (usedUp) return;
            KFC.instance.ExecuteAfterFrames(20, () =>
            {
                usedUp = true;
                var rng = UnityEngine.Random.Range(0, 2);
                if (rng == 1)
                {
                    ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, excaliber.card, false, "Ex", 0, 0);
                } else
                {
                    ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, failure.card, false, "Xx", 0, 0);
                }
            });
        }
    }
}