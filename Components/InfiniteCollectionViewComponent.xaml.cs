using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiStart.Components;

public partial class InfiniteCollectionViewComponent : INotifyPropertyChanged
{
    // Internal state
    private int _page = 0;
    private bool _hasMore = true;
    private bool _initialized;
    
    #region Properties
    
    private ObservableCollection<object> _itemsSource;
    public ObservableCollection<object> ItemsSource
    {
        get => _itemsSource;
        set => SetProperty(ref _itemsSource, value);
    }

    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }
    
    #endregion
    
    #region Bindable Properties
    
    // Bindable: page size (default 20)
    public static readonly BindableProperty PageSizeProperty =
        BindableProperty.Create(
            nameof(PageSize),
            typeof(int),
            typeof(InfiniteCollectionViewComponent),
            20);

    public int PageSize
    {
        get => (int)GetValue(PageSizeProperty);
        set => SetValue(PageSizeProperty, value);
    }

    // Bindable: delegate used to fetch a page (pageIndex, pageSize) -> IList
    public static readonly BindableProperty PageProviderProperty =
        BindableProperty.Create(
            nameof(PageProvider),
            typeof(Func<int, int, Task<IList>>),
            typeof(InfiniteCollectionViewComponent),
            defaultValue: null,
            propertyChanged: OnItemsOrProviderChanged);

    private static void OnItemsOrProviderChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is InfiniteCollectionViewComponent self)
        {
            _ = self.TryInitialLoadAsync();
        }
    }
    
    private async Task TryInitialLoadAsync()
    {
        if (_initialized) return;
        if (PageProvider is null) return;
        await LoadNextPageAsync();
    }

    public Func<int, int, Task<IList>>? PageProvider
    {
        get => (Func<int, int, Task<IList>>?)GetValue(PageProviderProperty);
        set => SetValue(PageProviderProperty, value);
    }

    // Bindable: item template (passed through to inner CollectionView)
    public static readonly BindableProperty ItemTemplateProperty =
        BindableProperty.Create(
            nameof(ItemTemplate),
            typeof(DataTemplate),
            typeof(InfiniteCollectionViewComponent),
            propertyChanged: OnItemTemplateChanged);

    public DataTemplate? ItemTemplate
    {
        get => (DataTemplate?)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }
    
    private static void OnItemTemplateChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is InfiniteCollectionViewComponent self)
        {
            self.List.ItemTemplate = newValue as DataTemplate;
        }
    }
    
    #endregion

    public InfiniteCollectionViewComponent()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }
    
    private async void OnLoaded(object? sender, EventArgs e)
    {
        if (!_initialized)
            await LoadNextPageAsync();
    }
    
    private async void OnRemainingItemsThresholdReached(object? sender, EventArgs e)
    {
        await LoadNextPageAsync();
    }
    
    private async Task LoadNextPageAsync()
    {
        if (IsLoading || !_hasMore) return;
        if (PageProvider is null) return;
        if (ItemsSource is null)
            ItemsSource = new ObservableCollection<object>();

        try
        {
            IsLoading = true;

            var nextPage = _page + 1;
            var batch = await PageProvider(nextPage, PageSize);

            if (batch is null || batch.Count == 0)
            {
                _hasMore = false;
                return;
            }

            foreach (var item in batch)
            {
                ItemsSource.Add(item);
            }

            _page = nextPage;
            _initialized = true;
        }
        finally
        {
            IsLoading = false;
        }
    }
    
    #region INotifyPropertyChanged

    protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value))
            return false;

        backingStore = value;
        onChanged?.Invoke();
        OnPropertyChanged(propertyName);
        return true;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        var changed = PropertyChanged;

        changed?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
}