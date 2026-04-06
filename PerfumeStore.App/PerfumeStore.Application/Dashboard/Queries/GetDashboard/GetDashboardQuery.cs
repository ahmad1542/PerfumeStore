using MediatR;
using PerfumeStore.Application.Dashboard.Dtos;

namespace PerfumeStore.Application.Dashboard.Queries.GetDashboard {
    public class GetDashboardQuery : IRequest<DashboardDto> {
    }
}
