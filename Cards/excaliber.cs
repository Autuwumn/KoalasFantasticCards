using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus;
using UnboundLib;
using KFC.MonoBehaviors;
using KFC.Cards;
using Photon.Pun;
using System;
using System.Collections;
using UnboundLib.GameModes;
using UnityEngine.UI;


namespace KFC.Cards
{
    public class excaliber : CustomEffectCard<excaliber_Mono>
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "Excaliber",
            Description = "You did it, you are the king",
            ModName = KFC.ModInitials,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_Excaliber"),
            Rarity = RarityUtils.GetRarity("Divine"),
            Theme = CardThemeColor.CardThemeColorType.DestructiveRed
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            gun.attackSpeed = 999999f;
            //gun.projectileColor = Color.clear;

        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            
        }
    }
}

namespace KFC.MonoBehaviors
{
    [DisallowMultipleComponent]
    public class excaliber_Mono : CardEffect
    {
        public ExcaliberSword_Mono excaliberSword;
        protected override void Start()
        {
            base.Start();
            foreach (var render in gun.transform.GetComponentsInChildren<Renderer>())
            {
                render.enabled = false;
            }
            var escaber = PhotonNetwork.Instantiate("KFC_Excaliber", data.hand.transform.position, Quaternion.identity);
            excaliberSword = escaber.AddComponent<ExcaliberSword_Mono>();
            excaliberSword.player = player;
            gun.transform.GetComponentInChildren<Canvas>().gameObject.AddComponent<CanvasGroup>().alpha = 0f;
            FixSword();
        }
        private void FixSword()
        {
            foreach (var render in gun.transform.GetComponentsInChildren<Renderer>())
            {
                render.enabled = false;
            }
            var polli = excaliberSword.gameObject.GetComponentInChildren<Collider2D>();
            foreach (var colli in player.GetComponentsInChildren<Collider2D>())
            {
                Physics2D.IgnoreCollision(polli, colli);
            }
        }
        public void OnEnable()
        {
            FixSword();
        }
        public override IEnumerator OnPointStart(IGameModeHandler gameModeHandler)
        {
            FixSword();
            yield break;
        }
        public override void OnRevive()
        {
            FixSword();
        }
        protected override void OnDestroy()
        {
            PhotonNetwork.Destroy(excaliberSword.gameObject);
            base.OnDestroy();
        }
    }
    public class ExcaliberSword_Mono : MonoBehaviour
    {
        public Player player;
        private Rigidbody2D rigid;
        private DamageBox damagebox;
        
        private void Start()
        {
            rigid = gameObject.GetComponent<Rigidbody2D>();
            damagebox = gameObject.GetComponentInChildren<DamageBox>();
        }

        private void Update()
        {
            rigid.position = player.data.hand.position;
            rigid.transform.up = player.data.aimDirection;
            damagebox.damage = player.data.weaponHandler.gun.damage*55f;
            damagebox.force = player.data.weaponHandler.gun.projectileSpeed * 1000f;
        }
    }
}