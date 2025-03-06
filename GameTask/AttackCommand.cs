using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTask
{
    public class AttackCommand : ICommand
    {
        private readonly Game _game;

        public AttackCommand(Game game)
        {
            _game = game;
        }

        public void Execute()
        {
            _game.PerformAttack();
        }
    }
}