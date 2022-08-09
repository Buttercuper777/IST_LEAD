using System.Collections.Generic;

namespace IST_LEAD.Core
{
    public interface INewProductBuilder
    {
        public List<string> GetFieldsByLocation(int col);
        
    }
}