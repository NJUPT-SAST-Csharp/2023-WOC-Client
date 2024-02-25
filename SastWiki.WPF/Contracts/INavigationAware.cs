namespace SastWiki.WPF.Contracts;

public interface INavigationAware
{
    /// <summary>
    /// 当即将跳转到该页面时，<see cref="INavigationService"/>会触发该方法并传递导航时的参数。
    /// 这个方法会在页面初始化前与在Frame中跳转前被调用，此时页面上某些元素可能没有完全初始化，请谨慎操作。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parameters">
    /// 传递进来的参数，使用模式匹配来获取你需要的类型
    /// </param>
    /// <returns>操作执行成功返回个true，否则跳转终止</returns>
    public Task<bool> OnNavigatedTo<T>(T parameters);

    /// <summary>
    /// 当即将跳转至另一页面时触发。
    /// </summary>
    /// <returns>操作执行成功返回个true，否则跳转终止</returns>
    public Task<bool> OnNavigatedFrom();
}
