using AutoMapper;
using DataAccessLayer.Data;
using DataAccessLayer.DTOs;
using DataAccessLayer.Enums;
using DataAccessLayer.Extension;
using DataAccessLayer.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Features.Queries.Get
{
    public class GetConstructionSiteByIdQuery : IRequest<ConstructionSiteDTO?>
    {
        public int GetConstructionSiteId { get; set; }
    }

    public class GetConstructionSiteByIdQueryHandler : IRequestHandler<GetConstructionSiteByIdQuery, ConstructionSiteDTO?>
    {
        private readonly AppDbContext _context;


        public GetConstructionSiteByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ConstructionSiteDTO?> Handle(GetConstructionSiteByIdQuery request, CancellationToken cancellationToken)
        {
            var constructionSiteDTO = await _context
                .ConstructionSites
                .Where(cs => cs.ConstructionSiteId == request.GetConstructionSiteId)
                .Select(cs => new ConstructionSiteDTO()
                {
                    ConstructionSiteId = cs.ConstructionSiteId,
                    Name = cs.Name,
                    StartDate = cs.StartDate.ToLongDateString(),
                    EndDate = cs.EndDate.ToLongDateString(),
                    Status = cs.Status.GetDisplayName(),
                    Employees = cs.ConstructionSiteEmployees
                        .Where(cse => cse.Employee != null)
                        .Select(cse => new EmployeeDTO()
                        {
                            EmployeeId = cse.Employee.Id,
                            Name = cse.Employee.FirstName,
                            Role = cse.Employee.Role.GetDisplayName(),
                        })
                        .ToList(),
                })
                .FirstOrDefaultAsync(cancellationToken);

            return constructionSiteDTO;
        }

    }

}
