using byin_netcore_business.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace byin_netcore_business.Interfaces
{
    public interface IGitHupRepository
    {
        public Task<List<Repository>> GetAllRepository();
    }
}
