using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using MediatR;
using Persistence;

namespace Application.Branches
{
    public class DeleteBranch
    {
        public class Command : IRequestWrapper<Unit>
        {
            public int Id { get; set; }
        }

        public class Handler : IHandlerWrapper<Command, Unit>
        {
            private readonly DataContext _dbContext;
            public Handler(DataContext dbContext)
            {
                _dbContext = dbContext;

            }

            public async Task<Response<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var branch = await _dbContext.Branches.FindAsync(request.Id);

                if (branch is null)
                    return Response<Unit>.MakeResponse(true, "There is no branch with this id: "+ request.Id, 404);

                _dbContext.Remove(branch);

                var result = await _dbContext.SaveChangesAsync() > 0;

                if (result)
                    return Response<Unit>.MakeResponse(true, "Branch Deleted", 200);
                
                return Response<Unit>.MakeResponse(true, "There is problem with delete the branch", 400);
            }
        }
    }
}