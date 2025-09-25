using System.Collections;
using MauiStart.Base;
using MauiStart.Models;

namespace MauiStart.ViewModels;

public class MainViewModel : BaseViewModel
{
    public override bool CanNavigateBack => false;

    // Page provider used by the InfiniteCollectionViewComponent (adapt to non-generic IList)
    public Func<int, int, Task<IList>> LoadPage => async (page, pageSize) => (IList)await GetUsersAsync(page, pageSize);

    public override async Task OnViewAppearing()
    {
        await base.OnViewAppearing();
    }

    // Simulated paged data source
    private static async Task<IList<User>> GetUsersAsync(int page, int pageSize)
    {
        await Task.Delay(1500); // simulate network latency

        var start = (page - 1) * pageSize;
        var total = 100; // pretend there's a total number of users

        if (start >= total)
            return Array.Empty<User>();

        var count = Math.Min(pageSize, total - start);
        return Enumerable.Range(start, count)
            .Select(i => new User($"User {i + 1}", $"user{i + 1}@example.com"))
            .ToList();
    }
}