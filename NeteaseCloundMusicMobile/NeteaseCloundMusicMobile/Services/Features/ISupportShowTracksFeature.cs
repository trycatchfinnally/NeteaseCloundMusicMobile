using System.Collections.Generic;
using NeteaseCloundMusicMobile.Client.Models;

namespace NeteaseCloundMusicMobile.Client.Services.Features
{
    /// <summary>
    /// 标识支持显示播放列表
    /// </summary>
    public interface ISupportShowTracksFeature
    {
        /// <summary>
        /// 对应当前播放的项
        /// </summary>
        List<PlayableItemBase> Tracks { get; }
    }
}
