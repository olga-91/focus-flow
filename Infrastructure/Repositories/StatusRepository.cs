using Domain.Model;
using Domain.Repositories;

namespace Infrastructure.Repositories;

public class StatusRepository(
    FocusFlowContext context)
    : Repository<Status>(context), IStatusRepository
{
}