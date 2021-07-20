var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
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
    const liElement = document.querySelector(`.lrc-panel  li[data-position='${position}']`);
    const ulElement = liElement.parentElement;
    for (let i = 0; i < ulElement.children.length; i++) {
        const element = ulElement.children[i];
        element.classList.remove("active");
    }
    liElement.classList.add("active");
    if (stopMove)
        return;
    const clientHeight = ulElement.clientHeight;
    const scrollHeight = ulElement.scrollHeight;
    const offsetTop = liElement.offsetTop - ulElement.offsetTop;
    let scrollTop = 0;
    if (offsetTop <= 0.4 * clientHeight)
        scrollTop = 0;
    else if (offsetTop > scrollHeight - 0.6 * clientHeight)
        scrollTop = scrollHeight - clientHeight;
    else
        scrollTop = offsetTop - 0.4 * clientHeight;
    ulElement.scrollTop = scrollTop;
}
function delay(ms) {
    return new Promise((resolve, reject) => {
        let ts = setTimeout(() => {
            resolve(0);
            clearTimeout(ts);
        }, ms);
    });
}
function hideBottom(rootElement) {
    return __awaiter(this, void 0, void 0, function* () {
        const height = rootElement.clientHeight;
        const symbolElement = rootElement.querySelector(".visible-symbol");
        let bottom = 0;
        const totalPiex = Math.abs(-symbolElement.offsetTop - height);
        console.log("大威天龙！" + totalPiex);
        while (bottom > -totalPiex) {
            bottom -= totalPiex / 100;
            rootElement.style.bottom = bottom + "px";
            yield delay(10);
        }
        rootElement.style.bottom = -totalPiex + "px";
        return true;
    });
}
function showBottom(rootElement) {
    return __awaiter(this, void 0, void 0, function* () {
        const height = rootElement.clientHeight;
        const symbolElement = rootElement.querySelector(".visible-symbol");
        let bottom = 0;
        const bottomString = rootElement.style.bottom;
        if (bottomString && bottomString.length > 2)
            bottom = Number(bottomString.slice(0, bottomString.length - 2));
        else
            return yield hideBottom(rootElement);
        const totalPiex = Math.abs(-symbolElement.offsetTop - height);
        console.log("大胆妖孽，还不现出原形！" + totalPiex);
        while (bottom < 0) {
            bottom += totalPiex / 100;
            rootElement.style.bottom = bottom + "px";
            yield delay(10);
        }
        rootElement.style.bottom = "0px";
        return true;
    });
}
class searchProgress {
    initComponent(dotNetObjectReference, input) {
        // this._dotNetObjectReference = dotNetObjectReference;
        const rxjs = window.rxjs;
        rxjs.fromEvent(input, "input").pipe(rxjs.operators.debounceTime(200), rxjs.operators.distinctUntilChanged())
            .subscribe((value) => {
            const keyword = value.target.value;
            if ((keyword === null || keyword === void 0 ? void 0 : keyword.length) > 0) {
                dotNetObjectReference.invokeMethodAsync("DoSearchAsync", keyword);
            }
        });
    }
}
window.searchProgress = new searchProgress();
//# sourceMappingURL=common.js.map