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
    public class KoalaGlory : CustomEffectCard<Fortune>
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "Glory of Koala",
            Description = "You gain an abundance of fame and fortune",
            ModName = KFC.ModIntDed,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_KoalaGlory"),
            Rarity = RarityUtils.GetRarity("Legendary"),
            Theme = CardThemeLib.CardThemeLib.instance.CreateOrGetType("Koality"),
            Stats = new CardInfoStat[]
            {
                new CardInfoStat
                {
                    amount = "<#ff00ff>+25% of Others",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Max Hp"
                },
                new CardInfoStat
                {
                    amount = "<#ff00ff>10% Max Hp",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Healing per second"
                }
            }
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            cardInfo.GetAdditionalData().canBeReassigned = false;
            cardInfo.categories = new CardCategory[] { CustomCardCategories.instance.CardCategory("cantEternity") };
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var others = PlayerManager.instance.players.Where((p) => p.playerID != player.playerID).ToArray();
            float hpup = 0;
            foreach(var o in others)
            {
                hpup += o.data.maxHealth*0.25f;
            }
            player.data.maxHealth += hpup;
            player.data.health = player.data.maxHealth;
        }
        public override bool GetEnabled()
        {
            return false;
        }
    }
}
namespace KFC.MonoBehaviors
{
    public class Fortune : CardEffect
    {
        public void Update()
        {
            if(player.data.maxHealth > 0)
            {
                player.data.healthHandler.Heal(player.data.maxHealth * (1f/3f) * TimeHandler.deltaTime);
            }
        }
    }
}