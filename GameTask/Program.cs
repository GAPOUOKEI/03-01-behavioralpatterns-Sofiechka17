namespace GameTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в игру!");
            Console.WriteLine("Управление:");
            Console.WriteLine("Стрелки: движение персонажа (@)");
            Console.WriteLine("W: вверх, A: влево, S: вниз, D: вправо - движение атаки (*)");
            Console.WriteLine("Пробел: атака");
            Console.WriteLine("Атака наносит 20 HP урона, враг наносит 10 HP урона.");
            Console.WriteLine("Цель: убить 3 врагов или проиграть.");
            Console.WriteLine("Нажмите любую клавишу, чтобы начать...");
            Console.ReadKey();

            Game game = new Game();
            Dictionary<ConsoleKey, ICommand> commands = new Dictionary<ConsoleKey, ICommand>
        {
            { ConsoleKey.UpArrow, new MoveCommand(game, "up", "player") },
            { ConsoleKey.DownArrow, new MoveCommand(game, "down", "player") },
            { ConsoleKey.LeftArrow, new MoveCommand(game, "left", "player") },
            { ConsoleKey.RightArrow, new MoveCommand(game, "right", "player") },
            { ConsoleKey.W, new MoveCommand(game, "up", "attack") },
            { ConsoleKey.A, new MoveCommand(game, "left", "attack") },
            { ConsoleKey.S, new MoveCommand(game, "down", "attack") },
            { ConsoleKey.D, new MoveCommand(game, "right", "attack") },
            { ConsoleKey.Spacebar, new AttackCommand(game) }
        };

            while (true)
            {
                game.Draw();

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    if (commands.ContainsKey(key))
                    {
                        commands[key].Execute();
                    }
                    else if (key == ConsoleKey.Escape)
                    {
                        break;
                    }
                }

                if (game.IsGameOver())
                {
                    Console.WriteLine("Хотите попробовать снова? (y/n)");
                    var choice = Console.ReadKey(true).Key;
                    if (choice == ConsoleKey.Y)
                    {
                        game = new Game(); // Перезапуск игры
                    }
                    else
                    {
                        break;
                    }
                }

                System.Threading.Thread.Sleep(100); // Задержка для плавности
            }

            Console.WriteLine("Игра завершена.");
        }
    }
}