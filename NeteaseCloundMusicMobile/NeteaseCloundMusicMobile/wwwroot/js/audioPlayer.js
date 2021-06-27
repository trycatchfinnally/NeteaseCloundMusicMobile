class AudioPlayer {
    constructor() {
        this.initComponent();
    }
    get volumn() {
        return Math.floor(this._audioElement.volume * 100);
    }
    set volumn(value) {
        if (value > 100)
            value = 100;
        this._audioElement.volume = value / 100;
    }
    ///总的时长，以秒为单位
    get duration() {
        if (this._audioElement.duration == null ||
            this._audioElement.duration == 0 || isNaN(this._audioElement.duration))
            return 100;
        return this._audioElement.duration;
    }
    get muted() {
        return this._audioElement.muted;
    }
    set muted(value) {
        this._audioElement.muted = value;
    }
    //返回当前的播放时间，以秒为单位
    get currentTime() {
        return this._audioElement.currentTime;
    }
    set currentTime(value) {
        this._audioElement.currentTime = value;
    }
    get paused() {
        return this._audioElement.paused;
    }
    checkAcess() {
        if (!this._audioElement)
            throw new Error("使用此服务之前，你必须先调用init为其指定一个合法的audio对象");
    }
    initComponent() {
        this._audioElement = document.querySelector("#mainAudio");
        this._audioElement.src = '';
    }
    play(src) {
        if (src != null && src.length > 0) {
            this._audioElement.src = src;
            this.currentTime = 0;
        }
        if (this._audioElement.src == null || this._audioElement.src.length == 0)
            return;
        return this._audioElement.play();
    }
    pause() {
        this._audioElement.pause();
    }
}
window.audioPlayer = new AudioPlayer();
//# sourceMappingURL=audioPlayer.js.map