using System;
using SFML.Graphics;
using SFML.System;

class BlockService
{
    private Texture blockTexture;
    public Sprite[] Blocks;
    public int BlocksOnField = 0;
    public BlockService(Texture texture)
    {
        this.blockTexture = texture;
    }

    public void Init(int level)
    {
        int blocksNumber = 0;
        if (level == 1) blocksNumber = 36;
        if (level == 2) blocksNumber = 72;
        if (level == 3) blocksNumber = 100;
        this.Blocks = new Sprite[blocksNumber];
        for (int i = 0; i < this.Blocks.Length; i++) this.Blocks[i] = new Sprite(this.blockTexture);
    }
    public void SetStartPosition(int level)
    {
        this.BlocksOnField = 0;
        int index = 0;

        if (level == 1)
        {
            for (int y = 0; y < this.Blocks.Length / 6; y++)  // 6 строк
            {
                for (int x = 0; x < this.Blocks.Length / 6; x++) // 6 столбцов
                {
                    this.Blocks[index].Position = new Vector2f(
                        x * (this.Blocks[index].TextureRect.Width + 100) + 100,
                        y * (this.Blocks[index].TextureRect.Height + 40) + 50
                    );
                
                    index++;
                }
            }

            this.BlocksOnField = index;
            return;
        }

        if (level == 2)
        {
            for (int y = 0; y < this.Blocks.Length / 8; y++)  // 6 строк
            {
                for (int x = 0; x < this.Blocks.Length / 9; x++) // 6 столбцов
                {
                    this.Blocks[index].Position = new Vector2f(
                        x * (this.Blocks[index].TextureRect.Width + 65) + 75,
                        y * (this.Blocks[index].TextureRect.Height + 38) + 50
                    );

                    index++;
                }
            }

            this.BlocksOnField = index;
            return;
        }

        if (level == 3) { 
            for (int y = 0; y < this.Blocks.Length / 10; y++)
            {
                for (int x = 0; x < this.Blocks.Length / 10; x++)
                {
                    this.Blocks[index].Position = new Vector2f(
                        x * (this.Blocks[index].Texture.Size.X + 40) + 70,
                        y * (this.Blocks[index].Texture.Size.Y + 20) + 50
                    );

                    index++;
                }
            }
            this.BlocksOnField = index;
            return;
        }

        this.BlocksOnField = 100;
    }

}

