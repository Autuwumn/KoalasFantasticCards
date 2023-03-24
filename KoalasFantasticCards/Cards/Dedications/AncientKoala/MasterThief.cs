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
    public class MasterThief : SimpleCard
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "Master Thief",
            Description = "You are too good at strealing",
            ModName = KFC.ModIntDed,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_MasterThief"),
            Rarity = CardInfo.Rarity.Rare,
            Theme = CardThemeLib.CardThemeLib.instance.CreateOrGetType("Koality"),
            Stats = new[]
            {
                new CardInfoStat
                {
                    amount = "<#ff00ff>+60%",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Damage"
                },
                new CardInfoStat
                {
                    amount = "<#ff00ff>+60%",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Health"
                },
                new CardInfoStat
                {
                    amount = "<#ff00ff>+20%",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Movement Speed"
                },
                new CardInfoStat
                {
                    amount = "<#ff00ff>+20%",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Jump Height"
                }
            }
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.damage = 1.6f;
            statModifiers.health = 1.6f;
            statModifiers.movementSpeed = 1.2f;
            statModifiers.jump = 1.2f;
        }
        public override bool GetEnabled()
        {
            return false;
        }
    }
}
