using System.Collections.Generic;

namespace NeteaseCloundMusicMobile.Client.Services.Features
{
    /// <summary>
    /// 实现此接口表示支持切换播放模式
    /// </summary>
    public interface ISupportPlayModeFeature
    {
        /// <summary>
        /// 播放模式
        /// </summary>
        IPlayMode PlayMode { get; }

        /// <summary>
        /// 支持的播放模式
        /// </summary>
        IReadOnlyList<IPlayMode> SupportPlayModes { get; }
        /// <summary>
        /// 切换播放模式
        /// </summary>
        void ChangePlayModel();
    }
}
