using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTask
{
    public class MoveCommand : ICommand
    {
        private readonly Game _game;
        private readonly string _direction;
        private readonly string _target;

        public MoveCommand(Game game, string direction, string target)
        {
            _game = game;
            _direction = direction;
            _target = target;
        }

        public void Execute()
        {
            if (_target == "player")
            {
                _game.MovePlayer(_direction);
            }
            else if (_target == "attack")
            {
                _game.MoveAttack(_direction);
            }
        }
    }
}