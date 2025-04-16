$(document).ready(function () {
    const $toggleButton = $('#toggleCategories');
    const $categoryList = $('#categoryList');

    $toggleButton.on('click', function (e) {
        console.log("Toggle button clicked");
        e.stopPropagation();
        $categoryList.toggleClass('d-none');
    });

    $(document).on('click', function (e) {
        console.log("Click outside, closing dropdown");
        if (!$(e.target).closest('#categoryList, #toggleCategories').length) {
            $categoryList.addClass('d-none');
        }
    });
});