using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client
{
    public static class TaskWhenAllHelper
    {


        public static async Task<(TResult1, TResult2)> WhenAllAsync<TResult1, TResult2>(Task<TResult1> task1, Task<TResult2> task2)
        {
            await Task.WhenAll(task1, task2);
            return (task1.Result, task2.Result);
        }
        public static async Task<(TResult1, TResult2,TResult3)> WhenAllAsync<TResult1, TResult2, TResult3>(Task<TResult1> task1, Task<TResult2> task2, Task<TResult3> task3)
        {
            await Task.WhenAll(task1, task2,task3);
            return (task1.Result, task2.Result, task3.Result);
        }
    }
}
