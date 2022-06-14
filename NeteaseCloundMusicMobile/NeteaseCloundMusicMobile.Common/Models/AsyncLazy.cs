using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeteaseCloundMusicMobile.Client.Models
{
    public class AsyncLazy<T>
    {
        private bool _valueCreate;
        private readonly Task<T> _valueCreateMethod;
        
        private T _value;

        public AsyncLazy(Func<Task<T>> valueFactory) : this(valueFactory?.Invoke())
        {

        }
        public AsyncLazy(Task<T> valueCreateMethod)
        {
            this._valueCreateMethod = valueCreateMethod ?? throw new ArgumentNullException(nameof(valueCreateMethod));
        }
        public async Task<T> EnsureValueAsync()
        {
            if (this._valueCreate) return this._value;
            this._value = await this._valueCreateMethod;
            this._valueCreate = true;
            return this._value;
        }
    }
}
