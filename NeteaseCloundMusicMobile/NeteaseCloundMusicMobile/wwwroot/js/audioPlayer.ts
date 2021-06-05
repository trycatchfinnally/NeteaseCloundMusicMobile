
class AudioPlayer {
    private _audioElement: HTMLAudioElement;

    public get volumn() {
        return Math.floor(this._audioElement.volume * 100);
    }
    public set volumn(value: number) {
        if (value > 100) value = 100;
        this._audioElement.volume = value / 100;
    }

    ///总的时长，以秒为单位
    public get duration(): number {
        if (
            this._audioElement.duration == null ||
            this._audioElement.duration == 0
        )
            return 100;
        return this._audioElement.duration;
    }
    public get muted(): boolean {
        return this._audioElement.muted;
    }
    public set muted(value: boolean) {
        this._audioElement.muted = value;
    }
    //返回当前的播放时间，以秒为单位
    public get currentTime(): number {
        return this._audioElement.currentTime;
    }
    public set currentTime(value: number) {
        this._audioElement.currentTime = value;
    }
    public get paused() {
        return this._audioElement.paused;
    }
    constructor() {
        this.initComponent();
    }
    private checkAcess() {
        if (!this._audioElement)
            throw new Error(
                "使用此服务之前，你必须先调用init为其指定一个合法的audio对象"
            );
    }
    private initComponent() {
        this._audioElement = document.querySelector("#mainAudio");
        this._audioElement.src = '';
       
    }

    public play(src: string) {
        if (src != null) this._audioElement.src = src;
        if (this._audioElement.src == null) return;
        this._audioElement.play();
        console.log('开始播放' + this._audioElement.src);
       
    }
    public pause() {
        this._audioElement.pause();
    }
}
const audioPlayer = new AudioPlayer();