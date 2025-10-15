function verificarAuth () {
    const token = localStorage.getItem("token");
    
    if (!token) {
        window.location.href = "/Login.html";
        return false;
    }
    return true;
}

document.addEventListener('DOMContentLoaded', () => {
    const btnLogout = document.getElementById("btn-logout");

    if (btnLogout) {
        btnLogout.addEventListener("click", logout);
    }
});

function logout() {
    localStorage.removeItem("token");
    localStorage.removeItem("user");
    window.location.href = "/Login.html";
}

function jaEstaLogado(){
    const token = localStorage.getItem("token");
    const paginaAtual = window.location.pathname;
    
    if(token && paginaAtual.includes('Login.html')){
        window.location.href = "/Dashboard.html";
    }
}