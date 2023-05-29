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
using Photon.Compression.Internal;

namespace KFC.Cards
{
    public class RockBottom : CustomEffectCard<BottomRock>
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "Rock Bottom",
            Description = "It's only up from here",
            ModName = KFC.ModIntDed,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_RockBottom"),
            Rarity = RarityUtils.GetRarity("Divine"),
            Theme = CardThemeColor.CardThemeColorType.TechWhite,
            Stats = new[]
            {
                new CardInfoStat
                {
                    amount = "-20%",
                    positive = false,
                    simepleAmount = CardInfoStat.SimpleAmount.aLotLower,
                    stat = "Damage"
                },
                new CardInfoStat
                {
                    amount = "-20%",
                    positive = false,
                    simepleAmount = CardInfoStat.SimpleAmount.aLotLower,
                    stat = "Health"
                },
                new CardInfoStat
                {
                    amount = "-20%",
                    positive = false,
                    simepleAmount = CardInfoStat.SimpleAmount.aLotLower,
                    stat = "Attack Speed"
                }
            }
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            statModifiers.health = 0.8f;
            gun.damage = 0.8f;
            gun.attackSpeed = 1/(0.8f);
        }
    }
}
namespace KFC.MonoBehaviors
{
    public class BottomRock : CardEffect
    {
        // damage, health, speed, jump, lifesteal
        // attackspeed, reloadtime, blockcd, sizeM
        // jumps, ammo, blocks, projectiles
        public float[] trueStatsFP = new float[] { 0, 0, 0, 0, 0 };
        public float[] trueStatsFN = new float[] { 0, 0, 0, 0 };
        public int[] trueStatsIP = new int[] { 0, 0, 0, 0, 0 };

        public float[] maxStatsFP = new float[] { 0, 0, 0, 0, 0 };
        public float[] maxStatsFN = new float[] { 0, 0, 0, 0 };
        public int[] maxStatsIP = new int[] { 0, 0, 0, 0, 0 };

        public bool build = false;
        
        protected override void Start()
        {
            KFC.instance.ExecuteAfterFrames(15, () =>
            {
                BuildStats();
            });
        }
        private void BuildStats()
        {
            trueStatsFP[0] = gun.damage;
            trueStatsFP[1] = data.maxHealth;
            trueStatsFP[2] = characterStats.movementSpeed;
            trueStatsFP[3] = characterStats.jump;
            trueStatsFP[4] = characterStats.lifeSteal;

            trueStatsFN[0] = gun.attackSpeed;
            trueStatsFN[1] = gun.GetComponentInChildren<GunAmmo>().reloadTime;
            trueStatsFN[2] = block.cooldown;
            trueStatsFN[3] = characterStats.sizeMultiplier;

            trueStatsIP[0] = characterStats.numberOfJumps;
            trueStatsIP[1] = gun.ammo;
            trueStatsIP[2] = block.additionalBlocks;
            trueStatsIP[3] = gun.numberOfProjectiles;
            trueStatsIP[4] = gun.reflects;



            maxStatsFP[0] = gun.damage;
            maxStatsFP[1] = data.maxHealth;
            maxStatsFP[2] = characterStats.movementSpeed;
            maxStatsFP[3] = characterStats.jump;
            maxStatsFP[4] = characterStats.lifeSteal;

            maxStatsFN[0] = gun.attackSpeed;
            maxStatsFN[1] = gun.GetComponentInChildren<GunAmmo>().reloadTime;
            maxStatsFN[2] = block.cooldown;
            maxStatsFN[3] = characterStats.sizeMultiplier;

            maxStatsIP[0] = characterStats.numberOfJumps;
            maxStatsIP[1] = gun.ammo;
            maxStatsIP[2] = block.additionalBlocks;
            maxStatsIP[3] = gun.numberOfProjectiles;
            maxStatsIP[4] = gun.reflects;

            build = true;
        }
        void Update()
        {
            if (build)
            {
                var masterListFPos = new[] { gun.damage, data.maxHealth, characterStats.movementSpeed, characterStats.jump, characterStats.lifeSteal };
                var masterListFNeg = new[] { gun.attackSpeed, gun.GetComponentInChildren<GunAmmo>().reloadTime, block.cooldown, characterStats.sizeMultiplier };
                var masterListI = new[] { characterStats.numberOfJumps, gun.ammo, block.additionalBlocks, gun.numberOfProjectiles, gun.reflects };

                for (var i = 0; i < masterListFPos.Length; i++)
                {
                    var a = masterListFPos[i];
                    if (a != maxStatsFP[i])
                    {
                        float m = a / maxStatsFP[i];
                        trueStatsFP[i] *= m;
                        if (trueStatsFP[i] > maxStatsFP[i]) maxStatsFP[i] = trueStatsFP[i];
                    }
                }
                for (var i = 0; i < masterListFNeg.Length; i++)
                {
                    var a = masterListFNeg[i];
                    if (a != maxStatsFN[i])
                    {
                        float m = a / maxStatsFN[i];
                        trueStatsFN[i] *= m;
                        if (trueStatsFN[i] < maxStatsFN[i]) maxStatsFN[i] = trueStatsFN[i];
                    }
                }
                for (var i = 0; i < masterListI.Length; i++)
                {
                    var a = masterListI[i];
                    if (a != maxStatsIP[i])
                    {
                        int d = a - maxStatsIP[i];
                        trueStatsIP[i] += d;
                        if (trueStatsIP[i] > maxStatsIP[i]) maxStatsIP[i] = trueStatsIP[i];
                    }
                }

                gun.damage = maxStatsFP[0];
                var r = data.maxHealth / data.health;
                data.maxHealth = maxStatsFP[1];
                data.health = data.maxHealth / r;
                characterStats.movementSpeed = maxStatsFP[2];
                characterStats.jump = maxStatsFP[3];
                characterStats.lifeSteal = maxStatsFP[4];

                gun.attackSpeed = maxStatsFN[0];
                gun.GetComponentInChildren<GunAmmo>().reloadTime = maxStatsFN[1];
                block.cooldown = maxStatsFN[2];
                characterStats.sizeMultiplier = maxStatsFN[3];

                characterStats.numberOfJumps = maxStatsIP[0];
                gun.ammo = maxStatsIP[1];
                block.additionalBlocks = maxStatsIP[2];
                gun.numberOfProjectiles = maxStatsIP[3];
                gun.reflects = maxStatsIP[4];
            }   
        }

    }
}