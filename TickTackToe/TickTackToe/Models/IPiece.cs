using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickTackToe.Models
{
    public interface IPiece<T>
    {
        T Content { get;}
    }
}
