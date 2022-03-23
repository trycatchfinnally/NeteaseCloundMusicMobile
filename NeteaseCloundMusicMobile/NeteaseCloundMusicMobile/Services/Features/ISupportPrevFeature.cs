using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Services.Features
{
    /// <summary>
    /// 实现此接口表示有上一个功能
    /// </summary>
    public interface ISupportPrevFeature
    {
        /// <summary>
        /// 上一个
        /// </summary>
        /// <returns></returns>
        Task<bool> PrevAsync();
    }
}
