using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus;
using UnboundLib;
using KFC.MonoBehaviors;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using UnboundLib.Networking;
using Photon.Pun.Simple;
using System.Linq;

namespace KFC.Cards
{
    class RedHerring : SimpleCard
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "Red Herring",
            Description = "Gives between *5 and /5 to each basic stat\nDisplays stats after pickup",
            ModName = KFC.ModInitials,
            Art = null,
            Rarity = CardInfo.Rarity.Rare,
            Theme = CardThemeColor.CardThemeColorType.DestructiveRed,
            Stats = null
        };
        public float GetRandomBalancedFloat()
        {
            float num;
            float pos = UnityEngine.Random.Range(0f, 1f);
            float mult = UnityEngine.Random.Range(0.2f, 1f);
            if(pos > 0.5f)
            {
                num = 1f / mult; 
            } else
            {
                num = mult;
            }
            return num;
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            card.cardStats = null;
            cardInfo.allowMultiple = false;
            gun.spread = 0.1f;
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            if (!player.data.view.IsMine) return;
            float[] rngs = new float[] { GetRandomBalancedFloat(), GetRandomBalancedFloat(), GetRandomBalancedFloat(), GetRandomBalancedFloat(), GetRandomBalancedFloat(), GetRandomBalancedFloat() };
            int[] rngits = new int[] { UnityEngine.Random.Range(-5, 5), UnityEngine.Random.Range(-5, 5) };
            NetworkingManager.RPC(typeof(RedHerring), nameof(RPC_Herring), player.playerID, rngs, rngits);
            KFC.instance.ExecuteAfterFrames(20, () =>
            {
                if(gun.numberOfProjectiles < 1)
                {
                    gun.numberOfProjectiles = 1;
                }
            });
        }
        [UnboundRPC]
        public static void RPC_Herring(int pid, float[] fluts, int[] onts)
        {
            card.cardStats = new CardInfoStat[8]; 
            KFC.instance.ExecuteAfterFrames(10, () =>
            {
                var player = PlayerManager.instance.GetPlayerWithID(pid);
                var gun = player.data.weaponHandler.gun;
                var gunAmmo = gun.GetComponentInChildren<GunAmmo>();
                var stats = player.data.stats;
                gun.damage *= fluts[0];
                gun.attackSpeed *= fluts[1];
                gun.reloadTime *= fluts[2];
                gun.projectileSpeed *= fluts[3];
                player.data.maxHealth *= fluts[4];
                player.data.health = player.data.maxHealth;
                stats.movementSpeed *= fluts[5];
                gunAmmo.maxAmmo += onts[0];
                gun.numberOfProjectiles += onts[1];
                card.cardStats[0] = new CardInfoStat { amount = "*" + fluts[0], positive = true, simepleAmount = CardInfoStat.SimpleAmount.notAssigned, stat = "Damage" };
                card.cardStats[1] = new CardInfoStat { amount = "*" + fluts[1], positive = true, simepleAmount = CardInfoStat.SimpleAmount.notAssigned, stat = "Attack Speed" };
                card.cardStats[2] = new CardInfoStat { amount = "*" + fluts[2], positive = true, simepleAmount = CardInfoStat.SimpleAmount.notAssigned, stat = "Reload Time" };
                card.cardStats[3] = new CardInfoStat { amount = "*" + fluts[3], positive = true, simepleAmount = CardInfoStat.SimpleAmount.notAssigned, stat = "Projectile Speed" };
                card.cardStats[4] = new CardInfoStat { amount = "*" + fluts[4], positive = true, simepleAmount = CardInfoStat.SimpleAmount.notAssigned, stat = "Health" };
                card.cardStats[5] = new CardInfoStat { amount = "*" + fluts[5], positive = true, simepleAmount = CardInfoStat.SimpleAmount.notAssigned, stat = "Speed" };
                card.cardStats[6] = new CardInfoStat { amount = "+" + onts[0], positive = true, simepleAmount = CardInfoStat.SimpleAmount.notAssigned, stat = "Ammo" };
                card.cardStats[7] = new CardInfoStat { amount = "+" + onts[1], positive = true, simepleAmount = CardInfoStat.SimpleAmount.notAssigned, stat = "Bullets" };
                if (fluts[0] < 1) card.cardStats[0] = new CardInfoStat { amount = "*" + fluts[0], positive = false, simepleAmount = CardInfoStat.SimpleAmount.notAssigned, stat = "Damage" };
                if (fluts[1] > 1) card.cardStats[1] = new CardInfoStat { amount = "*" + fluts[1], positive = false, simepleAmount = CardInfoStat.SimpleAmount.notAssigned, stat = "Attack Speed" };
                if (fluts[2] > 1) card.cardStats[2] = new CardInfoStat { amount = "*" + fluts[2], positive = false, simepleAmount = CardInfoStat.SimpleAmount.notAssigned, stat = "Reload Time" };
                if (fluts[3] < 1) card.cardStats[3] = new CardInfoStat { amount = "*" + fluts[3], positive = false, simepleAmount = CardInfoStat.SimpleAmount.notAssigned, stat = "Projectile Speed" };
                if (fluts[4] < 1) card.cardStats[4] = new CardInfoStat { amount = "*" + fluts[4], positive = false, simepleAmount = CardInfoStat.SimpleAmount.notAssigned, stat = "Health" };
                if (fluts[5] < 1) card.cardStats[5] = new CardInfoStat { amount = "*" + fluts[5], positive = false, simepleAmount = CardInfoStat.SimpleAmount.notAssigned, stat = "Speed" };
                if (onts[0] < 0) card.cardStats[6] = new CardInfoStat { amount = "" + onts[0], positive = false, simepleAmount = CardInfoStat.SimpleAmount.notAssigned, stat = "Ammo" };
                if (onts[1] < 0) card.cardStats[7] = new CardInfoStat { amount = "" + onts[1], positive = false, simepleAmount = CardInfoStat.SimpleAmount.notAssigned, stat = "Bullet" };
            });
        }
    }
}