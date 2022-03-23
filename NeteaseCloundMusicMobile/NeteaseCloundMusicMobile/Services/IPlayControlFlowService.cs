using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NeteaseCloundMusicMobile.Client.Models;

namespace NeteaseCloundMusicMobile.Client.Services
{
    /// <summary>
    /// 表示播放流程控制
    /// </summary>
    public interface IPlayControlFlowService
    {
        /// <summary>
        /// 当成功切换到下一首的时候执行
        /// </summary>
        event EventHandler SuccessfulNextExecute;


        /// <summary>
        /// 用以表示是否加载中
        /// </summary>
        bool Loading { get; }

        /// <summary>
        /// 播放模式
        /// </summary>
        IPlayMode PlayMode { get; }
        /// <summary>
        /// 支持上一个
        /// </summary>
        bool SupportPrev { get; }

        /// <summary>
        /// 支持的播放模式
        /// </summary>
        IReadOnlyList<IPlayMode> SupportPlayModes { get; }
        /// <summary>
        /// 当前播放的对象
        /// </summary>
        PlayableItemBase CurrentPlayableItem { get; }
        /// <summary>
        /// 音乐播放服务
        /// </summary>
        AudioPlayService AudioPlayService { get; }
        /// <summary>
        /// 支持显示播放的项
        /// </summary>
        bool SupportShowTracks { get; }
        /// <summary>
        /// 对应当前播放的项
        /// </summary>
        List<PlayableItemBase> Tracks { get; }

        /// <summary>
        /// 是否支持直接将可播放对象添加到播放列表中，如果为false，则Add2PlaySequenceAsync和AddRange2PlaySequenceAsync将不可用
        /// </summary>
        bool SupportPlayTrack { get; }
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
        /// <summary>
        /// 置顶播放
        /// </summary>
        /// <param name="playableItem"></param>
        /// <returns></returns>
        Task<bool> JumpTheQueueAsync(PlayableItemBase playableItem);
        /// <summary>
        /// 切换播放模式
        /// </summary>
        void ChangePlayModel();
        /// <summary>
        /// 下一个
        /// </summary>
        /// <returns></returns>
        Task<bool> NextAsync();
        /// <summary>
        /// 上一个
        /// </summary>
        /// <returns></returns>
        Task<bool> PrevAsync();
    }


    /// <summary>
    /// 表示一系列播放模式
    /// </summary>
    public interface IPlayMode
    {
        /// <summary>
        /// 该播放模式是否支持插队播放
        /// </summary>
        bool SupportJumpTheQueue => false;
        /// <summary>
        /// 表示显示顺序
        /// </summary>
        int Sort => 0;
        /// <summary>
        /// 表示播放模式的名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 表示模式的图标类名
        /// </summary>
        string IconClass { get; }
        int? OnNext(int index, int count);
        int? OnPrev(int index, int count);

    }
}
