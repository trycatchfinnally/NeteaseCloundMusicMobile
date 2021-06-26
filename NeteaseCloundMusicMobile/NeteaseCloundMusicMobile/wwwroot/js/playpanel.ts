 function activeLi(position: number, isMouseOver: boolean) {
    let liElement = document.querySelector(`.lrc-panel  li[data-position='${position}']`) as HTMLLIElement;
    let ulElement = liElement.parentElement;
    for (var i = 0; i < ulElement.children.length; i++) {
        const element = ulElement.children[i];
        element.classList.remove("active");
    }
    liElement.classList.add("active");
    if (isMouseOver) return;
    let clientHeight = ulElement.clientHeight;
    let scrollHeight = ulElement.scrollHeight;
    let offsetTop = liElement.offsetTop - ulElement.offsetTop;
    let scrollTop = 0;
    if (offsetTop <= 0.4 * clientHeight) scrollTop = 0;
    else if (offsetTop > scrollHeight - 0.6 * clientHeight)
        scrollTop = scrollHeight - clientHeight;
    else scrollTop = offsetTop - 0.4 * clientHeight;
    ulElement.scrollTop = scrollTop;
}