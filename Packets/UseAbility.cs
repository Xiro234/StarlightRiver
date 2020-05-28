using NetEasy;
using StarlightRiver.Abilities;
using System;
using System.Linq;
using Terraria;

namespace StarlightRiver.Packets
{
    [Serializable]
    public class UseAbility : Module
    {
        public UseAbility() { }

        public UseAbility(int fromWho, Ability ability)
        {
            this.fromWho = fromWho;
            (abActive, abTimer) = (ability.Active, ability.Timer);
            abType = ability.GetType();
        }

        private readonly int fromWho;
        private readonly bool abActive;
        private readonly int abTimer;
        private readonly Type abType;

        protected override void Receive()
        {
            // TODO: Scalie, review this method please.
            AbilityHandler mp = Main.player[fromWho].GetModPlayer<AbilityHandler>();
            Ability ab = mp.Abilities.Single(a => a.GetType() == abType);

            ab.OnCast();
            (ab.Active, ab.Timer) = (abActive, abTimer);
        }

        protected override bool PreSend(Node? ignoreClient, Node? toClient)
        {
            if (abType == null)
            {
                throw new ArgumentException("Specify the ability to sync.");
            }
            return base.PreSend(ignoreClient, toClient);
        }
    }
}