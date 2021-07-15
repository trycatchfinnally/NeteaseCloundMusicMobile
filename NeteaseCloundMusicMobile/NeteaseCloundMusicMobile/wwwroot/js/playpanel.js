function activeLi(position, stopMove) {
    var liElement = document.querySelector(".lrc-panel  li[data-position='" + position + "']");
    var ulElement = liElement.parentElement;
    for (var i = 0; i < ulElement.children.length; i++) {
        var element = ulElement.children[i];
        element.classList.remove("active");
    }
    liElement.classList.add("active");
    if (stopMove)
        return;
    var clientHeight = ulElement.clientHeight;
    var scrollHeight = ulElement.scrollHeight;
    var offsetTop = liElement.offsetTop - ulElement.offsetTop;
    var scrollTop = 0;
    if (offsetTop <= 0.4 * clientHeight)
        scrollTop = 0;
    else if (offsetTop > scrollHeight - 0.6 * clientHeight)
        scrollTop = scrollHeight - clientHeight;
    else
        scrollTop = offsetTop - 0.4 * clientHeight;
    ulElement.scrollTop = scrollTop;
}
//# sourceMappingURL=playpanel.js.map