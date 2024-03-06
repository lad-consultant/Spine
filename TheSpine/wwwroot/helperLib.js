window.scrollToElement = (id) => {
    const element = document.getElementById(id);
    element.scrollIntoView();
}

function handleOutsidePopupMenuClick(event) {
    var openedMenus = document.querySelectorAll('.mud-popover-open');
    var isOutside = true;

    for (var i = 0; i < openedMenus.length; i++) {
        if (openedMenus[i].contains(event.target)) {
            isOutside = false;
            break;
        }
    }

    if (isOutside) {
        for (var i = 0; i < openedMenus.length; i++) {
            openedMenus[i].classList.remove('mud-popover-open');

            const mudOverlayElement = document.querySelector('.mud-overlay');

            if (mudOverlayElement && !mudOverlayElement.contains(event.target)) {
                mudOverlayElement.click();
            }
        }
    }
}

document.body.addEventListener('click', handleOutsidePopupMenuClick);