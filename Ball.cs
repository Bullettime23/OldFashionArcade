using System;
using SFML.Graphics;
using SFML.System;



class Ball
{
    public Sprite Sprite;
    private float speed;
    private Vector2f direction;

    public Ball(Texture texture)
    {
        Sprite = new Sprite(texture);
    }

    public void Start(float speed, Vector2f direction) {
        if (this.speed != 0) return;
        
        this.speed = speed;
        this.direction = direction;        
    }

    public void Stop()
    {
        this.speed = 0;
        this.direction = new Vector2f(0, 0);
    }

    public void Move(Vector2i bordersPre, Vector2i bordersPos) {
        if (this.Sprite.Position.X < bordersPre.X || this.Sprite.Position.X + this.Sprite.TextureRect.Width > bordersPos.X)
        {
            this.direction.X *= -1;
        }

        if (this.Sprite.Position.Y < bordersPre.Y)
        {
            this.direction.Y *= -1;
        }
        Sprite.Position += direction * speed;
    }

    public bool CheckBlockCollision(Sprite sprite) {
        if (this.Sprite.GetGlobalBounds().Intersects(sprite.GetGlobalBounds()))
        {
            this.direction.Y *= -1;
            return true;
        }

        return false;
    }

    public bool CheckStickCollision(Sprite sprite)
    {
        if (this.Sprite.GetGlobalBounds().Intersects(sprite.GetGlobalBounds()))
        {
            this.direction.Y *= -1;
            float f = ((this.Sprite.Position.X + this.Sprite.Texture.Size.X * 0.5f) - (sprite.Position.X + sprite.Texture.Size.X * 0.5f))/sprite.Texture.Size.X;
            this.direction.X = f * 2f;
            return true;
        }

        return false;
    }

    public bool CheckFloorColision(int floorY)
    {
        if(this.Sprite.Position.Y + this.Sprite.TextureRect.Height > floorY)
        {
            return true;
        }
        return false;
    }
}
