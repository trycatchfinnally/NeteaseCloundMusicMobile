using System;
using System.Threading.Tasks;
using NeteaseCloundMusicMobile.Client.Models;

namespace NeteaseCloundMusicMobile.Client.Services
{
    public interface IBasicPlayControlFlowService
    {
        /// <summary>
        /// 当前播放项
        /// </summary>
        PlayableItemBase CurrentPlayableItem { get; }
        /// <summary>
        /// 标识播放服务
        /// </summary>
        AudioPlayService AudioPlayService { get; }
        /// <summary>
        /// 当成功切换到下一首的时候执行
        /// </summary>
        event EventHandler SuccessfulNextExecute;
        /// <summary>
        /// 下一个
        /// </summary>
        /// <returns></returns>
        Task<bool> NextAsync();
    }
}
