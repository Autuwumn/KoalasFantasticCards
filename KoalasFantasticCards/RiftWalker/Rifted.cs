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
using ClassesManagerReborn.Util;
using System.IO;

namespace KFC.Cards
{
    public class Rifted : SimpleCard
    {
        internal static CardInfo card = null;
        private static int buffs = -2;
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = RiftClass.name;
        }
        public override CardDetails Details => new CardDetails
        {
            Title = "Void Fragment",
            Description = "Certifed Ruh Roh Raggy Moment",
            ModName = "RIFT",
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_Rifted"),
            Rarity = RarityUtils.GetRarity("Trinket"),
            Theme = CardThemeColor.CardThemeColorType.EvilPurple,
            Stats = new[]
            {
                new CardInfoStat
                {
                    amount = "<#FF00FF>-1%",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Stats"
                }
            }
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            this.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = KFC.ArtAssets.LoadAsset<Sprite>("locked");
            this.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.4f, 0.4f, 0.4f, 1);
            this.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Mask>().enabled = false;
            this.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(2).gameObject.SetActive(false);
            gun.damage = 0.99f;
            gun.dmgMOnBounce = 0.99f;
            gun.speedMOnBounce = 0.99f;
            gun.spread = 0.01f;
            gun.projectileSpeed = 0.99f;
            gun.reflects = -1;
            statModifiers.movementSpeed = 0.99f;
            statModifiers.health = 0.99f;
            gun.ammo = -1;
            gun.reloadTime = 1.01f;
            gun.attackSpeed = 1.01f;
            gun.reloadTimeAdd = 0.10f;
        }
        public override bool GetEnabled()
        {
            return false;
        }
    }
}
