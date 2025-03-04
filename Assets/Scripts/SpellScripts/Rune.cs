using WeaponsScripts.Modifiers;
using System.Collections.Generic;

namespace SpellScripts
{
    public class Rune
    {
        public string Name { get; set; }
        public string Descrition { get; set; }
        public List<IModifier> Modifiers { get; set; }
        public int ManaCost { get; set; }
        public int RuneDuration { get; set; }

        public Rune()
        {
            Modifiers = new();
        }

        public Rune(
            string Name,
            string Descrition,
            List<IModifier> Modifiers,
            int ManaCost,
            int RuneDuration
        )
        {
            this.Name = Name;
            this.Descrition = Descrition;
            this.Modifiers = Modifiers;
            this.ManaCost = ManaCost;
            this.RuneDuration = RuneDuration;
        }

        public void AddModifier(IModifier Modifier) { Modifiers.Add(Modifier); }
    }
}