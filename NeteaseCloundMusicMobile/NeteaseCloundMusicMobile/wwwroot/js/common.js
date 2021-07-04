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
function activeLi(position, stopMove) {
    let liElement = document.querySelector(`.lrc-panel  li[data-position='${position}']`);
    let ulElement = liElement.parentElement;
    for (var i = 0; i < ulElement.children.length; i++) {
        const element = ulElement.children[i];
        element.classList.remove("active");
    }
    liElement.classList.add("active");
    if (stopMove)
        return;
    let clientHeight = ulElement.clientHeight;
    let scrollHeight = ulElement.scrollHeight;
    let offsetTop = liElement.offsetTop - ulElement.offsetTop;
    let scrollTop = 0;
    if (offsetTop <= 0.4 * clientHeight)
        scrollTop = 0;
    else if (offsetTop > scrollHeight - 0.6 * clientHeight)
        scrollTop = scrollHeight - clientHeight;
    else
        scrollTop = offsetTop - 0.4 * clientHeight;
    ulElement.scrollTop = scrollTop;
}
//# sourceMappingURL=common.js.map