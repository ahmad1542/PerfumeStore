using MediatR;
using PerfumeStore.Application.Dashboard.Dtos;

namespace PerfumeStore.Application.Dashboard.Queries.GetDashboard {
    public class GetDashboardQueryHandler(IDashboardRepository dashboardRepository) : IRequestHandler<GetDashboardQuery, DashboardDto> {
        public async Task<DashboardDto> Handle(GetDashboardQuery request, CancellationToken cancellationToken) {
            return await dashboardRepository.GetAsync();
        }
    }
}
