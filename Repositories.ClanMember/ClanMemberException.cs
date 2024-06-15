using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ClanMember
{
    public class ClanMemberException : Exception
    {
        public ClanMemberException()
        {
            
        }

        public ClanMemberException(string message) : base(message) 
        {
            
        }
    }
}
