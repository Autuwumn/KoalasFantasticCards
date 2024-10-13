using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus;
using UnboundLib;
using KFC.MonoBehaviors;
using KFC.Cards;
using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnboundLib.GameModes;
using WillsWackyManagers.Utils;
using UnityEngine.Experimental.PlayerLoop;

namespace KFC.Cards
{
    public class Shadow : CustomEffectCard<shadowMono>
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "Peephole",
            Description = "When you do anything, become shrouded in darkness",
            ModName = KFC.CurseInt,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_Shadow"),
            Rarity = RarityUtils.GetRarity("Legendary"),
            Theme = CardThemeColor.CardThemeColorType.EvilPurple,
            OwnerOnly = true
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { CurseManager.instance.curseCategory };
        }
    }
}

namespace KFC.MonoBehaviors
{
    public class shadowMono : CardEffect
    {
        private GameObject coverPrefab;
        private GameObject cover;

        private void Start()
        {
            coverPrefab = KFC.ArtAssets.LoadAsset<GameObject>("ShadowCover");
            cover = Instantiate(coverPrefab, player.transform);
            cover.transform.localPosition = Vector3.zero;
            cover.GetComponent<SpriteRenderer>().sortingLayerID = -1160774667;
            foreach(var c in cover.GetComponentsInChildren<SpriteRenderer>())
            {
                c.sortingLayerID = -1160774667;
            }
        }
        private void Update()
        {
            if (cover != null)
            {
                if (Input.anyKey)
                {
                    cover.SetActive(true);
                }
                else
                {
                    cover.SetActive(false);
                }
            }
        }
    }
}