using System.Collections.Generic;

namespace Talisman.Components
{
	/// <summary>
	/// The playing surface
	/// </summary>
	public sealed class Board
	{
		public Dictionary<string, Space> Spaces { get; set; }  // key = ID

		public Board()
		{
			Spaces = new Dictionary<string, Space>();
		}

		/// <summary>
		/// Build a list of possible places for a character to move
		/// </summary>
		/// <param name="fromSpace">The space to start from</param>
		/// <param name="SpacesToMove">Number of spaces away to find</param>
		/// <returns>A list of possible spaces to move to</returns>
		public List<string> MoveChoices(Space fromSpace, int SpacesToMove)
		{
			List<string> choices = new List<string>();
			string nextId = fromSpace.ID;
			Space toSpace = fromSpace;
			// move left
			for (uint i = 0; i < SpacesToMove; i++)
			{
				nextId = toSpace.Left;
				toSpace = Spaces[nextId];
			}
			choices.Add(nextId);

			// move right
			toSpace = fromSpace;
			for (uint i = 0; i < SpacesToMove; i++)
			{
				nextId = toSpace.Right;
				toSpace = Spaces[nextId];
			}
			choices.Add(nextId);

			return choices;
		}
	}
}
