using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus;
using UnboundLib;
using KFC.RiftShenanigans;
using KFC.Cards;
using System.Linq;
using System.Collections.Generic;
using System;
using ClassesManagerReborn.Util;

namespace KFC.Cards
{
    public class RiftMind : SimpleCard
    {
        internal static CardInfo card = null;
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = RiftClass.name;
        }
        public override CardDetails Details => new CardDetails
        {
            Title = "Mind of the Rift",
            Description = "Your minds is so corrupt it cant be corrupted more",
            ModName = KFC.ModInitials,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_RiftMind"),
            Rarity = RarityUtils.GetRarity("Legendary"),
            Theme = CardThemeColor.CardThemeColorType.EvilPurple,
            Stats = new[]
            {
                new CardInfoStat
                {
                    amount = "+100%",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Projectile Speed"
                },
                new CardInfoStat
                {
                    amount = "<#FF00FF>Infinite",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Willpower"
                }
            }
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            this.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = KFC.ArtAssets.LoadAsset<Sprite>("locked");
            this.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.4f, 0.4f, 0.4f, 1);
            this.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Mask>().enabled = false;
            this.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(2).gameObject.SetActive(false);
            cardInfo.allowMultiple = false;
            gun.projectileSpeed = 2f;
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            player.gameObject.GetOrAddComponent<RiftFreeMono>();
            KFC.instance.ExecuteAfterFrames(20, () => {
                if (KFC.hasCard(RiftGun.card, player) && KFC.hasCard(RiftBody.card, player) && KFC.hasCard(RiftMind.card, player) && !KFC.hasCard(RiftSoul.card, player))
                {
                    ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, RiftSoul.card, false, "Rs", 0, 0);
                    ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, RiftSoul.card);
                }
            });
        }
        protected override void Removed(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            Destroy(player.gameObject.GetOrAddComponent<RiftFreeMono>());
        }
    }
}
namespace KFC.RiftShenanigans
{
    public class RiftFreeMono : MonoBehaviour
    {
        public void Update()
        {
            var player = gameObject.GetComponent<Player>();
            player.data.silenceHandler.StopSilence();
            player.data.stunHandler.StopStun();
            
        }
    }
}