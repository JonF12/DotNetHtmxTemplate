let startFrom = 0;
const pageSize = 3;

const prevBtn = document.getElementById('previous-page-btn') as HTMLButtonElement;
const nextBtn = document.getElementById('next-page-btn') as HTMLButtonElement;

function updatePaginationButtons(): void {
    nextBtn.setAttribute('hx-vals', JSON.stringify({ PageSize: pageSize, StartFrom: startFrom + pageSize }));
    
    if (startFrom > 0) {
        prevBtn.removeAttribute('disabled');
        prevBtn.setAttribute('hx-vals', JSON.stringify({ PageSize: pageSize, StartFrom: startFrom - pageSize }));
    } else {
        prevBtn.setAttribute('disabled', 'true');
    }
}

nextBtn.addEventListener('click', () => {
    startFrom += pageSize;  
    updatePaginationButtons();  
});

prevBtn.addEventListener('click', () => {
    if (startFrom > 0) {
        startFrom -= pageSize;  
        updatePaginationButtons();
    }
});

// Initialize the button states on load
updatePaginationButtons();