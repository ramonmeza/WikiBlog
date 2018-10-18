var postModal = $("#postModal");

postModal.on("hidden.bs.modal", function () {
    window.location.href = "/posts/";
});

// Edit post button
$("#editModal").on("click", function () {
    postModal.find(".modal-content").each(function () {
        $(this).toggleClass("d-none");
    });
});