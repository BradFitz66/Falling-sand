using Raylib_cs;

namespace kum
{
	internal class World
	{
		int width, height, resize_factor;
		Tile[,] sandbox;

		public World(int width, int height, int resize_factor)
		{
			this.width = width / resize_factor;
			this.height = height / resize_factor;
			this.resize_factor = resize_factor;

			sandbox = new Tile[width, height];

			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					sandbox[i, j] = new Tile(i, j, this, "air");
				}
			}
		}

		internal Tile Get(int x, int y)
		{
			if (x < 0 || x >= width || y < 0 || y >= height)
			{
				return new Tile(x, y, this, "sand");
			}

			return sandbox[x, y];
		}

		internal void Set(int x, int y, Tile tile)
		{
			if (x < 0 || x >= width || y < 0 || y >= height)
			{
				return;
			}
			sandbox[x, y] = tile;
		}

		internal void Paint(int x, int y, string type, int brush_size)
		{
			//Check if x, y is within bounds of sandbox array
			if (x < 0 || x >= width || y < 0 || y >= height)
			{
				return;
			}

			for (int i = 0; i < brush_size; i++)
			{
				for (int j = 0; j < brush_size; j++)
				{
					sandbox[x + i, y + j] = new Tile(x + i, y + j, this, type);
				}
			}
			
		}

		internal void Draw()
		{
			
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					if (i < 0 || i >= width || j < 0 || j >= height)
					{
						return;
					}

					if (!(sandbox[i, j].type == "air"))
					{
						Raylib.DrawRectangle(i * resize_factor, j * resize_factor, resize_factor, resize_factor, sandbox[i, j].color);
					}
				}
			}
		}

		internal void Update()
		{
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					if (i < 0 || i >= width || j < 0 || j >= height)
					{
						return;
					}
					if (!(sandbox[i, j].type == "air") && !(sandbox[i, j].updated))
					{
						sandbox[i, j].Update();
					}
					else if (sandbox[i, j].updated)
					{
						sandbox[i, j].updated = false;
					}
				}
			}
		}
	}
}