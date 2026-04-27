using Academy.Entities;
using Academy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy
{
    class Academy(GroupService _service)
    {
        public async Task<bool> AddGroup(string groupName, int rating)
        {
            try
            {
                await _service.AddGroup(groupName, rating);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
      
    }
}
