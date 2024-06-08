using DataAccessLayer.Data;
using DataAccessLayer.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Features.Queries
{
    public class GetEmployeeQuery : IRequest<List<Employee>?>
    {
    }

    public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, List<Employee>?>
    {
        private readonly AppDbContext _context;
        public GetEmployeeQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>?> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users.ToListAsync(cancellationToken);
        }
    }
}
