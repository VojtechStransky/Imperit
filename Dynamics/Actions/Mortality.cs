﻿using Imperit.State;
using System.Linq;

namespace Imperit.Dynamics.Actions
{
	public class Mortality : IAction
	{
		public (IAction[], Player) Perform(Player player, Player active, IProvinces provinces)
		{
			return (new[] { this }, provinces.Any(prov => prov.IsControlledBy(player.Id)) ? player : player.Die());
		}
		public (IAction[], Province) Perform(Province province, Player active)
		{
			if (province is Sea Sea && Sea.Occupied && !Sea.Soldiers.Any)
			{
				return (new[] { this }, Sea.Revolt().Item1);
			}
			if (!province.CanSoldiersSurvive)
			{
				return (new[] { this }, province.Revolt().Item1);
			}
			return (new[] { this }, province);
		}
		public byte Priority => 200;
	}
}