"use strict";
var startFrom = 0;
var pageSize = 3;
var prevBtn = document.getElementById('previous-page-btn');
var nextBtn = document.getElementById('next-page-btn');
function updatePaginationButtons() {
    nextBtn.setAttribute('hx-vals', JSON.stringify({ PageSize: pageSize, StartFrom: startFrom + pageSize }));
    if (startFrom > 0) {
        prevBtn.removeAttribute('disabled');
        prevBtn.setAttribute('hx-vals', JSON.stringify({ PageSize: pageSize, StartFrom: startFrom - pageSize }));
    }
    else {
        prevBtn.setAttribute('disabled', 'true');
    }
}
nextBtn.addEventListener('click', function () {
    startFrom += pageSize;
    updatePaginationButtons();
});
prevBtn.addEventListener('click', function () {
    if (startFrom > 0) {
        startFrom -= pageSize;
        updatePaginationButtons();
    }
});
// Initialize the button states on load
updatePaginationButtons();
