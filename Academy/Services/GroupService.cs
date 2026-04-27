using Academy.Context;
using Academy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Services;

public class GroupService(AcademyDbContext _context)
{
    //private readonly AcademyDbContext _context;
    //public GroupService(AcademyDbContext context)
    //{
    //    _context = context; 
    //}
    public async Task<Group> AddGroup(string name, int rating)
    {
        Group group = new Group()
        {
            Name = name,
            Rating = rating
        };
        await _context.AddAsync(group);
        await _context.SaveChangesAsync();
        return group;
    }
}
