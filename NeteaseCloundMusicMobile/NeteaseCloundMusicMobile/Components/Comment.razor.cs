using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using NeteaseCloundMusicMobile.Client.Models;
using NeteaseCloundMusicMobile.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Components
{
    public partial class Comment
    {

        private class ReplyRenderFragmentParameter
        {
            public CommentsItem Item { get; set; }

            public EventCallback<MouseEventArgs> LikeOrNotEventCallBack { get; set; }
        }
        private class NewCommentQuery
        {
            public long id { get; set; }
            public int type { get; set; }
            public int pageNo { get; set; } = 1;
            public int pageSize { get; set; } = 30;
            public long? sortType { get; set; }
            public string cursor { get; set; }


            public bool Loading { get; set; }
        }
        private class HotCommentQuery
        {
            public long id { get; set; }
            public int type { get; set; }
            public int limit { get; set; } = 20;
            public int offset { get; set; }
            public int PageIndex
            {
                get
                {
                    return offset / limit + 1;
                }
                set
                {
                    offset = value * limit - limit;
                }
            }
        }
        private static readonly Dictionary<string, int> m_comment_types = new Dictionary<string, int> { { "MV",     1 }, { "AL", 3 }, { "PL", 2 },{ "SO",0} };
        private HotCommentQuery _hotCommentQuery;
        private NewCommentQuery _newCommentQuery;

        private HotCommentApiResultModel _hotCommentApiResult;

        private NewCommentData _newCommentData;

        [Parameter]

        public string CommentThreadId { get; set; }
        [Inject]
        private LikedProgressService LikedProgressService { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            var commentArray = this.CommentThreadId?.Split("_");
            if (commentArray?.Length != 4) return;
            if (!m_comment_types.TryGetValue(commentArray[1], out var commentType)) return;
            if (!long.TryParse(commentArray.Last(), out var commentId)) return;

            this._newCommentQuery = new NewCommentQuery { id = commentId, type = commentType };
            this._hotCommentQuery = new HotCommentQuery { id = commentId, type = commentType };
            (_hotCommentApiResult, _newCommentData) = await TaskWhenAllHelper.WhenAllAsync(HttpRequestService.MakePostRequestAsync<HotCommentApiResultModel>("/comment/hot", _hotCommentQuery),
             HttpRequestService.MakePostRequestAsync<NewCommentApiResultModel>("/comment/new", _newCommentQuery).ContinueWith(x => x.Result.data)
             );

        }

        private static string MapCreateTime(long time)
        {
            var dt = time.ToDateTime();
            var dtNow = DateTime.Now;
            var timeSpan = dtNow - dt;
            if (timeSpan.TotalHours <= 1) return $"{Convert.ToInt32(timeSpan.TotalMinutes)}分钟前";
            if (dt.Date == dtNow.Date) return dt.ToString("hh:mm");
            return dt.ToString("G");
        }
        private Task NewNext(int pageIndex)
        {

            this._newCommentQuery.pageNo = pageIndex;
            this._newCommentQuery.cursor = this._newCommentData.cursor;
            return NewCommentLoadingAsync();
        }
        private async Task HotNext(int pageIndex)
        {

            this._hotCommentQuery.PageIndex = pageIndex;
            this._hotCommentApiResult = await HttpRequestService.MakePostRequestAsync<HotCommentApiResultModel>("/comment/hot", _hotCommentQuery);


        }

        private Task OnSortTypeChangedAsync(long? sortType)
        {
            this._newCommentQuery.sortType = sortType;
            this._newCommentQuery.pageNo = 1;
            this._newCommentQuery.cursor = string.Empty;
            return NewCommentLoadingAsync();

        }


        private async Task NewCommentLoadingAsync()
        {
            this._newCommentQuery.Loading = true;
            this._newCommentData = await HttpRequestService.MakePostRequestAsync<NewCommentApiResultModel>("/comment/new", _newCommentQuery).ContinueWith(x => x.Result.data);
            this._newCommentQuery.Loading = false;



        }


        private async Task LikeOrNotAsync(CommentsItem item)
        {
            item.liked = await LikedProgressService.LikedOrNotAsync(item.commentId, !item.liked, CanLikedMediaType.Comment) ? !item.liked : item.liked;

        }
    }
}
