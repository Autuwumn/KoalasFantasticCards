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

namespace KFC.Cards
{
    public class KoalaWrath : CustomEffectCard<KoalasDeath>
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "Koala's Wrath",
            Description = "Thou has angered me",
            ModName = KFC.ModIntDed,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_KoalaWrath"),
            Rarity = RarityUtils.GetRarity("Trinket"),
            Theme = CardThemeLib.CardThemeLib.instance.CreateOrGetType("Koality"),
            Stats = new CardInfoStat[]
            {
                new CardInfoStat
                {
                    amount = "<#ff00ff>-15%",
                    positive = false,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Hp per second"
                }
            }
        };
        public override bool GetEnabled()
        {
            return false;
        }
    }
}

namespace KFC.MonoBehaviors
{
    public class KoalasDeath : CardEffect
    {
        public void Update()
        {
            var dmgMult = player.data.maxHealth * 0.15f * TimeHandler.deltaTime;
            player.data.healthHandler.CallTakeDamage(Vector2.one * dmgMult, Vector2.zero, null, null, true);
        }
        public override IEnumerator OnPointStart(IGameModeHandler gameModeHandler)
        {
            player.data.health = player.data.maxHealth;
            yield return null;
        }
    }
}