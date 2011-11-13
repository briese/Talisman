using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Talisman.Components;
using System.IO;

namespace Talisman
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Board _board;
		Dictionary<string, Character> _characterPool;  // these are the characters which can be chosen at the start, or when you need a new hero. Key = name.

		public Game()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();

			this._board = new Board();
			
			LoadStartingCharacters();
			LoadBoard();
			this.RunTextGame();
			this.Exit();  // no GUI stuff yet, all console
		}

		/// <summary>
		/// Read in all starting characters from file
		/// </summary>
		private void LoadStartingCharacters()
		{
			string filepath = @"C:\Games\Talisman\Components\Characters.txt";
			FileStream file = File.Open(filepath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
			StreamReader reader = new StreamReader(file);
			string fileLine = reader.ReadLine();
			Character current = null;
			_characterPool = new Dictionary<string, Character>();
			uint uintValue;
			string key, value;
			bool isUint;

			while(!reader.EndOfStream)
			{
				if (fileLine == "Character")
				{
					if (current != null)
					{
						_characterPool.Add(current.Name, current);
					}
					current = new Character();
				}
				else if (fileLine != "")
				{
					key = fileLine.Substring(0, fileLine.IndexOf("|"));
					value = fileLine.Substring(fileLine.IndexOf("|")+1);
					isUint = uint.TryParse(value, out uintValue);

					if (key == "Name") { current.Name = value; }
					else if (key == "BaseStrength") { current.BaseStrength = uintValue; }
					else if (key == "BaseCraft") { current.BaseCraft = uintValue; }
					else if (key == "BaseLife")
					{
						current.BaseLife = uintValue;
						current.Life = uintValue;
					}
					else if (key == "BaseFate")
					{
						current.BaseFate = uintValue;
						current.Fate = uintValue;
					}
					else if (key == "StartingSpace") { current.StartingSpace = value; }
					else if (key == "BaseAlignment")
					{
						if (value == "Good") { current.BaseAlignment = Alignment.Good; }
						else if (value == "Neutral") { current.BaseAlignment = Alignment.Neutral; }
						else if (value == "Evil") { current.BaseAlignment = Alignment.Evil; }
						current.Alignment = current.BaseAlignment;
					}
					else if (key == "Special")
					{
						// TODO: handle specials
					}
				}

				fileLine = reader.ReadLine();  // next line
			}

			// add last character
			if (current != null)
			{
				_characterPool.Add(current.Name, current);
			}

			reader.Close();
			file.Close();
		}

		/// <summary>
		/// Read in all spaces from file
		/// </summary>
		private void LoadBoard()
		{
			string filepath = @"C:\Games\Talisman\Components\Spaces.txt";
			FileStream file = File.Open(filepath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
			StreamReader reader = new StreamReader(file);
			string fileLine = reader.ReadLine();
			Space current = null;
			uint uintValue;
			string key, value;
			bool isUint;

			while (!reader.EndOfStream)
			{
				if (fileLine == "Space")
				{
					if (current != null)
					{
						if (current.PrimaryText == null)
						{
							current.PrimaryText = "Draw 1 card";
						}
						if (current.SecondaryText == null)
						{
							current.SecondaryText = "Do not draw a card if there is already one in the space.";
						}

						_board.Spaces.Add(current.ID, current);
					}


					current = new Space();
				}
				else if (fileLine != "")
				{
					key = fileLine.Substring(0, fileLine.IndexOf("|"));
					value = fileLine.Substring(fileLine.IndexOf("|") + 1);
					isUint = uint.TryParse(value, out uintValue);

					if (key == "ID") { current.ID = value; }
					else if (key == "Title") { current.Title = value; }
					else if (key == "Region") { current.Region = value; }
					else if (key == "PrimaryText") { current.PrimaryText = value; }
					else if (key == "SecondaryText") { current.SecondaryText = value; }
					else if (key == "Left") { current.Left = value; }
					else if (key == "Right") { current.Right = value; }
					else if (key == "Across") { current.Across = value; }
				}

				fileLine = reader.ReadLine();  // next line
			}

			// add last space
			if (current != null)
			{
				if (current.PrimaryText == null)
				{
					current.PrimaryText = "Draw 1 card";
				}
				if (current.SecondaryText == null)
				{
					current.SecondaryText = "Do not draw a card if there is already one in the space.";
				}

				_board.Spaces.Add(current.ID, current);
			}

			reader.Close();
			file.Close();
		}

		/// <summary>
		/// Test component loading, mostly
		/// </summary>
		private void RunTextGame()
		{
			string input;
			uint uintInput;

			do
			{
				Console.Out.WriteLine();
				Console.Out.WriteLine("* Main Menu *");
				Console.Out.WriteLine("1. Dice");
				Console.Out.WriteLine("2. Character");
				Console.Out.WriteLine("3. Board");
				Console.Out.Write(":");
				input = Console.In.ReadLine();
				if (uint.TryParse(input, out uintInput))
				{
					if (uintInput == 1)
					{
						TestDice();
					}
					else if (uintInput == 2)
					{
						TestCharacter();
					}
					else if (uintInput == 3)
					{
						TestBoard();
					}
				}
			} while (input != "quit" && input != "");
			
		}

		private void TestDice()
		{
			string input;
			uint uintInput;
			uint i;

			Console.Out.WriteLine();
			Console.Out.WriteLine("* Main Menu > Test Dice *");
			do
			{
				Console.Out.WriteLine();
				Console.Out.Write("Roll dice:");
				input = Console.In.ReadLine();
				if (uint.TryParse(input, out uintInput))
				{
					Console.Out.WriteLine(Dice.Roll(uintInput));
					for (i = 0; i < uintInput; i++)
					{
						Console.Out.WriteLine("Dice #" + i + ": " + Dice.Die(i));
					}
				}
			} while (input != "quit" && input != "");
		}

		private void TestCharacter()
		{
			string input;
			Character character;

			Console.Out.WriteLine();
			Console.Out.WriteLine("* Main Menu > Test Characters *");
			do
			{
				Console.Out.WriteLine();
				Console.Out.Write("View Character:");
				input = Console.In.ReadLine();
				if (_characterPool.ContainsKey(input))
				{
					character = _characterPool[input];
					Console.Out.WriteLine("Name:" + character.Name);
					Console.Out.WriteLine("Life:" + character.Life.ToString());
					Console.Out.WriteLine("BaseLife:" + character.BaseLife.ToString());
					Console.Out.WriteLine("Strength:" + character.Strength.ToString());
					Console.Out.WriteLine("BaseStrength:" + character.BaseStrength.ToString());
					Console.Out.WriteLine("Craft:" + character.Craft.ToString());
					Console.Out.WriteLine("BaseCraft:" + character.BaseCraft.ToString());
					Console.Out.WriteLine("Fate:" + character.Fate.ToString());
					Console.Out.WriteLine("BaseFate:" + character.BaseFate.ToString());
					//Console.Out.WriteLine("Space:" + character.Space.ToString());
					Console.Out.WriteLine("StartingSpace:" + character.StartingSpace.ToString());
					Console.Out.WriteLine("Alignment:" + character.Alignment.ToString());
					Console.Out.WriteLine("BaseAlignment:" + character.BaseAlignment.ToString());
				}
			} while (input != "quit" && input != "");
		}

		private void TestBoard()
		{
			string input;
			int intInput;
			Space space;
			string nextId;
			List<string> movementChoices;

			Console.Out.WriteLine();
			Console.Out.WriteLine("* Main Menu > Test Board *");
			space = _board.Spaces["Fields1"];
			do
			{
				Console.Out.WriteLine();
				Console.Out.WriteLine("Current Space:" + space.Title + " [" + space.ID + "]");
				Console.Out.Write("Number of spaces:");
				input = Console.In.ReadLine();
				if (int.TryParse(input, out intInput))
				{
					movementChoices = _board.MoveChoices(space, intInput);
					for (int i = 0; i < movementChoices.Count; i++)
					{
						Console.Out.WriteLine("  " + i.ToString() + ". " + movementChoices[i]);
					}
					Console.Out.Write("Move to #:");
					input = Console.In.ReadLine();
					if (int.TryParse(input, out intInput))
					{
						nextId = movementChoices[intInput];
						space = _board.Spaces[nextId];
					}
				}
				
			} while (input != "quit" && input != "");
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// TODO: Add your update logic here
			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here

			base.Draw(gameTime);
		}
	}
}
