namespace Imperit.Dynamics.Actions
{
	public class Earnings : IAction
	{
		public (IAction[], State.Player) Perform(State.Player player, State.Player active, State.IProvinces provinces)
		{
			return (new[] { this }, player == active ? player.Earn() : player);
		}
		public byte Priority => 10;
	}
}