using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityChatBot
{
    public static class Utils
    {
        public static string NormalizeInput(string input)
        {
            return input?.Trim().ToLower() ?? "";
        }
    }
}
