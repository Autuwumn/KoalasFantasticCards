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
using ModdingUtils.Extensions;

namespace KFC.Cards
{
    public class KingOfSpeed : CustomEffectCard<armor_mono>
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "King Of Speed",
            Description = "Makes everybody fast, but none can match your Speed",
            ModName = KFC.ModInitials,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_SpeedKing"),
            Rarity = RarityUtils.GetRarity("Legendary"),
            Theme = CardThemeColor.CardThemeColorType.DestructiveRed,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat
                {
                    amount = "*10",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Your Speed"
                },
                new CardInfoStat
                {
                    amount = "*5",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Others Speed"
                }
            }
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            cardInfo.allowMultiple = false;
            cardInfo.GetAdditionalData().canBeReassigned = false;
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            characterStats.movementSpeed *= 10;
            player.data.jumps++;
            List<Player> otherPlayers = PlayerManager.instance.players.Where((p) => p.playerID != player.playerID).ToList();
            foreach(Player op in otherPlayers)
            {
                op.data.stats.movementSpeed *= 5;
            }
        }
        protected override void Removed(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            characterStats.movementSpeed /= 10;
            player.data.jumps--;
            List<Player> otherPlayers = PlayerManager.instance.players.Where((p) => p.playerID != player.playerID).ToList();
            foreach (Player op in otherPlayers)
            {
                op.data.stats.movementSpeed /= 5;
            }
        }
    }
}
