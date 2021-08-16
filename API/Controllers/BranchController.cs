using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Branches;
using Domain;
using MediatR;
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

        [HttpPost]
        public async Task<IActionResult> CreateBranch(Branch branch)
        {
            return HandleResponse(await Mediator.Send(new CreateBranch.Command{Branch = branch}));
        }

        [HttpPut]
        public async Task<IActionResult> EditBranch(Branch branch)
        {
            return HandleResponse(await Mediator.Send(new UpdateBranch.Command{Branch = branch}));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBranch(int id)
        {
            return HandleResponse(await Mediator.Send(new DeleteBranch.Command{Id = id}));
        }

        [HttpGet]
        public async Task<IActionResult> GetBranches()
        {
            return HandleResponse(await Mediator.Send(new GetBranches.Query()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBranch(int id)
        {            
            return HandleResponse(await Mediator.Send(new GetBranch.Query{Id = id}));
        }


    }
}