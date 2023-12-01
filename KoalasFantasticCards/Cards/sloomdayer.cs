using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus;
using UnboundLib;
using KFC.MonoBehaviors;
using KFC.Cards;
using Photon.Pun;
using System.Linq;

namespace KFC.Cards
{
    public class doomSlayer : SimpleCard
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "Doom Slayer",
            Description = "<#8B0000><b>Rip and Tear",
            ModName = KFC.ModInitials,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_DoomSlayer"),
            Rarity = RarityUtils.GetRarity("Legendary"),
            Theme = CardThemeColor.CardThemeColorType.DestructiveRed
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            gun.damage = 2.5f;
            statModifiers.health = 2.5f;
            statModifiers.movementSpeed = 1.5f;
            statModifiers.regen = 12.5f;
            statModifiers.lifeSteal = 2.5f;
        }
    }
}