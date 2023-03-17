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

namespace KFC.Cards
{
    public class Alyssa : SimpleCard
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "Alyssa",
            Description = "+14 Projectile Speed",
            ModName = KFC.ModIntDed,
            Art = null,
            Rarity = CardInfo.Rarity.Rare,
            Theme = CardThemeColor.CardThemeColorType.ColdBlue,
            Stats = new[]
            {
                new CardInfoStat
                {
                    amount = "<#FF00FF>+14",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Projectile Speed"
                }
            }
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.projectileSpeed = 14f;
        }
    }
}
