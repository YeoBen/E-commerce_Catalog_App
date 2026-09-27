window.initSwipeToRemove = function (containerId, dotNetHelper) {
    const container = document.getElementById(containerId);
    if (!container) return;

    let startX = 0;
    let currentRow = null;
    const threshold = 60;

    container.addEventListener('touchstart', e => {
        currentRow = e.target.closest('.cart-row');
        if (currentRow) startX = e.touches[0].clientX;
    });

    container.addEventListener('touchmove', e => {
        if (!currentRow) return;
        const deltaX = e.touches[0].clientX - startX;
        if (deltaX < 0) {
            currentRow.style.transform = `translateX(${Math.max(deltaX, -100)}px)`;
        }
    });

    container.addEventListener('touchend', e => {
        if (!currentRow) return;
        const deltaX = e.changedTouches[0].clientX - startX;
        if (deltaX < -threshold) {
            const productId = parseInt(currentRow.dataset.productId);
            const uom = currentRow.dataset.uom;
            dotNetHelper.invokeMethodAsync('OnSwipeRemove', productId, uom);
        }
        currentRow.style.transform = '';
        currentRow = null;
    });
};