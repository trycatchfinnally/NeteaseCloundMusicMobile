using System.Threading.Tasks;
using NeteaseCloundMusicMobile.Client.Models;

namespace NeteaseCloundMusicMobile.Client.Services.Features
{
    /// <summary>
    /// 是否支持丢垃圾桶
    /// </summary>
    public interface ISupportGarbageFeature
    {
        /// <summary>
        /// 将歌曲添加到垃圾箱使用
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<bool> GarbageTrackAsync(PlayableItemBase item);
    }
}
