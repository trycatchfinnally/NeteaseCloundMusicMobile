﻿const rxjs = (window as any).rxjs;
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
        this._audioElement = document.querySelector("#mainAudio") as HTMLAudioElement;
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
        element.classList.remove("text-danger");
    }
    liElement.classList.add("text-danger");
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
      let ts=  setTimeout(() => {
            resolve(0);
          clearTimeout(ts);
        }, ms);
    });
}
async function hideBottom(rootElement: HTMLElement) {
    const height = rootElement.clientHeight;
    const symbolElement = rootElement.querySelector(".visible-symbol") as HTMLElement;

    let bottom = 0;
    const totalPiex = Math.abs(-symbolElement.offsetTop - height);
    console.log("大威天龙！");
    let bottomArray = [bottom];
    while (bottom > -totalPiex) {
        bottom -= totalPiex / 50;
        bottomArray.push(bottom);
    }
    bottomArray.push(-totalPiex);
    rxjs.interval(20).pipe(rxjs.operators.take(bottomArray.length), rxjs.operators.map(x => bottomArray[x]))
        .subscribe(x => rootElement.style.bottom = x + "px");
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
    console.log("大胆妖孽，还不现出原形！");
    let bottomArray = [bottom];
    while (bottom < 0) {
        bottom += totalPiex / 50;
        bottomArray.push(bottom);
    }
    bottomArray.push(0);
    rxjs.interval(20).pipe(rxjs.operators.take(bottomArray.length), rxjs.operators.map(x => bottomArray[x]))
        .subscribe(x => rootElement.style.bottom = x + "px");
    return true;
}

function positionTrack(id: number) {
    console.log(id);
    const element = document.querySelector(`[data-track-id='${id}']`) as HTMLElement;
    
    if (element == null) return;
    const trElement = element.parentElement.parentElement;
    const tbodyElement = trElement.parentElement;
    const clientHeight = tbodyElement.clientHeight;
    const scrollHeight = tbodyElement.scrollHeight;
    const offsetTop = trElement.offsetTop - tbodyElement.offsetTop;
    let scrollTop = 0;
    if (offsetTop <= 0.4 * clientHeight) scrollTop = 0;
    else if (offsetTop > scrollHeight - 0.6 * clientHeight)
        scrollTop = scrollHeight - clientHeight;
    else scrollTop = offsetTop - 0.4 * clientHeight;
    tbodyElement.scrollTop = scrollTop;
}

class searchProgress {

    public initComponent(dotNetObjectReference: any, input: HTMLInputElement) {
        // this._dotNetObjectReference = dotNetObjectReference;
       
        rxjs.fromEvent(input, "input").pipe(rxjs.operators.debounceTime(200), rxjs.operators.distinctUntilChanged())
            .subscribe((value: any) => {
                const keyword = value.target.value as string;

                if (keyword?.length > 0) {
                    dotNetObjectReference.invokeMethodAsync("DoSearchAsync", keyword);
                }

            });

    }


}
(window as any).searchProgress = new searchProgress();