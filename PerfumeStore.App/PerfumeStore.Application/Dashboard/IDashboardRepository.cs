using PerfumeStore.Application.Dashboard.Dtos;

namespace PerfumeStore.Application.Dashboard {
    public interface IDashboardRepository {
        Task<DashboardDto> GetAsync();
    }
}
