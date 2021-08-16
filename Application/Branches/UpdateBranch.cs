using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Branches
{
    public class UpdateBranch
    {
        public class Command : IRequestWrapper<Unit>
        {
            public Branch Branch { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Branch).SetValidator(new BranchValidator());
            }
        }
        
        public class Handler : IHandlerWrapper<Command, Unit>
        {
            private readonly DataContext _dbContext;
            private readonly IMapper _mapper;
            public Handler(DataContext dbContext, IMapper mapper)
            {
                _mapper = mapper;
                _dbContext = dbContext;
            }

            public async Task<Response<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var branch = await _dbContext.Branches.FindAsync(request.Branch.Id);
                
                 _mapper.Map(request.Branch, branch);

                var result = await _dbContext.SaveChangesAsync() > 0;

                if (result)
                    return Response<Unit>.MakeResponse(true, "Branch Updated", 200);
                
                return Response<Unit>.MakeResponse(false, "There is problem with update branch", 400);

            }
        }
    }
}