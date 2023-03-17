using HarmonyLib;
using KFC.Cards;
using KFC.MonoBehaviors;
using ModsPlus;
using Photon.Pun;
using RarityLib.Utils;
using Sonigon;
using Sonigon.Internal;
using SoundImplementation;
using System.Reflection;
using UnboundLib;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnboundLib.GameModes;

namespace KFC.Cards
{
    public class turret : CustomEffectCard<turretMono>
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "Turrets",
            Description = "I dont even know anymore",
            ModName = KFC.ModInitials,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_Torrets"),
            Rarity = RarityUtils.GetRarity("Epic"),
            Theme = CardThemeColor.CardThemeColorType.PoisonGreen,
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
    public class turretMono : CardEffect
    {
        public turretController controller;
        private void SpawnTurret()
        {
            var turror = PhotonNetwork.Instantiate("KFC_Turret",player.transform.position,Quaternion.identity);
            controller = turror.AddComponent<turretController>();
            turror.AddComponent<Gun>().Equals(gun);
            controller.owner = player;
        }
        public override void OnJump()
        {
            base.OnJump();
            SpawnTurret();
        }
        public override IEnumerator OnPointStart(IGameModeHandler gameModeHandler)
        {
            SpawnTurret();
            return base.OnPointStart(gameModeHandler);
        }
    }

    public class turretController : MonoBehaviour
    {
        public Player owner;
        private Rigidbody2D rigid;
        private float playerDist = 1000f;
        private Vector2 targetLoc;
        public Gun gun;
        private float maxHealth = 100f;
        private float health = 100f;
        private float damage = 100f;
        private CustomHealthBar turretHealh;
        private float attackSpeed = 0.2f;
        private float attackCount = 0f;

        private void Start()
        {
            rigid = gameObject.GetComponent<Rigidbody2D>();
            turretHealh = gameObject.AddComponent<CustomHealthBar>();
            turretHealh.SetColor(Color.magenta);
            turretHealh.SetValues(health, maxHealth);
            gun.damage = 100f;
            gun.gravity = 0f;
            gun.ignoreWalls = true;
        }
        private Vector2 nearestPlayer()
        {
            playerDist = 1000f;
            List<Player> enemyPlayers = PlayerManager.instance.players.Where((pl) => pl.playerID != owner.playerID && pl.teamID != owner.teamID && pl.data.health > 0).ToList();
            if (enemyPlayers.Count == 0) return Vector2.zero;
            targetLoc = Vector2.zero;
            foreach(var loc in enemyPlayers)
            {
                if(Vector2.Distance(gameObject.transform.position,loc.transform.position) < playerDist)
                {
                    targetLoc = loc.transform.position;
                    playerDist = Vector2.Distance(gameObject.transform.position,loc.transform.position);
                }
            }
            return targetLoc;
        }
        private void Update()
        {
            turretHealh.SetValues(health, maxHealth);
            var lookAt = nearestPlayer()-(Vector2)gameObject.transform.position;
            gameObject.transform.GetChild(0).gameObject.transform.up = lookAt;
            var aimVector = lookAt.normalized;
            attackCount += TimeHandler.deltaTime;
            print(damage);
            if(attackCount > attackSpeed)
            {
                attackCount = 0f;
                print("shoot");
            }
        }
    }
}