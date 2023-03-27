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
using System.Collections;
using UnboundLib.GameModes;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using ModdingUtils.Extensions;

namespace KFC.Cards
{
    public class KoalaMight : SimpleCard
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "Might of Koala",
            Description = "You gain great strength",
            ModName = KFC.ModIntDed,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_KoalaMight"),
            Rarity = RarityUtils.GetRarity("Legendary"),
            Theme = CardThemeLib.CardThemeLib.instance.CreateOrGetType("Koality"),
            Stats = new CardInfoStat[]
            {
                new CardInfoStat
                {
                    amount = "<#ff00ff>+"+KFC.mysteryValue[2],
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Damage"
                },
                new CardInfoStat
                {
                    amount = "<#ff00ff>+"+KFC.mysteryValue[2],
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Attack Speed"
                },
                new CardInfoStat
                {
                    amount = "<#ff00ff>+"+KFC.mysteryValue[2],
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Reload Time"
                },
                new CardInfoStat
                {
                    amount = "<#ff00ff>+"+KFC.mysteryValue[3],
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Percentage Damage"
                },
                new CardInfoStat
                {
                    amount = "<#ff00ff>+8",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Ammo"
                },
                new CardInfoStat
                {
                    amount = "<#ff00ff>Reset",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Spread"
                }
            }
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            cardInfo.categories = new CardCategory[] { CustomCardCategories.instance.CardCategory("cantEternity") };
            gun.damage = (float)KFC.mysteryValue[1];
            gun.attackSpeed = 1f/(float)KFC.mysteryValue[1];
            gun.reloadTime = 1f/(float)KFC.mysteryValue[1];
            gun.percentageDamage = ((float)KFC.mysteryValue[1]-1f)/3f;
            gun.ammo = 8;
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            gun.spread = 0;
        }
        public override bool GetEnabled()
        {
            return false;
        }
    }
}