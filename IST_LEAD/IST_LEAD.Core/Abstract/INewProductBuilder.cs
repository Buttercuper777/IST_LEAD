using System.Collections.Generic;

namespace IST_LEAD.Core.Abstract
{
    public interface INewProductBuilder
    {
        public List<string> GetFieldsByLocation(int col);
        
    }
}