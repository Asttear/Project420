using Microsoft.AspNetCore.Components;

namespace Project420.Client.Shared;

public class InfinitePage : ComponentBase
{
    private bool _loading = false;
    protected bool _hasMore = true;
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

    protected async Task LoadMore<T>(List<T> list, Func<Task<IList<T>?>> load)
    {
        _nextRender = false;
        if (_loading)
        {
            return;
        }
        _loading = true;
        var append = await load();

        _nextRender = false;
        if (append is not null)
        {
            list.AddRange(append);
            _hasMore = true;
            _nextRender = true;
        }
        else if (_hasMore)
        {
            _hasMore = false;
            _nextRender = true;
        }
        _loading = false;
    }
}
