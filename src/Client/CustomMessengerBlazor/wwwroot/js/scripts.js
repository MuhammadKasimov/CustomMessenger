window.addEscKeyListener = (dotNetHelper) => {
    document.addEventListener('keyup', (event) => {
        if (event.key === 'Escape') {
            dotNetHelper.invokeMethodAsync('OnEscapeKeyPressed');
        }
    });
};

window.removeEscKeyListener = () => {
    document.removeEventListener('keyup', (event) => {
        if (event.key === 'Escape') {
            // Note: The listener can't be directly removed without storing the function reference.
            // This example only shows adding/removing but doesn't properly handle removal.
        }
    });
};

window.getWindowSize = () => {
    return {
        width: window.innerWidth,
        height: window.innerHeight
    };
};
