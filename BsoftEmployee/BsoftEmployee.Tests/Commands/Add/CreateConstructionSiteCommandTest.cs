using AutoMapper;
using BusinessLogicLayer.Features.Commands.Add;
using DataAccessLayer.Data;
using DataAccessLayer.DTOs;
using DataAccessLayer.Enums;
using DataAccessLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BsoftEmployee.Tests.Commands.Add
{
    public class CreateConstructionSiteCommandTest : TestBase
    {
        private readonly IMapper _mapper;

        public CreateConstructionSiteCommandTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateConstructionSiteDTO, ConstructionSite>();
                cfg.CreateMap<ConstructionSite, ConstructionSiteDTO>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task Handle_Should_Create_New_ConstructionSite()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                // Arrange
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var handler = new CreateConstructionSiteCommandHandler(context, _mapper);
                var command = new CreateConstructionSiteCommand
                {
                    ConstructionSite = new CreateConstructionSiteDTO
                    {
                        Name = "New Site",
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddMonths(1),
                        Status = ConstructionSiteStatus.InProgress
                    }
                };

                // Act
                var result = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(command.ConstructionSite.Name, result.Name);
                var createdSite = await context.ConstructionSites.FirstOrDefaultAsync(cs => cs.Name == command.ConstructionSite.Name);
                Assert.NotNull(createdSite);
                Assert.Equal(command.ConstructionSite.Name, createdSite.Name);
            }
        }

        [Fact]
        public async Task Handle_Should_Return_Error_For_Invalid_Input()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                // Arrange
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var handler = new CreateConstructionSiteCommandHandler(context, _mapper);
                var command = new CreateConstructionSiteCommand
                {
                    ConstructionSite = new CreateConstructionSiteDTO
                    {
                        Name = null, // Invalid input
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddMonths(1),
                        Status = ConstructionSiteStatus.InProgress
                    }
                };

                // Act
                async Task Act() => await handler.Handle(command, CancellationToken.None);

                // Assert
                var exception = await Assert.ThrowsAsync<ArgumentException>(Act);
                Assert.Equal("Construction site name cannot be null or empty.", exception.Message);
            }
        }
    }
}
