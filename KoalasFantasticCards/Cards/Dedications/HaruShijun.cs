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
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            if (!player.data.view.IsMine) return;
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
                            blockCards.Add(c);
                        }
                    }
                }
                List<CardInfo> cardsToAdd = new List<CardInfo> {
                    blockCards[UnityEngine.Random.Range(0, blockCards.Count - 1)],
                    blockCards[UnityEngine.Random.Range(0, blockCards.Count - 1)],
                    blockCards[UnityEngine.Random.Range(0, blockCards.Count - 1)]
                };
                int card1 = blockCards.IndexOf(cardsToAdd.ElementAt(0));
                int card2 = blockCards.IndexOf(cardsToAdd.ElementAt(1));
                int card3 = blockCards.IndexOf(cardsToAdd.ElementAt(2));
                NetworkingManager.RPC(typeof(HaruShijun), nameof(RPCCardGive), player.playerID, card1, card2, card3);
            });
        }
        [UnboundRPC]
        public static void RPCCardGive(int pid, int c1, int c2, int c3)
        {
            var player = PlayerManager.instance.GetPlayerWithID(pid);
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
                        blockCards.Add(c);
                    }
                }
            }
            var cardsToAdd = new List<CardInfo>
            {
                blockCards.ElementAt(c1),
                blockCards.ElementAt(c2),
                blockCards.ElementAt(c3)
            };
            ModdingUtils.Utils.Cards.instance.AddCardsToPlayer(player, cardsToAdd.ToArray(), false, null, null, null);
            ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, cardsToAdd.ToArray());
        }
    }
}