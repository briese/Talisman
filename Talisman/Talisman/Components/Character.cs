using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Talisman.Components
{
	public sealed class Character
	{
		public string Name { get; set; }
		public uint Life { get; set; }
		public uint BaseLife { get; set; }

		public uint Strength { get { return BaseStrength + BonusStrength + ItemStrength + MiscStrength; } }
		public uint BaseStrength { get; set; }
		public uint BonusStrength { get; set; }
		public uint ItemStrength { get; set; }
		public uint MiscStrength { get; set; }

		public uint Craft { get { return BaseCraft + BonusCraft + ItemCraft + MiscCraft; } }
		public uint BaseCraft { get; set; }
		public uint BonusCraft { get; set; }
		public uint ItemCraft { get; set; }
		public uint MiscCraft { get; set; }

		public uint Gold { get; set; }
		public uint Fate { get; set; }
		public uint BaseFate { get; set; }

		public string Space { get; set; }
		public string StartingSpace { get; set; }
		public Alignment Alignment { get; set; }
		public Alignment BaseAlignment { get; set; }
		
		public Character() { }
	}

	public enum Alignment
	{
		Good,
		Neutral,
		Evil
	}
}
