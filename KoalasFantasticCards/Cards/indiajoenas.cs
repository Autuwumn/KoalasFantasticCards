using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus;
using UnboundLib;
using Jotunn.Utils;
using Photon.Pun;
using KFC.MonoBehaviors;
using System.Xml.Schema;
using System.Collections;
using UnboundLib.GameModes;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon;

namespace KFC.Cards
{
    class indiajoenas : CustomEffectCard<indiajon_Mono>
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title       = "Imbiamba Jombes",
            Description = "Definetly not a refrence, sorry theres a key on my catboard",
            ModName     = KFC.ModInitials,
            Art         = KFC.ArtAssets.LoadAsset<GameObject>("C_IndiaJoenas"),
            Rarity      = RarityUtils.GetRarity("Epic"),
            Theme       = CardThemeColor.CardThemeColorType.EvilPurple,
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
    public class indiajon_Mono : CardEffect
    {
        private int rocks = 0;
        public void SpawnBall()
        {
            if(rocks > 3) return;
            List<Player> enemyPlayers = PlayerManager.instance.players.Where((pl) => pl.playerID != player.playerID && pl.teamID != player.teamID && pl.data.health > 0).ToList();
            if (enemyPlayers.Count == 0) return;
            Player pt = enemyPlayers[UnityEngine.Random.Range(0, enemyPlayers.Count)];
            var aimVector = pt.data.aimDirection.normalized;
            var dist = 10;
            var ballPos = pt.transform.position-aimVector.normalized*dist;
            var betterBall = PhotonNetwork.Instantiate("KFC_Boulder", ballPos, Quaternion.identity);
            betterBall.GetComponent<Rigidbody2D>().velocity = aimVector*200f;
            rocks++;
            KFC.instance.ExecuteAfterSeconds(5f, () =>
            {
                rocks--;
                PhotonNetwork.Destroy(betterBall);
            });
        }
        public override void OnBlock(BlockTrigger.BlockTriggerType trigger)
        {
            SpawnBall();
        }
    }
    public class BallObject : MonoBehaviour
    {
        void Awake()
        {
            transform.localRotation = Quaternion.Euler(new Vector3(90, 0, 0));
        }
    }
}