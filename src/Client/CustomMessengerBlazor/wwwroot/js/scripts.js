const menuButton = document.getElementById('menuButton');
window.addEscKeyListener = (dotNetHelper) => {
    document.addEventListener('keyup', (event) => {
        if (event.key === 'Escape') {
            dotNetHelper.invokeMethodAsync('OnEscapeKeyPressed');
        }
    });
};


window.getWindowSize = () => {
    return {
        width: window.innerWidth,
        height: window.innerHeight
    };
};

function toggleMenuButton() {
    const menuButton = document.getElementById('menu-button');
    if (menuButton) {
        console.log("entered")
        menuButton.classList.toggle('close');
    } else {
        console.error('Menu button not found');
    }
};