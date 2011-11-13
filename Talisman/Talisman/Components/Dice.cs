using System;

namespace Talisman.Components
{
	/// <summary>
	/// Handles dice rolling
	/// </summary>
	public static class Dice
	{
		private static uint _total;
		private static uint _numDice;
		private static uint[] _dice;
		private static Random _rnd;

		static Dice()
		{
			_rnd = new Random();
		}

		/// <summary>
		/// Roll a number of d6
		/// </summary>
		/// <param name="NumberOfDice">How many dice to roll</param>
		/// <returns>Total of all dice rolled</returns>
		public static uint Roll(uint NumberOfDice = 1)
		{
			_total = 0;
			_dice = new uint[NumberOfDice];
			_numDice = NumberOfDice;

			for (uint i = 0; i < NumberOfDice; i++)
			{
				_dice[i] = (uint)_rnd.Next(1, 7);
				_total += _dice[i];
			}
			
			return _total;
		}

		/// <summary>
		/// Return the value of a single d6 that was rolled
		/// </summary>
		/// <param name="index">Which die</param>
		/// <returns>Value of a single die</returns>
		public static uint Die(uint index)
		{
			if (index < _numDice) return _dice[index];
			return 0;
		}

	}
}
