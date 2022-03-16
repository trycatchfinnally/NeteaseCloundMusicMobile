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

    /*总的时长，以秒为单位*/
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
    scrollToCenter(ulElement, liElement);

}
function delay(ms: number) {
    return new Promise((resolve, reject) => {
        let ts = setTimeout(() => {
            resolve(0);
            clearTimeout(ts);
        }, ms);
    });
}
function hideBottom(rootElement: HTMLElement) {
    const loadingSymbolCssName = "js-loading";
    if (rootElement.classList.contains(loadingSymbolCssName)) return false;
    rootElement.classList.add(loadingSymbolCssName);
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

    rxjs.from(bottomArray.map(x => rxjs.of(x).pipe(rxjs.operators.delay(20)))).pipe(rxjs.operators.concatAll())
        .subscribe(x => {
            rootElement.style.bottom = x + "px";
            
        },null, () => rootElement.classList.remove(loadingSymbolCssName));
    return true;
}
function showBottom(rootElement: HTMLElement) {
    const loadingSymbolCssName = "js-loading";
    if (rootElement.classList.contains(loadingSymbolCssName)) return false;
    rootElement.classList.add(loadingSymbolCssName);

    const height = rootElement.clientHeight;
    const symbolElement = rootElement.querySelector(".visible-symbol") as HTMLElement;
    let bottom = 0;
    const bottomString = rootElement.style.bottom;
    if (bottomString && bottomString.length > 2)
        bottom = Number(bottomString.slice(0, bottomString.length - 2));
    else return hideBottom(rootElement);
    const totalPiex = Math.abs(-symbolElement.offsetTop - height);
    console.log("大胆妖孽，还不现出原形！");
    let bottomArray = [bottom];
    while (bottom < 0) {
        bottom += totalPiex / 50;
        bottomArray.push(bottom);
    }
    bottomArray.push(0);
    rxjs.from(bottomArray.map(x => rxjs.of(x).pipe(rxjs.operators.delay(20)))).pipe(rxjs.operators.concatAll())
        .subscribe(x => {
            rootElement.style.bottom = x + "px";
            
        },null, () => rootElement.classList.remove(loadingSymbolCssName));
    return true;
}

function positionTrack(id: number, trackQuickBodyRoot: HTMLElement) {

    const element = document.querySelector(`[data-track-id='${id}']`) as HTMLElement;

    if (element == null) return;
    const trElement = element.parentElement.parentElement;
    const tbodyElement = trackQuickBodyRoot.parentElement.parentElement;
    scrollToCenter(tbodyElement, trElement);

}


function scrollToCenter(containerElement: HTMLElement, itemElement: HTMLElement) {

    const clientHeight = containerElement.clientHeight;
    const scrollHeight = containerElement.scrollHeight;
    const offsetTop = itemElement.offsetTop - containerElement.offsetTop;

    let scrollTop = 0;
    if (offsetTop <= 0.4 * clientHeight) scrollTop = 0;
    else if (offsetTop > scrollHeight - 0.6 * clientHeight)
        scrollTop = scrollHeight - clientHeight;
    else scrollTop = offsetTop - 0.4 * clientHeight;

    containerElement.scrollTop = scrollTop;
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


function copy(txt: string) {
    if (txt?.length > 0) {
        const aux = document.createElement("input");
        aux.setAttribute("value", txt);
        document.body.appendChild(aux);
        aux.select();
        document.execCommand("copy");
        document.body.removeChild(aux);


        return true;
    }
    return false;
}