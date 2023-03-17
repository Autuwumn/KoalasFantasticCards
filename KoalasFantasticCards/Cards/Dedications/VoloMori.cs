using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus;
using UnboundLib;
using KFC.MonoBehaviors;
using KFC.Cards;
using System.Linq;
using System.Collections.Generic;
using System;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;

namespace KFC.Cards
{
    public class VoloMori : SimpleCard
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "Reinette",
            Description = "",
            ModName = KFC.ModIntDed,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_Reinette"),
            Rarity = CardInfo.Rarity.Rare,
            Theme = CardThemeColor.CardThemeColorType.MagicPink,
            Stats = new[]
            {
                new CardInfoStat
                {
                    amount = "<#FF5733>-60%",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Size"
                },
                new CardInfoStat
                {
                    amount = "<#FF5733>-60%",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Block Cooldown"
                }
            }
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { CustomCardCategories.instance.CardCategory("CardManipulation") };
            cardInfo.allowMultiple = false;
            block.cdMultiplier = 0.4f;
            statModifiers.sizeMultiplier = 0.4f;
        }
    }
}
