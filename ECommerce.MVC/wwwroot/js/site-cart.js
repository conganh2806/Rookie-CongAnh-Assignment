
function showToast(message, isError = false) {
    const toast = `<div class="toast-container position-fixed bottom-0 end-0 p-3">
        <div class="toast align-items-center text-bg-${isError ? 'danger' : 'success'} border-0 show" role="alert">
            <div class="d-flex">
                <div class="toast-body">${message}</div>
                <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button>
            </div>
        </div>
    </div>`;
    $('body').append(toast);
    setTimeout(() => $('.toast').remove(), 3000);
}

function updateCartIcon() {
    $.get('/Cart/GetCartItemCount', function (data) {
        console.log("Received cart count:", data);

        const count = data.count;
        const badgeWrapper = $('.cart-notification-badge');
        const countSpan = $('#cart-count');

        countSpan.text(count);

        if (count > 0) {
            badgeWrapper.removeClass('d-none');
        } else {
            badgeWrapper.addClass('d-none');
        }
    });
}