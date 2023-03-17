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
using SimulationChamber;
using HarmonyLib;
using Photon.Pun;
using Photon.Pun.Simple;
using static UnityEngine.ParticleSystem;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;

namespace KFC.Cards
{
    public class Pexiltd : CustomEffectCard<meteorHandler>
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "Pexiltd",
            Description = "Rain down heavens anger apon those whom have hurt you",
            ModName = KFC.ModIntDed,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_Pexiltd"),
            Rarity = CardInfo.Rarity.Rare,
            Theme = CardThemeColor.CardThemeColorType.MagicPink,
            Stats = new[]
            {
                new CardInfoStat
                {
                    amount = "+20%",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Damage Growth"
                },
                new CardInfoStat
                {
                    amount = "-200%",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Attack Speed"
                },
                new CardInfoStat
                {
                    amount = "+4",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Bullets"
                },
                new CardInfoStat
                {
                    amount = "+12",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Ammo"
                },
                new CardInfoStat
                {
                    amount = "-40%",
                    positive = false,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Damage"
                },
                new CardInfoStat
                {
                    amount = "+50%",
                    positive = false,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Reload Time"
                }
            }
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { CustomCardCategories.instance.CardCategory("CardManipulation") };
            cardInfo.allowMultiple = false;
            gun.damageAfterDistanceMultiplier = 1.2f;
            gun.attackSpeed = 0.33f;
            gun.numberOfProjectiles = 4;
            gun.ammo = 12;
            gun.reloadTime = 1.5f;
            gun.spread = 0.15f;
            gun.gravity = 0.1f;
            gun.projectileSpeed = 1.5f;
            gun.projectileColor = Color.magenta;
            gun.damage = 0.6f;
        }
    }
}

namespace KFC.MonoBehaviors
{
    public class meteorHandler : CardEffect
    {
        public float height = 20f;
        public void Update()
        {
            var newPos = new Vector3(player.transform.position.x, player.transform.position.y + +(2.5f + height - player.transform.position.y), player.transform.position.z);
            var cursX = MainCam.instance.cam.ScreenToWorldPoint(Input.mousePosition).x;
            var cursY = MainCam.instance.cam.ScreenToWorldPoint(Input.mousePosition).y;
            var cursPos = new Vector3(cursX, cursY, player.transform.position.z);
            var targetPos = cursPos - newPos;
            gun.SetFieldValue("forceShootDir", targetPos);
        }
        public override void OnShoot(GameObject projectile)
        {
            var newPos = new Vector3(projectile.transform.position.x, projectile.transform.position.y + (2.5f + height - player.transform.position.y), projectile.transform.position.z);
            projectile.transform.position = newPos;
        }
    }
}