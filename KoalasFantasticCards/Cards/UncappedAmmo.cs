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

namespace KFC.Cards
{
    public class UncappedAmmo : CustomEffectCard<UnlimitedAmmo>
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "Uncapped Ammo",
            Description = "+400 ammo",
            ModName = KFC.ModInitials,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_Ammo"),
            Rarity = CardInfo.Rarity.Rare,
            Theme = CardThemeColor.CardThemeColorType.FirepowerYellow
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            gun.ammo = 400;
        }
    }
}
namespace KFC.MonoBehaviors
{
    public class UnlimitedAmmo : CardEffect
    {
        public void Update()
        {
            var ammo = 3;
            foreach(var c in player.data.currentCards)
            {
                ammo += c.gameObject.GetComponent<Gun>().ammo;
            }
            gunAmmo.maxAmmo = ammo;
        }
    }
}