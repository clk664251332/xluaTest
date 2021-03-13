using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Core
{
    [Serializable]
    public class PlayerData
    {
        public string name;
        public int level;
    }

    class Player
    {
        PlayerData playerData;
    }
}
