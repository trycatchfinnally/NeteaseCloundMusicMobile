const rxjs = window.rxjs;
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
    /*总的时长，以秒为单位*/
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
const audioPlayer = new AudioPlayer();
window.audioPlayer = audioPlayer;
/**激活歌词显示 */
let liPositionSubscribers = [];
function startLrcScroll() {
    stopLrcScroll();
    const ulElement = document.querySelector(".lrc-panel");
    if (ulElement == null)
        return;
    const mouseoverSymbol = "data-mouseover";
    liPositionSubscribers =
        [
            rxjs.fromEvent(ulElement, 'mouseover')
                .subscribe(x => ulElement.setAttribute(mouseoverSymbol, 'true')),
            rxjs.fromEvent(ulElement, 'mouseout')
                .subscribe(x => ulElement.removeAttribute(mouseoverSymbol)),
            rxjs.interval(500)
                .pipe(rxjs.operators.filter(x => !audioPlayer.paused))
                .subscribe(x => {
                let liPositions = [];
                for (let i = 0; i < ulElement.children.length; i++) {
                    const element = ulElement.children[i];
                    liPositions.push(Number(element.getAttribute("data-position")));
                }
                let positionMillSeconds = audioPlayer.currentTime * 1000;
                let activePositions = liPositions.filter(x => Math.abs(x - positionMillSeconds) <= 1000)
                    .sort((a, b) => Math.abs(a - positionMillSeconds) - Math.abs(b - positionMillSeconds));
                if (activePositions.length == 0)
                    return;
                let activePosition = activePositions[0];
                let liElement = document.querySelector(`.lrc-panel  li[data-position].text-danger`);
                if (liElement != null)
                    liElement.classList.remove("text-danger");
                liElement = document.querySelector(`.lrc-panel  li[data-position='${activePosition}']`);
                if (liElement != null) {
                    liElement.classList.add("text-danger");
                    if (!ulElement.hasAttribute(mouseoverSymbol))
                        scrollToCenter(ulElement, liElement);
                }
            })
        ];
}
/**断开歌词显示的订阅 */
function stopLrcScroll() {
    if ((liPositionSubscribers === null || liPositionSubscribers === void 0 ? void 0 : liPositionSubscribers.length) > 0)
        liPositionSubscribers.forEach(x => x.unsubscribe());
}
/**
 * 已废弃，不在通过wasm调用
 * @param position
 * @param stopMove
 */
function activeLi(position, stopMove) {
    const liElement = document.querySelector(`.lrc-panel  li[data-position='${position}']`);
    const ulElement = liElement.parentElement;
    for (let i = 0; i < ulElement.children.length; i++) {
        const element = ulElement.children[i];
        element.classList.remove("text-danger");
    }
    liElement.classList.add("text-danger");
    if (stopMove)
        return;
    scrollToCenter(ulElement, liElement);
}
function delay(ms) {
    return new Promise((resolve, reject) => {
        let ts = setTimeout(() => {
            resolve(0);
            clearTimeout(ts);
        }, ms);
    });
}
/**
 * 已废弃，改用css实现
 * @param rootElement
 */
function hideBottom(rootElement) {
    const loadingSymbolCssName = "js-loading";
    if (rootElement.classList.contains(loadingSymbolCssName))
        return false;
    rootElement.classList.add(loadingSymbolCssName);
    const height = rootElement.clientHeight;
    const symbolElement = rootElement.querySelector(".visible-symbol");
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
    }, null, () => rootElement.classList.remove(loadingSymbolCssName));
    return true;
}
/**
 * 已废弃，改用css实现
 * @param rootElement
 */
function showBottom(rootElement) {
    const loadingSymbolCssName = "js-loading";
    if (rootElement.classList.contains(loadingSymbolCssName))
        return false;
    rootElement.classList.add(loadingSymbolCssName);
    const height = rootElement.clientHeight;
    const symbolElement = rootElement.querySelector(".visible-symbol");
    let bottom = 0;
    const bottomString = rootElement.style.bottom;
    if (bottomString && bottomString.length > 2)
        bottom = Number(bottomString.slice(0, bottomString.length - 2));
    else
        return hideBottom(rootElement);
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
    }, null, () => rootElement.classList.remove(loadingSymbolCssName));
    return true;
}
function positionTrack(id, trackQuickBodyRoot) {
    const element = document.querySelector(`[data-track-id='${id}']`);
    if (element == null)
        return;
    const trElement = element.parentElement.parentElement;
    const tbodyElement = trackQuickBodyRoot.parentElement.parentElement;
    scrollToCenter(tbodyElement, trElement);
}
function scrollToCenter(containerElement, itemElement) {
    const clientHeight = containerElement.clientHeight;
    const scrollHeight = containerElement.scrollHeight;
    const offsetTop = itemElement.offsetTop - containerElement.offsetTop;
    let scrollTop = 0;
    if (offsetTop <= 0.4 * clientHeight)
        scrollTop = 0;
    else if (offsetTop > scrollHeight - 0.6 * clientHeight)
        scrollTop = scrollHeight - clientHeight;
    else
        scrollTop = offsetTop - 0.4 * clientHeight;
    containerElement.scrollTop = scrollTop;
}
class searchProgress {
    initComponent(dotNetObjectReference, input) {
        // this._dotNetObjectReference = dotNetObjectReference;
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
function copy(txt) {
    if ((txt === null || txt === void 0 ? void 0 : txt.length) > 0) {
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
//# sourceMappingURL=common.js.map