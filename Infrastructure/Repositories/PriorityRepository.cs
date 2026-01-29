using Domain.Model;
using Domain.Repositories;

namespace Infrastructure.Repositories;

public class PriorityRepository(
    FocusFlowContext context)
    : Repository<Priority>(context), IPriorityRepository
{
}