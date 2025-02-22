﻿using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus;
using UnboundLib;
using KFC.MonoBehaviors;
using KFC.Cards;
using System.Linq;
using System.Collections.Generic;
using System;
using UnboundLib.Networking;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;

namespace KFC.Cards
{
    public class F3nxCorgi : SimpleCard
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "Bounce like F3NX_Corgi",
            Description = "<#FF8000>+9</color><#FFFFFF> Bounces",
            ModName = KFC.ModIntDed,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_F3nx"),
            Rarity = CardInfo.Rarity.Rare,
            Theme = CardThemeColor.CardThemeColorType.TechWhite,
            Stats = new[]
            {
                new CardInfoStat
                {
                    amount = "<#FF8000>Gain below",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "<#FF8000>per bounce"
                },
                new CardInfoStat
                {
                    amount = "<#FF8000>+1%",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Speed on Bounce"
                },
                new CardInfoStat
                {
                    amount = "<#FF8000>+1%",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Dmg on Bounce"
                }
            }
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            gun.reflects = 9;
            gun.objectsToSpawn = new ObjectsToSpawn[] { KFC.instance.wallbounce };
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            KFC.instance.ExecuteAfterFrames(20, () =>
            {
                var mult = (float)Math.Pow(1.01, gun.reflects);
                if (mult > 2.5f) mult = 2.5f;
                gun.dmgMOnBounce *= mult;
                gun.speedMOnBounce *= mult;
            });
        }
    }
}