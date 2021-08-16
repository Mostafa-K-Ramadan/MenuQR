using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Domain;
using Persistence;

namespace Application.Branches
{
    public class GetBranch
    {
        public class Query : IRequestWrapper<Branch>
        {
            public int Id { get; set; }
        }

        public class Handler : IHandlerWrapper<Query, Branch>
        {
            private readonly DataContext _dbContext;
            public Handler(DataContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<Response<Branch>> Handle(Query request, CancellationToken cancellationToken)
            {
                var branch = await _dbContext.Branches.FindAsync(request.Id);

                if (branch == null) 
                    return Response<Branch>.MakeResponse(false, "Returning the value unsuccesfully", 404); 

                return Response<Branch>.MakeResponse(true, "Returning the value succesfully", branch, 200);
            }
        }
    }
}