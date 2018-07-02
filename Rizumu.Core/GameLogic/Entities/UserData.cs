using Microsoft.Xna.Framework.Graphics;
using Rizumu.Engine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.GameLogic.Entities
{
	public class UserData
	{
		public string Name;
		public int Xp;
		public int Level => CalculateLevel();
		public Texture2D Picture; // I should add an easter egg with a profile pic that's just big ass anime tiddies
		// TODO: Achievements?

		private int CalculateLevel()
		{
			// TODO: increase level by different amounts of XP
			return Xp / 1000;
		}
	}
}
