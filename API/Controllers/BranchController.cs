using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    public class BranchController : BaseApiController
    {
        private readonly DataContext _dbContext;
        public BranchController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Branch>>> GetBranches()
        {
            return await _dbContext.Branches.ToListAsync();
        }
    }
}