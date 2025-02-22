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
using System.Collections;
using UnboundLib.GameModes;
using Photon.Pun;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;

namespace KFC.Cards
{
    public class Geballion : CustomEffectCard<GeballionsAura>
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "Might of Geballion",
            Description = "Aura of pain",
            ModName = KFC.ModIntDed,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_Geballion"),
            Rarity = CardInfo.Rarity.Rare,
            Theme = CardThemeColor.CardThemeColorType.DestructiveRed,
            Stats = new[]
            {
                new CardInfoStat
                {
                    amount = "<#FF8000>5% your max hp",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "dmg to nearby players"
                },
                new CardInfoStat
                {
                    amount = "<#FF8000>+150%",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Health"
                },
                new CardInfoStat
                {
                    amount = "<#FF8000>+25",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Regen"
                }
            }
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            statModifiers.health = 2.5f;
            statModifiers.regen = 25;
        }
    }
}
namespace KFC.MonoBehaviors
{
    public class GeballionsAura : CardEffect
    {
        private int time = 0;
        private bool inGame = false;
        //private GameObject ring;

        public void OnEnable()
        {
            inGame = true;
            //ring = KFC.ArtAssets.LoadAsset<GameObject>("GeballionsAura");
            //Instantiate(ring);    
        }
        public override IEnumerator OnRoundStart(IGameModeHandler gameModeHandler)
        {
            inGame = true;
            time = 0;
            yield break;
        }
        public override void OnRevive()
        {
            inGame = true;
            time = 0;
        }
        public override IEnumerator OnRoundEnd(IGameModeHandler gameModeHandler)
        {
            inGame = false;
            yield break;
        }
        public void Update()
        {
            time++;
            if(inGame == true && time >= 300)
            {
                //ring.transform.position = player.transform.position;
                List<Player> enemyPlayers = PlayerManager.instance.players.Where((pl) => pl.playerID != player.playerID && pl.teamID != player.teamID && pl.data.health > 0).ToList();
                if (player.data.health <= 0)
                {
                    inGame = false;
                    return;
                }
                foreach(Player ployer in enemyPlayers)
                {
                    if(Vector3.Distance(player.transform.position, ployer.transform.position) < 10) 
                    {
                        var dmgDealt = Vector2.one*player.data.maxHealth*0.05f*TimeHandler.deltaTime;
                        ployer.data.healthHandler.DoDamage(dmgDealt, Vector2.zero, Color.red,null,player,true,true,true);
                    }
                }
            }
        }
    }
}