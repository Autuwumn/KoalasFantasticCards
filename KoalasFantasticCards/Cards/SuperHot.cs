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
    public class SuperHot : CustomEffectCard<superhot_mono>
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "Armor",
            Description = "Gain a 100 hp armor that is seperate from your main hp",
            ModName = KFC.ModInitials,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_Armor"),
            Rarity = CardInfo.Rarity.Rare,
            Theme = CardThemeColor.CardThemeColorType.FirepowerYellow,
            OwnerOnly = true
        };
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            foreach(var p in PlayerManager.instance.players)
            {

            }
        }
    }
}

namespace KFC.MonoBehaviors
{
    public class superhot_mono : CardEffect
    {
        private float moveCD = 2.5f;
        private float moveCDMax = 2.5f;
        public void Update()
        {
            if (player.isActiveAndEnabled)
            {
                if (Input.anyKey)
                {
                    moveCD = 0f;
                }
            }
            moveCD += Time.deltaTime;
            if(moveCD > moveCDMax)
            {

            }
        }
    }
}