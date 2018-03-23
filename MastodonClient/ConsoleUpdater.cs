using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastodonClient
{
    class ConsoleUpdater : IUpdater
    {
        public ConsoleUpdater()
        {

        }

        public async Task Update(Item.Status toot)
        {
            Console.WriteLine(toot);
            return;
        }

        public void Delete(string id)
        {

        }
    }
}
