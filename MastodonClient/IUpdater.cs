using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastodonClient
{
    public interface IUpdater
    {
        Task Update(Item.Status toot);
        void Delete(string id);
    }
}
