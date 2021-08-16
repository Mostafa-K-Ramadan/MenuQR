using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Branches
{
    public class GetBranches
    {
        public class Query : IRequestWrapper<List<Branch>>
        {
        }

        public class Handler : IHandlerWrapper<Query, List<Branch>>
        {
            private readonly DataContext _dbContext;
            public Handler(DataContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<Response<List<Branch>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var branches = await _dbContext.Branches.ToListAsync();

                if (branches.Count == 0)
                    return Response<List<Branch>>.MakeResponse(true, "No Branches in System", 204);

                return Response<List<Branch>>.MakeResponse(true, "Branches List",branches, 200);

            }
        }
    }
}