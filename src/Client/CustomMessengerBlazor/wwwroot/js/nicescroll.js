// wwwroot/js/nicescroll.js
function initializeNiceScroll(selector) {
    $(selector).niceScroll({
        cursorcolor: "#424242", // Customize the cursor color
        cursorwidth: "8px",     // Customize the cursor width
        cursorborder: "none",   // Remove cursor border
        railpadding: { top: 0, right: 0, left: 0, bottom: 0 }, // Remove extra padding
        autohidemode: false     // Ensure the scrollbar is always visible (optional)
    });
}