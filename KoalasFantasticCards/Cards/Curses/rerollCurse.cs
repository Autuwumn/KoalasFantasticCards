using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus;
using UnboundLib;
using KFC.MonoBehaviors;
using KFC.Cards;
using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnboundLib.GameModes;
using WillsWackyManagers.Utils;
using UnityEngine.Experimental.PlayerLoop;
using UnboundLib.Utils;

namespace KFC.Cards
{
    public class rerollNew : CustomEffectCard<rerollCurse>
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "Instant Rerolling",
            Description = "Rerolls all future picks",
            ModName = KFC.CurseInt,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_Shadow"),
            Rarity = RarityUtils.GetRarity("Legendary"),
            Theme = CardThemeColor.CardThemeColorType.EvilPurple,
            OwnerOnly = true
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { CurseManager.instance.curseCategory };
        }
    }
}

namespace KFC.MonoBehaviors
{
    public class rerollCurse : CardEffect
    {
        CardInfo lastCard = rerollNew.card;
        protected override void Start()
        {
            lastCard = rerollNew.card;
        }
        void Update()
        {
            CardInfo curCard = data.currentCards[data.currentCards.Count - 1];
            if (curCard != lastCard)
            {
                var cardList = CardManager.cards;
                CardInfo.Rarity newRarity = curCard.rarity;
                List<CardInfo> possibleCards = new List<CardInfo>();
                foreach(var b in cardList.Values)
                {
                    var c = b.cardInfo;
                    if (c.rarity == newRarity && ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, c) && CardManager.IsCardActive(c) && c != curCard)
                    {
                        possibleCards.Add(c);
                    }
                }
                var newCard = possibleCards.ElementAt(Random.Range(0, possibleCards.Count));
                lastCard = curCard;
                KFC.instance.ExecuteAfterFrames(10, () =>
                {
                    lastCard = newCard;
                    ModdingUtils.Utils.Cards.instance.RemoveCardFromPlayer(player, data.currentCards.Count - 1);
                    ModdingUtils.Utils.Cards.instance.AddCardsToPlayer(player, new CardInfo[] { newCard }, false, null, null, null);
                });
            }
        }
    }
}