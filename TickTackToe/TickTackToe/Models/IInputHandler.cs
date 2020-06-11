using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickTackToe.Models;

namespace TickTacToe.Models
{
    /// <summary>
    /// ゲーム入力のインターフェース
    /// </summary>
    public interface IInputHandler
    {
        Task<(int, int)> GetPoint(TicTacBoard board);
    }
}
