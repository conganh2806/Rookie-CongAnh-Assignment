document.addEventListener("DOMContentLoaded", function () {
    const buttons = document.querySelectorAll(".show-password");

    buttons.forEach(button => {
        button.addEventListener("click", function () {
            const input = this.closest(".input-group").querySelector("input");
            const isPassword = input.getAttribute("type") === "password";

            input.setAttribute("type", isPassword ? "text" : "password");
            this.textContent = isPassword ? "Hide" : "Show";
        });
    });
});