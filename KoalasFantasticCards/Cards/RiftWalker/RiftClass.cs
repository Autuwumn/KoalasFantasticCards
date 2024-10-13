using ClassesManagerReborn;
using System.Collections;

namespace KFC.Cards 
{
    class RiftClass : ClassHandler
    {
        internal static string name = "Walker";

        public override IEnumerator Init()
        {
            while (!(RiftWalker.card && RiftGun.card && RiftBody.card && RiftMind.card)) yield return null;
            ClassesRegistry.Register(RiftWalker.card, CardType.Entry, 1);
            ClassesRegistry.Register(RiftGun.card, CardType.Card, RiftWalker.card, 1);
            ClassesRegistry.Register(RiftBody.card, CardType.Card, RiftWalker.card, 1);
            ClassesRegistry.Register(RiftMind.card, CardType.Card, RiftWalker.card, 1);
        }
    }
}
