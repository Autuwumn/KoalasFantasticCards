using ClassesManagerReborn;
using System.Collections;

namespace KFC.Cards
{
    class ViruzClass : ClassHandler
    {
        internal static string name = "Viruz";

        public override IEnumerator Init()
        {
            while (!(ViruzCard.card && Ard.card && Bard.card && Ccard.card)) yield return null;
            ClassesRegistry.Register(ViruzCard.card, CardType.Entry, 1);
            ClassesRegistry.Register(Ard.card, CardType.Card, ViruzCard.card, 5);
            ClassesRegistry.Register(Bard.card, CardType.Card, ViruzCard.card, 10);
            ClassesRegistry.Register(Ccard.card, CardType.Card, ViruzCard.card, 5);
        }
    }
}