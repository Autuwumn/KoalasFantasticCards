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

namespace KFC.Cards
{
    class RedHerring : SimpleCard
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "Red Herring",
            Description = null,
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
                gunAmmo.maxAmmo += onts[0];
                gun.numberOfProjectiles += onts[1];
                player.data.maxHealth *= fluts[4];
                player.data.health = player.data.maxHealth;
                stats.movementSpeed *= fluts[5];
            });
        }
    }
}