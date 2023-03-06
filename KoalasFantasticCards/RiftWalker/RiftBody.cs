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
using Photon.Pun;
using System.Xml.Schema;
using System.Collections;
using UnboundLib.GameModes;

namespace KFC.Cards
{
    public class RiftBody : CustomEffectCard<RiftBodyMono>
    {
        internal static CardInfo card = null;
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = RiftClass.name;
        }
        public override CardDetails Details => new CardDetails
        {
            Title = "Body of the Rift",
            Description = "Step into the rift for a short time",
            ModName = KFC.ModInitials,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_RiftBody"),
            Rarity = RarityUtils.GetRarity("Legendary"),
            Theme = CardThemeColor.CardThemeColorType.EvilPurple,
            Stats = new[]
            {
                new CardInfoStat
                {
                    amount = "+100%",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Health"
                },
                new CardInfoStat
                {
                    amount = "+15",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Regen"
                },
                new CardInfoStat
                {
                    amount = "<#FF00FF>Unstable",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Body"
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
            statModifiers.health = 2f;
            statModifiers.regen = 15f;
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            KFC.instance.ExecuteAfterFrames(20, () => {
                if (KFC.hasCard(RiftGun.card, player) && KFC.hasCard(RiftBody.card, player) && KFC.hasCard(RiftMind.card, player) && !KFC.hasCard(RiftSoul.card, player))
                {
                    ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, RiftSoul.card, false, "Rs", 0, 0);
                    ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, RiftSoul.card);
                }
            });
        }
    }
}
namespace KFC.RiftShenanigans
{
    public class RiftBodyMono : CardEffect
    {
        private float bodyTimer = 12.5f;
        private ParticleSystem[] jumpParts;
        private ParticleSystem[] landParts;

        public override IEnumerator OnPointStart(IGameModeHandler gameModeHandler)
        {
            bodyTimer = 12.5f;
            yield break;
        }
        private void InvisPlayer()
        {
            player.data.weaponHandler.gun.gameObject.SetActive(false);
            player.transform.GetChild(0).gameObject.SetActive(false);
            if (player.data.view.IsMine) return;
            player.transform.GetChild(1).gameObject.SetActive(false);
            player.transform.GetChild(2).gameObject.SetActive(false);
            player.transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(false);
            player.transform.GetChild(4).transform.GetChild(0).gameObject.SetActive(false);
            player.transform.GetChild(6).gameObject.SetActive(false);
            jumpParts = player.data.jump.jumpPart;
            landParts = player.data.landParts;
            player.data.jump.jumpPart = null;
            player.data.landParts = null;
            player.data.healthHandler.gameObject.SetActive(false);
            foreach (var coli in player.GetComponentsInChildren<SpriteRenderer>())
            {
                coli.enabled = false;
            }
        }
        private void VisPlayer()
        {
            player.data.weaponHandler.gun.gameObject.SetActive(true);
            player.transform.GetChild(0).gameObject.SetActive(true);
            player.transform.GetChild(1).gameObject.SetActive(true);
            player.transform.GetChild(2).gameObject.SetActive(true);
            player.transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(true);
            player.transform.GetChild(4).transform.GetChild(0).gameObject.SetActive(true);
            player.transform.GetChild(6).gameObject.SetActive(true);
            player.data.jump.jumpPart = jumpParts;
            player.data.landParts = landParts;
            player.data.healthHandler.gameObject.SetActive(true);
            foreach (var coli in player.GetComponentsInChildren<SpriteRenderer>())
            {
                coli.enabled = true;
            }
        }
        public override void OnBlock(BlockTrigger.BlockTriggerType blockTriggerType)
        {
            base.OnBlock(blockTriggerType);
            if (bodyTimer > 12.5f)
            {
                bodyTimer = 0f;
                for(var i = 0; i < player.transform.childCount; i++)
                {
                    var chuld = player.transform.GetChild(i).gameObject;
                    if(chuld.name == "BegoneThot(Clone)" || chuld.name == "SupriseMofo(Clone)") Destroy(chuld);
                }
                var partA = KFC.ArtAssets.LoadAsset<GameObject>("BegoneThot");
                Instantiate(partA, player.transform);
                InvisPlayer();
                foreach(var coli in player.GetComponentsInChildren<Collider2D>())
                {
                    coli.enabled = false;
                }
                KFC.instance.ExecuteAfterSeconds(5f, () =>
                {
                    VisPlayer();
                    foreach (var coli in player.GetComponentsInChildren<Collider2D>())
                    {
                        coli.enabled = true;
                    }
                    var partB = KFC.ArtAssets.LoadAsset<GameObject>("SupriseMofo");
                    Instantiate(partB, player.transform);
                    KFC.instance.ExecuteAfterSeconds(10f, () =>
                    {
                        var partC = KFC.ArtAssets.LoadAsset<GameObject>("SupriseMofo");
                        Instantiate(partC, player.transform);
                    });
                });
            }
        }
        public void Update()
        {
            bodyTimer += TimeHandler.deltaTime;
        }
    }
}