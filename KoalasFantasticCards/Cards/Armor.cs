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

namespace KFC.Cards
{
    public class Armor : CustomEffectCard<armor_mono>
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "Armor",
            Description = "Gain a 100 hp armor that is seperate from your main hp",
            ModName = KFC.ModInitials,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_Armor"),
            Rarity = CardInfo.Rarity.Rare,
            Theme = CardThemeColor.CardThemeColorType.FirepowerYellow
        };
    }
}

namespace KFC.MonoBehaviors
{
    public class armor_mono : CardEffect
    {
        private float armorHp = 0f;
        private bool armorDestroyed = true;
        //private CustomHealthBar healthBar;

        private void ResetHealthBar()
        {
            //healthBar = player.transform.GetChild(2).gameObject.GetOrAddComponent<CustomHealthBar>();
            //healthBar.enabled = true;
            armorHp = 0f;
            foreach (var c in player.data.currentCards)
            {
                if (c == Armor.card)
                {
                    armorHp += 100f;
                }
            }
            //healthBar.SetValues(armorHp, 100f);
            //healthBar.SetColor(Color.gray);
            armorDestroyed = false;

        }
        protected override void Start()
        {
            ResetHealthBar();
            base.Start();
        }

        public override IEnumerator OnPointStart(IGameModeHandler gameModeHandler)
        {
            ResetHealthBar();
            yield break;
        }
        public override void OnRevive()
        {
            ResetHealthBar();
            base.OnRevive();
        }
        public override void OnTakeDamage(Vector2 damage, bool selfDamage)
        {
            var dmgTaken = damage.magnitude;
            if(!armorDestroyed)
            {
                armorHp -= dmgTaken;
                //healthBar.SetValues(armorHp, 100f);
                player.data.health += dmgTaken;
                if(armorHp <= 0f)
                {
                    //Destroy(healthBar.gameObject);
                    armorDestroyed = true;
                }
            }
            base.OnTakeDamage(damage, selfDamage);
        }
    }
}