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
    public class KoalaStats : CustomEffectCard<StatThief>
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "Stats of Koala",
            Description = "You steal 1% of others basic stats every point, basic stats listed below",
            ModName = KFC.ModIntDed,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_KoalasStats"),
            Rarity = RarityUtils.GetRarity("Legendary"),
            Theme = CardThemeLib.CardThemeLib.instance.CreateOrGetType("Koality"),
            Stats = new CardInfoStat[]
            {
                new CardInfoStat
                {
                    amount = "<#ff00ff>Damage <#ffffff>|",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "<#ff00ff>Health"
                },
                new CardInfoStat
                {
                    amount = "<#ff00ff>Attack Speed <#ffffff>|",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "<#ff00ff>Reload Time"
                },
                new CardInfoStat
                {
                    amount = "<#ff00ff>Speed <#ffffff>|",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "<#ff00ff>Jump Height"
                },
                new CardInfoStat
                {
                    amount = "<#ff00ff>Block CD <#ffffff>|",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "<#ff00ff>Enjoyment"
                }
            }
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            cardInfo.categories = new CardCategory[] { CustomCardCategories.instance.CardCategory("cantEternity") };
        }
        public override bool GetEnabled()
        {
            return false;
        }
    }
}
namespace KFC.MonoBehaviors
{
    public class StatThief : CardEffect
    {
        public override IEnumerator OnPointStart(IGameModeHandler gameModeHandler)
        {
            var others = PlayerManager.instance.players.Where((p) => p.playerID != player.playerID).ToList();
            // damage, health, attack speed, reload time, movement speed, jump height, block cooldown
            var statChanges = new[] { 0f, 0f, 0f, 0f, 0f, 0f, 0f };
            foreach (var o in others)
            {
                var g = o.data.weaponHandler.gun;
                statChanges[0] += g.damage;
                g.damage *= 0.99f;
                statChanges[1] += o.data.maxHealth / 100;
                o.data.maxHealth *= 0.99f;
                o.data.health = o.data.maxHealth;
                statChanges[2] += 0.3f / g.attackSpeed;
                g.attackSpeed /= 0.99f;
                statChanges[3] += 1f / g.gameObject.GetComponentInChildren<GunAmmo>().reloadTimeMultiplier;
                g.gameObject.GetComponentInChildren<GunAmmo>().reloadTimeMultiplier /= 0.99f;
                statChanges[4] += o.data.stats.movementSpeed;
                o.data.stats.movementSpeed *= 0.99f;
                statChanges[5] += o.data.stats.jump;
                o.data.stats.jump *= 0.99f;
                statChanges[6] += 1f/o.data.block.cdMultiplier;
                o.data.block.cdMultiplier /= 0.99f;
            }
            for (var i = 0; i < statChanges.Length; i++)
            {
                statChanges[i] *= 0.01f;
                statChanges[i] += 1;
            }
            gun.damage *= statChanges[0];
            player.data.maxHealth *= statChanges[1];
            player.data.health = player.data.maxHealth;
            gun.attackSpeedMultiplier /= statChanges[2];
            gunAmmo.reloadTimeMultiplier /= statChanges[3];
            characterStats.movementSpeed *= statChanges[4];
            characterStats.jump *= statChanges[5];
            block.cdMultiplier *= statChanges[6];
            yield return null;
        }
    }
}