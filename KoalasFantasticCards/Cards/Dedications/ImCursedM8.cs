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
using CardChoiceSpawnUniqueCardPatch.CustomCategories;

namespace KFC.Cards
{
    public class ImCursed : CustomEffectCard<GattlingHandler>
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "ImCursedM8's Gattlin Guun",
            Description = "Lay waste to all before you",
            ModName = KFC.ModIntDed,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_CursedM8"),
            Rarity = CardInfo.Rarity.Rare,
            Theme = CardThemeColor.CardThemeColorType.DestructiveRed,
            Stats = new[]
            {
                new CardInfoStat
                {
                    amount = "+900%",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Attack Speed"
                },
                new CardInfoStat
                {
                    amount = "-70%",
                    positive = false,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Damage"
                },
                new CardInfoStat
                {
                    amount = "+400%",
                    positive = false,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Reload time"
                },
                new CardInfoStat
                {
                    amount = "+60",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Ammo"
                }
            }
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            gun.reloadTime = 5f;
            gun.attackSpeed = 0.1f;
            gun.ammo = 60;
            gun.damage = 0.3f;
            gun.numberOfProjectiles = 1;
            gun.projectileColor = Color.red;
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var sr = gun.transform.GetChild(1).GetChild(3).gameObject.GetComponent<SpriteRenderer>();
            if(gun.spread == 0)
            {
                gun.spread = 0.2f;
            }
            base.Added(player, gun, gunAmmo, data, health, gravity, block, characterStats);
        }
    }
}
namespace KFC.MonoBehaviors
{
    public class GattlingHandler : CardEffect
    {
        public GameObject gattling;
        public void OnEnable()
        {
            if (!gattling)
            {
                gattling = Instantiate(KFC.ArtAssets.LoadAsset<GameObject>("gattlin"));
            }
        }
        public void Update()
        {
            if (gattling)
            {
                if (player.isActiveAndEnabled)
                {
                    gattling.transform.position = player.data.hand.position;
                    gattling.transform.up = player.data.aimDirection;
                } else
                {
                    gattling.transform.position = new Vector3(1000, 1000, 0);
                }
            }
        }
        public override IEnumerator OnPointStart(IGameModeHandler gameModeHandler)
        {
            if(!gattling)
            {
                gattling = Instantiate(KFC.ArtAssets.LoadAsset<GameObject>("gattlin"));
            }
            return base.OnPointStart(gameModeHandler);
        }
        public override IEnumerator OnPointEnd(IGameModeHandler gameModeHandler)
        {
            Destroy(gattling); gattling = null;
            return base.OnPointEnd(gameModeHandler);
        }
        protected override void OnDestroy()
        {
            Destroy(gattling);
            base.OnDestroy();
        }
    }
}