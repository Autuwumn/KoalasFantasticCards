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
using UnityEngine.Sprites;

namespace KFC.Cards
{
    public class Triangle : SimpleCard
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "Triangle",
            Description = "Gain a 100 hp armor that is seperate from your main hp",
            ModName = KFC.ModInitials,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_Triangle"),
            Rarity = CardInfo.Rarity.Rare,
            Theme = CardThemeColor.CardThemeColorType.FirepowerYellow
        };

        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var objs = FindObjectsOfType<SpriteRenderer>();
            foreach(var o in objs)
            {
                o.sprite = KFC.ArtAssets.LoadAsset<GameObject>("trungle").GetComponent<SpriteRenderer>().sprite;
            }
        }
    }
}
