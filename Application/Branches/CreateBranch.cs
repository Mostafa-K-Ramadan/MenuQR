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
    public class CreateBranch
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
            public Handler(DataContext dbContext)
            {

                _dbContext = dbContext;
            }

            public async Task<Response<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {   
                _dbContext.Branches.Add(request.Branch);

                var result = await _dbContext.SaveChangesAsync() > 0;

                if (result)
                    return Response<Unit>.MakeResponse(true, "Branch Created", 201);
                
                return Response<Unit>.MakeResponse(true, "Branch Create Failed", 400);

            }
        }
    }
}