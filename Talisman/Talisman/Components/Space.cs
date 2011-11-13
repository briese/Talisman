using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Talisman
{
	/// <summary>
	/// A single board space
	/// </summary>
	public sealed class Space
	{
		public string ID { get; set; }             // unique id
		public string Title { get; set; }          // name of the space
		public string Region { get; set; }         // name of the region this space is in
		public string PrimaryText { get; set; }    // the bold text
		public string SecondaryText { get; set; }  // the help text
		public string Across { get; set; }       // ID of the space across the river. Or null
		public string Left { get; set; }         // ID of the space to the left.
		public string Right { get; set; }        // ID of the space to the right.

		// TODO: objects on this space

		public Space() { }
	}
}
