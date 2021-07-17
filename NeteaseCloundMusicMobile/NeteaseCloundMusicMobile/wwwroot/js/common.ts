﻿
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
            this._audioElement.duration == 0 || isNaN(this._audioElement.duration)
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

        if (src != null && src.length > 0) {
            this._audioElement.src = src;
            this.currentTime = 0;
        }
        if (this._audioElement.src == null || this._audioElement.src.length == 0) return;

        return this._audioElement.play();


    }
    public pause() {
        this._audioElement.pause();
    }
}
(window as any).audioPlayer = new AudioPlayer();

function activeLi(position: number, stopMove: boolean) {
    const liElement = document.querySelector(`.lrc-panel  li[data-position='${position}']`) as HTMLLIElement;
    const ulElement = liElement.parentElement;
    for (let i = 0; i < ulElement.children.length; i++) {
        const element = ulElement.children[i];
        element.classList.remove("active");
    }
    liElement.classList.add("active");
    if (stopMove) return;
    const clientHeight = ulElement.clientHeight;
    const scrollHeight = ulElement.scrollHeight;
    const offsetTop = liElement.offsetTop - ulElement.offsetTop;
    let scrollTop = 0;
    if (offsetTop <= 0.4 * clientHeight) scrollTop = 0;
    else if (offsetTop > scrollHeight - 0.6 * clientHeight)
        scrollTop = scrollHeight - clientHeight;
    else scrollTop = offsetTop - 0.4 * clientHeight;
    ulElement.scrollTop = scrollTop;
}
function delay(ms: number) {
    return new Promise((resolve, reject) => {
        setTimeout(resolve, ms);
    });
}
async function hideBottom(rootElement: HTMLElement) {
    const height = rootElement.clientHeight;
    const symbolElement = rootElement.querySelector(".visible-symbol") as HTMLElement;

    let bottom = 0;
    const totalPiex = Math.abs(-symbolElement.offsetTop - height);
    console.log("大威天龙！")
    while (bottom > -totalPiex) {
        bottom -= totalPiex / 100;
        rootElement.style.bottom = bottom + "px";
        await delay(10);
    }
    rootElement.style.bottom = -totalPiex + "px";
    return true;
}
async function showBottom(rootElement: HTMLElement) {
    const height = rootElement.clientHeight;
    const symbolElement = rootElement.querySelector(".visible-symbol") as HTMLElement;
    let bottom = 0;
    const bottomString = rootElement.style.bottom;
    if (bottomString && bottomString.length > 2)
        bottom = Number(bottomString.slice(0, bottomString.length - 2));
    else return await hideBottom(rootElement);
    const totalPiex = Math.abs(-symbolElement.offsetTop - height);
    console.log("大胆妖孽，还不现出原形！")
    while (bottom < 0) {
        bottom += totalPiex / 100;
        rootElement.style.bottom = bottom + "px";
        await delay(10);
    }
    rootElement.style.bottom = "0px";
    return true;
}
