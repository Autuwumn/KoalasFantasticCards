using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus;
using UnboundLib;
using KFC.MonoBehaviors;
using UnboundLib.Utils;
using System.Linq;
using System.Collections.Generic;
using UnboundLib.Networking;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;

namespace KFC.Cards
{
    public class HaruShijun : SimpleCard
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "HaruShijun",
            Description = "You like blocking aye?",
            ModName = KFC.ModIntDed,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_HaruShijun"),
            Rarity = RarityUtils.GetRarity("Rare"),
            Theme = CardThemeColor.CardThemeColorType.DestructiveRed,
            Stats = new[]
            {
                new CardInfoStat
                {
                    amount = "+3",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Random Block Cards"
                }
            }
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            cardInfo.categories = new CardCategory[] { CustomCardCategories.instance.CardCategory("CardManipulation"), CustomCardCategories.instance.CardCategory("cantEternity") };
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            KFC.instance.ExecuteAfterFrames(20, () =>
            {
                var cardList = UnboundLib.Utils.CardManager.cards;
                List<CardInfo> blockCards = new List<CardInfo>();
                foreach (var b in cardList.Values)
                {
                    var c = b.cardInfo;
                    if (c.gameObject.GetComponent<Block>())
                    {
                        var h = c.gameObject.GetComponent<Block>();
                        if (h.cdAdd != 0 || h.cdMultiplier != 1 || h.additionalBlocks != 0)
                        {
                            if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, c) && CardManager.IsCardActive(c))
                            {
                                blockCards.Add(c);
                            }
                        }
                    }
                }
                List<CardInfo> cardsToAdd = new List<CardInfo> {
                    blockCards[UnityEngine.Random.Range(0, blockCards.Count - 1)],
                    blockCards[UnityEngine.Random.Range(0, blockCards.Count - 1)],
                    blockCards[UnityEngine.Random.Range(0, blockCards.Count - 1)]
                };
                ModdingUtils.Utils.Cards.instance.AddCardsToPlayer(player, cardsToAdd.ToArray(), false, null, null, null);
            });
        }
    }
}