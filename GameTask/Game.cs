using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTask
{
    public class Game
    {
        private int _playerX, _playerY;
        private int _attackX, _attackY;
        private int _score;
        private int _health = 100;
        private int _playerDamage = 20;
        private int _enemyHealth = 40;
        private int _enemyDamage = 50;
        private int _enemiesDefeated = 0;
        private readonly Random _random = new Random();
        private readonly List<(int x, int y)> _items = new List<(int x, int y)>();
        private (int x, int y) _enemy;

        public Game()
        {
            _playerX = 10;
            _playerY = 10;
            SpawnEnemy();
            SpawnItem();
        }

        public void MovePlayer(string direction)
        {
            switch (direction)
            {
                case "up": if (_playerY > 1) _playerY--; break;
                case "down": if (_playerY < 18) _playerY++; break;
                case "left": if (_playerX > 1) _playerX--; break;
                case "right": if (_playerX < 18) _playerX++; break;
            }
            CheckCollisions();
        }

        public void MoveAttack(string direction)
        {
            switch (direction)
            {
                case "up": if (_attackY > 1) _attackY--; break;
                case "down": if (_attackY < 18) _attackY++; break;
                case "left": if (_attackX > 1) _attackX--; break;
                case "right": if (_attackX < 18) _attackX++; break;
            }
            CheckAttack();
        }

        public void PerformAttack()
        {
            _attackX = _playerX;
            _attackY = _playerY;
        }

        private void CheckCollisions()
        {
            if (_playerX == _enemy.x && _playerY == _enemy.y)
            {
                _health -= _enemyDamage;
                Console.WriteLine($"Враг атакует! Ваше здоровье: {_health}");
                SpawnEnemy();
            }

            for (int i = 0; i < _items.Count; i++)
            {
                if (_playerX == _items[i].x && _playerY == _items[i].y)
                {
                    _score++;
                    _items.RemoveAt(i);
                    SpawnItem();
                    break;
                }
            }
        }

        private void CheckAttack()
        {
            if (_attackX == _enemy.x && _attackY == _enemy.y)
            {
                _enemyHealth -= _playerDamage;
                Console.WriteLine($"Вы атаковали врага! Его здоровье: {_enemyHealth}");

                if (_enemyHealth <= 0)
                {
                    _enemiesDefeated++;
                    Console.WriteLine("Враг побежден!");
                    SpawnEnemy();
                    _enemyHealth = 40;
                }

                // Задержка на несколько секунд перед продолжением
                System.Threading.Thread.Sleep(500);
            }
        }

        private void SpawnEnemy()
        {
            _enemy = (_random.Next(1, 19), _random.Next(1, 19));
        }

        private void SpawnItem()
        {
            _items.Add((_random.Next(1, 19), _random.Next(1, 19)));
        }

        public void Draw()
        {
            Console.Clear();
            Console.WriteLine($"Счет: {_score}  Здоровье: {_health}");

            for (int y = 0; y < 20; y++)
            {
                for (int x = 0; x < 20; x++)
                {
                    if (x == _playerX && y == _playerY)
                        Console.Write('@');
                    else if (x == _attackX && y == _attackY)
                        Console.Write('*');
                    else if (x == _enemy.x && y == _enemy.y)
                        Console.Write('A');
                    else if (_items.Contains((x, y)))
                        Console.Write('+');
                    else if (x == 0 || x == 19 || y == 0 || y == 19)
                        Console.Write('-');
                    else
                        Console.Write(' ');
                }
                Console.WriteLine();
            }
        }

        public bool IsGameOver()
        {
            if (_health <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Вы проиграли!");
                Console.ForegroundColor = ConsoleColor.White;
                return true;
            }

            if (_enemiesDefeated >= 3)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Вы победили!");
                Console.ForegroundColor = ConsoleColor.White;
                return true;
            }

            return false;
        }
    }
}