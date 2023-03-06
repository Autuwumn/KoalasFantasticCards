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
    public class ImCursed : CustomEffectCard<GattlingHandler>
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "ImCursedM8",
            Description = "Lmao Gattling Gun",
            ModName = KFC.ModIntDed,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_CursedM8"),
            Rarity = CardInfo.Rarity.Rare,
            Theme = CardThemeColor.CardThemeColorType.DestructiveRed
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.reloadTime = 0.00001f;
            gun.attackSpeed = 0.1f;
            gun.ammo = 27;
            gun.spread = 0.1f;
            gun.damage = 0.1f;
            gun.projectileColor = Color.red;
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var sr = gun.transform.GetChild(1).GetChild(3).gameObject.GetComponent<SpriteRenderer>();
            
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
                gattling.transform.position = player.data.hand.position;
                gattling.transform.up = player.data.aimDirection;
            }
        }
        public void Update()
        {
            if (gattling)
            {
                gattling.transform.position = player.data.hand.position;
                gattling.transform.up = player.data.aimDirection;
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