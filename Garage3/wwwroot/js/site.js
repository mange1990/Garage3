// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function optimiseForPrint() {
    document.querySelectorAll(".hideforprint").forEach(e => e.classList.add('d-none'));
    window.print();
    document.querySelectorAll(".hideforprint").forEach(e => e.classList.remove('d-none'));
}