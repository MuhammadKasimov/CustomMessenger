// wwwroot/js/nicescroll.js
export function initializeNiceScroll() {
    $("#scrollable-div").niceScroll({
        cursorcolor: "#424242",
        cursorwidth: "8px",
        cursorborder: "none",
        autohidemode: false 
    });
}

export function toggleMenuButton() {
    console.log("scripts.js loaded");
    const menuButton = document.getElementById('menuButton');
    if (menuButton) {
        menuButton.classList.toggle('close');
    } else {
        console.error('Menu button not found');
    }
};
