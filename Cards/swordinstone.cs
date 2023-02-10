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
            KFC.instance.ExecuteAfterFrames(20, () =>
            {
                var rng = UnityEngine.Random.Range(0, 2);
                if (rng == 1)
                {
                    CardInfo[] cardsToAdd = new CardInfo[1];
                    cardsToAdd[0] = excaliber.card;
                    ModdingUtils.Utils.Cards.instance.AddCardsToPlayer(player, cardsToAdd, false, null, null, null, true);
                }
            });
        }
    }
}