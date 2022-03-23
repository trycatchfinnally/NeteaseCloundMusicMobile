using System.Collections.Generic;
using System.Threading.Tasks;
using NeteaseCloundMusicMobile.Client.Models;

namespace NeteaseCloundMusicMobile.Client.Services.Features
{
    /// <summary>
    /// 是否支持将歌曲手动添加到歌单中
    /// </summary>
    public interface ISupportManualAddTracksFeature
    {
        /// <summary>
        /// 添加到播放列表
        /// </summary>
        /// <param name="playableItem"></param>
        /// <param name="autoPlay"></param>
        /// <param name="clearCollection"></param>
        /// <returns></returns>
        Task Add2PlaySequenceAsync(PlayableItemBase playableItem, bool autoPlay = true, bool clearCollection = false);
        /// <summary>
        /// 添加到播放列表
        /// </summary>
        /// <param name="playableItems"></param>
        /// <param name="autoPlay"></param>
        /// <param name="clearCollection"></param>
        /// <returns></returns>
        Task AddRange2PlaySequenceAsync(IEnumerable<PlayableItemBase> playableItems, bool autoPlay = true,
            bool clearCollection = false);
    }
}
