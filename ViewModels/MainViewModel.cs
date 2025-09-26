using System.Collections;
using System.Windows.Input;
using MauiStart.Base;
using MauiStart.Models;

namespace MauiStart.ViewModels;

public class MainViewModel : BaseViewModel
{
    public ICommand SelectItemCommand { get; }

    private string _selectedIndex;

    public string SelectedIndex
    {
        get => _selectedIndex;
        set => SetProperty(ref _selectedIndex, value);
    }
    
    // public override bool CanNavigateBack => false;
    //
    // // Page provider used by the InfiniteCollectionViewComponent (adapt to non-generic IList)
    // public Func<int, int, Task<IList>> LoadPage => async (page, pageSize) => (IList)await GetUsersAsync(page, pageSize);
    
    public MainViewModel()
    {
        SelectItemCommand = new Command<string>((arg) => SelectBox(arg));

    }

    public override async Task OnViewAppearing()
    {
        await base.OnViewAppearing();
    }

    private void SelectBox(string boxNumber)
    {
        SelectedIndex = boxNumber;
    }

    // Simulated paged data source
    // private static async Task<IList<User>> GetUsersAsync(int page, int pageSize)
    // {
    //     await Task.Delay(1500); // simulate network latency
    //
    //     var start = (page - 1) * pageSize;
    //     var total = 100; // pretend there's a total number of users
    //
    //     if (start >= total)
    //         return Array.Empty<User>();
    //
    //     var count = Math.Min(pageSize, total - start);
    //     return Enumerable.Range(start, count)
    //         .Select(i => new User($"User {i + 1}", $"user{i + 1}@example.com"))
    //         .ToList();
    // }
}