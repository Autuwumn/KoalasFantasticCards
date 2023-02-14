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
using System.Xml.Schema;

namespace KFC.Cards
{
    public class excaliber : CustomEffectCard<excaliber_Mono>
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "ExCrucible",
            Description = "You are worthy",
            ModName = KFC.ModInitials,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_Excaliber"),
            Rarity = RarityUtils.GetRarity("Divine"),
            Theme = CardThemeColor.CardThemeColorType.DestructiveRed,
            OwnerOnly = true
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
        }
    }
}

namespace KFC.MonoBehaviors
{
    public class excaliber_Mono : CardEffect
    {
        public ExcaliberSword_Mono excaliberSword;
        private float swordShoot = 0f;
        private CustomHealthBar swordCool;
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
            swordCool = excaliberSword.gameObject.transform.GetChild(2).gameObject.AddComponent<CustomHealthBar>();
            swordCool.SetValues(swordShoot, gun.attackSpeed*5f / 0.3f);
            swordCool.SetColor(Color.red);
            FixSword();
        }
        private void FixSword()
        {
            foreach (var render in gun.transform.GetComponentsInChildren<Renderer>())
            {
                render.enabled = false;
            }
            var polli = excaliberSword.gameObject.GetComponentInChildren<PolygonCollider2D>();
            foreach (var colli in player.GetComponentsInChildren<Collider2D>())
            {
                Physics2D.IgnoreCollision(polli, colli);
            }
        }
        public override void OnShoot(GameObject projectile)
        {
            Destroy(projectile);
            if (excaliberSword.boomer == false && swordShoot > gun.attackSpeed*5f/0.3f)
            {
                excaliberSword.StartBoomer();
                swordShoot = 0f;
            }
        }
        public void Update()
        {
            swordShoot += TimeHandler.deltaTime;
            swordCool.SetValues(swordShoot, gun.attackSpeed*5f/0.3f);
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
        public bool boomer = false;
        private Vector2 swordLoc;
        private Vector3 swordAim;
        private Vector3 targetLoc;
        private bool goOut = true;
        private Vector3 playerRemember;

        private void Start()
        {
            rigid = gameObject.GetComponent<Rigidbody2D>();
            damagebox = gameObject.GetComponentInChildren<DamageBox>();
        }
        public void StartBoomer()
        {
            boomer = true;
            goOut = true;
            targetLoc = player.data.hand.position+player.data.aimDirection.normalized*30f;
            swordAim = player.data.aimDirection;
            playerRemember = player.data.hand.position;
        }
        public Vector2 RotateVector(Vector2 v, float angle)
        {
            float radian = angle * Mathf.Deg2Rad;
            float _x = v.x * Mathf.Cos(radian) - v.y * Mathf.Sin(radian);
            float _y = v.x * Mathf.Sin(radian) + v.y * Mathf.Cos(radian);
            return new Vector2(_x, _y);
        }
        private void Update()
        {
            if (!boomer)
            {
                swordLoc = player.data.hand.position;
                swordAim = player.data.aimDirection;
            }
            if (boomer)
            {
                if(goOut)
                {
                    swordLoc = Vector2.MoveTowards(swordLoc, targetLoc,60*TimeHandler.deltaTime*player.data.weaponHandler.gun.projectileSpeed);
                    if (Vector2.Distance(swordLoc,(Vector2)targetLoc) < 1) goOut = false;
                    swordAim = player.data.aimDirection;
                }
                if(!goOut)
                {
                    swordLoc = Vector2.MoveTowards(swordLoc, player.data.hand.position,60*TimeHandler.deltaTime*player.data.weaponHandler.gun.projectileSpeed);
                    if(Vector2.Distance(swordLoc,(Vector2)player.data.hand.position) < 1) boomer = false;
                    swordAim = player.data.aimDirection;
                }
            }
            player.data.weaponHandler.gun.ammo = 0;
            rigid.position = swordLoc;
            rigid.transform.up = swordAim;
            if(player.data.health <= 0)
            {
                rigid.position = new Vector2(1000, 1000);
            }
            damagebox.damage = player.data.weaponHandler.gun.damage*55f;
            damagebox.force = player.data.weaponHandler.gun.projectileSpeed * 1000f;
        }
    }
}