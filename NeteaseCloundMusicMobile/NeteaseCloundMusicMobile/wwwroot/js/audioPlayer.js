var AudioPlayer = /** @class */ (function () {
    function AudioPlayer() {
        this.initComponent();
    }
    Object.defineProperty(AudioPlayer.prototype, "volumn", {
        get: function () {
            return Math.floor(this._audioElement.volume * 100);
        },
        set: function (value) {
            if (value > 100)
                value = 100;
            this._audioElement.volume = value / 100;
        },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(AudioPlayer.prototype, "duration", {
        ///总的时长，以秒为单位
        get: function () {
            if (this._audioElement.duration == null ||
                this._audioElement.duration == 0)
                return 100;
            return this._audioElement.duration;
        },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(AudioPlayer.prototype, "muted", {
        get: function () {
            return this._audioElement.muted;
        },
        set: function (value) {
            this._audioElement.muted = value;
        },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(AudioPlayer.prototype, "currentTime", {
        //返回当前的播放时间，以秒为单位
        get: function () {
            return this._audioElement.currentTime;
        },
        set: function (value) {
            this._audioElement.currentTime = value;
        },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(AudioPlayer.prototype, "paused", {
        get: function () {
            return this._audioElement.paused;
        },
        enumerable: false,
        configurable: true
    });
    AudioPlayer.prototype.checkAcess = function () {
        if (!this._audioElement)
            throw new Error("使用此服务之前，你必须先调用init为其指定一个合法的audio对象");
    };
    AudioPlayer.prototype.initComponent = function () {
        this._audioElement = document.querySelector("#mainAudio");
        this._audioElement.src = '';
    };
    AudioPlayer.prototype.play = function (src) {
        if (src != null)
            this._audioElement.src = src;
        if (this._audioElement.src == null)
            return;
        this._audioElement.play();
        console.log('开始播放' + this._audioElement.src);
    };
    AudioPlayer.prototype.pause = function () {
        this._audioElement.pause();
    };
    return AudioPlayer;
}());
var audioPlayer = new AudioPlayer();
//# sourceMappingURL=audioPlayer.js.map