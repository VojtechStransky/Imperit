using System.Collections.Generic;
using System.Linq;

namespace Imperit.State
{
    public abstract class Province : IAppearance
    {
        public readonly int Id;
        public readonly string Name;
        public readonly Shape Shape;
        public readonly IArmy Army, DefaultArmy;
        public readonly uint Earnings;
        protected readonly Settings settings;
        public Province(int id, string name, Shape shape, IArmy army, IArmy defaultArmy, uint earnings, Settings set)
        {
            Id = id;
            Name = name;
            Shape = shape;
            Army = army;
            DefaultArmy = defaultArmy;
            Earnings = earnings;
            settings = set;
        }
        public abstract uint CanMoveTo(Province dest);
        protected abstract Province WithArmy(IArmy army);
        public (Province, Dynamics.IAction[]) GiveUpTo(IArmy his_army)
        {
            return (WithArmy(his_army), new[] { Army.Lose(this), his_army.Gain(this) }.NotNull().ToArray());
        }
        public (Province, Dynamics.IAction[]) Revolt() => GiveUpTo(DefaultArmy);
        public virtual Province StartMove(Province dest, IArmy army) => WithArmy(Army.Subtract(army));
        public (Province, Dynamics.IAction[]) AttackedBy(IArmy another) => GiveUpTo(Army.AttackedBy(another));
        public Province ReinforcedBy(IArmy another) => WithArmy(Army.Join(another));
        public bool IsControlledBy(int p) => Army.IsControlledBy(p);
        public bool IsAllyOf(IArmy army) => Army.IsAllyOf(army);
        public bool IsAllyOf(Province prov) => Army.IsAllyOf(prov.Army);
        public uint Soldiers => Army.Soldiers;
        public bool Occupied => !(Army is PeasantArmy);
        public virtual Color Fill => Army.Color;
        public IEnumerator<Point> GetEnumerator() => Shape.GetEnumerator();
        public Point Center => Shape.Center;

        public override bool Equals(object? obj) => obj != null && obj is Province p && p.Id == Id;
        public override int GetHashCode() => Id.GetHashCode();
        public static bool operator ==(Province? a, Province? b) => (a is null && b is null) || (a is object x && x.Equals(b));
        public static bool operator !=(Province? a, Province? b) => ((a is null) != (b is null)) || (a is object x && !x.Equals(b));
    }
}