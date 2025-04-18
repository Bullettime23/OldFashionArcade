using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

//Код разделён на главный класс и класс мяча +
//Мяч уничтожает блоки при соприкосновении. Платформа отталкивает мяч +
//Метод мяча Move принимает область перемещения +
//Дополнительно:

//Количество попыток на уровень ограничено +
//Есть блоки, которые исчезают с нескольких ударов — 2 балла.
//Количество и расположение блоков рассчитывается на основе номера уровня. Есть возможность пройти уровень — 5 баллов.

class Program
{
    static RenderWindow window;
    static int attemptsLeft = 3;
    static int level = 1;

    static Texture ballTexture;
    static Texture ballSmallTexture;
    static Texture stickTexture;
    static Texture blockTexture;

    static Sprite stick;
    static BlockService blockService;
    static Ball ball;
    static Sprite[] health;

    static Text gameOverMessage;
    static Text winMessage;
    static Font font;
    static void SetStartPosition()
    {
        blockService.SetStartPosition(level);
        stick.Position = new Vector2f(Mouse.GetPosition(window).X - stick.Texture.Size.X * 0.5f / 2, 740);
        ball.Sprite.Position = new Vector2f(stick.Position.X + stick.TextureRect.Width / 2, stick.Position.Y - ball.Sprite.TextureRect.Height - 5);
    }

    static void Main(string[] args)
    {
        window = new RenderWindow(new VideoMode(1000, 800), "Arcade");
        window.Closed += CloseWindow;
        window.SetFramerateLimit(60);

        ballTexture = new Texture("./Ball.png");
        blockTexture = new Texture("./Block.png");
        stickTexture = new Texture("./Stick.png");
        ballSmallTexture = new Texture("./Ball-small.png");
        font = new Font("./Game-fonts.otf");

        gameOverMessage = new Text("\t\t\tИгра окончена!\nНажмите левую кнопку мыши для рестарта", font, 40);
        gameOverMessage.FillColor = Color.Yellow;
        gameOverMessage.Position = new Vector2f(20, window.Size.Y / 3);
        winMessage = new Text(gameOverMessage);
        winMessage.DisplayedString = "\t\t\tВы победили! :D\nНажмите левую кнопку мыши для рестарта";

        ball = new Ball(ballTexture);
        stick = new Sprite(stickTexture);
        health = new Sprite[attemptsLeft];
        for (int i = 0; i < attemptsLeft; i++){
            health[i] = new Sprite(ballSmallTexture);
            health[i].Position = new Vector2f(30 + 32 * i, window.Size.Y - 20);                
        }

        blockService = new BlockService(blockTexture);
        blockService.Init(level);

        SetStartPosition();

        while (window.IsOpen) {
            window.Clear();

            window.DispatchEvents();

            //Тут логика игры
            if (blockService.BlocksOnField == 0)
            {
                level += 1;
                attemptsLeft = 3;
                ball.Stop();
                blockService.Init(level);
                SetStartPosition();
            }

            if (attemptsLeft != 0 && level != 4) {

            stick.Position = new Vector2f(Mouse.GetPosition(window).X - stick.Texture.Size.X * 0.5f, stick.Position.Y);

            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                ball.Start(8, new Vector2f(0, -1));
            }         

            //Collisions
            if(ball.CheckFloorColision((int)window.Size.Y))
            {
                attemptsLeft -= 1;
                ball.Stop();
                SetStartPosition();
            }
            ball.CheckStickCollision(stick);
            for (int i = 0; i < blockService.Blocks.Length; i++)
            {
                if(ball.CheckBlockCollision(blockService.Blocks[i]) ==  true)
                {
                        blockService.Blocks[i].Position = new Vector2f(window.Size.X + 200, window.Size.Y + 200);
                        blockService.BlocksOnField -= 1;
                        break;
                }
            }
            ball.Move(new Vector2i(0, 0), new Vector2i((int)window.Size.X, (int)window.Size.Y));

            // Отрисовка
            window.Draw(stick);
            window.Draw(ball.Sprite);
            for (int i = 0; i < blockService.Blocks.Length; i++) window.Draw(blockService.Blocks[i]);
            for (int i = 0; i < attemptsLeft; i++) window.Draw(health[i]);

            window.Display();

            }

            if (level == 4) //Победа
            {

                window.Draw(winMessage);
                window.Display();

                if (Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    level = 1;
                    blockService.Init(level);
                    SetStartPosition();
                    attemptsLeft = 3;
                }
                
            }

            if (attemptsLeft == 0)//Поражение
            {
                window.Draw(gameOverMessage);
                window.Display();

                if (Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    SetStartPosition();
                    attemptsLeft = 3;
                }
            }
        }


       
    }

    private static void CloseWindow(object sender, EventArgs e)
    {
        window.Close();
    }
}
