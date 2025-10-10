
document.getElementById("btn-logout").addEventListener("click", function() {
    localStorage.removeItem('token');
    localStorage.removeItem('userEmail');
    window.location.href = "/Login.html";
})

document.addEventListener("DOMContentLoaded", () => {
    const menuItems = document.querySelectorAll(".menu-item");
    const currentPage = window.location.pathname.split("/").pop();

    menuItems.forEach(item => {
        item.classList.remove("active");

        const href = item.getAttribute("href");
        if (href === currentPage) {
            item.classList.add("active");
        }

        item.addEventListener("click", () => {
            menuItems.forEach(i => i.classList.remove("active"));
            item.classList.add("active");
        });
    });
});


function user(){
    const userArea = document.getElementById("user-area");
    const name = localStorage.getItem('user')
    
    userArea.innerHTML = "Bem vindo, " + name + "!";
}

user();


