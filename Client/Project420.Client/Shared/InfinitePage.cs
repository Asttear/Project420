using Masa.Blazor;
using Microsoft.AspNetCore.Components;

namespace Project420.Client.Shared;

public abstract class InfinitePage<T> : ComponentBase
{
    private bool _hasMore = true;
    protected bool _nextRender = true;

    protected override bool ShouldRender()
    {
        if (_nextRender)
        {
            return base.ShouldRender();
        }
        _nextRender = true;
        return false;
    }

    protected async Task OnLoad(InfiniteScrollLoadEventArgs args)
    {
        _nextRender = false;
        var append = await LoadDataAsync();
        if (append.Any())
        {
            _hasMore = true;
            Items.AddRange(append);
            args.Status = InfiniteScrollLoadStatus.Ok;
            return;
        }
        else if (!_hasMore)
        {
            _nextRender = false;
            args.Status = InfiniteScrollLoadStatus.Empty;
        }
        else
        {
            _hasMore = false;
            args.Status = InfiniteScrollLoadStatus.Empty;
        }
    }

    protected abstract List<T> Items { get; }

    protected abstract Task<IList<T>> LoadDataAsync();
}
