function verificarAuth () {
    const token = localStorage.getItem("token");
    
    if (!token) {
        window.location.href = "/Login.html";
        return false;
    }
    return true;
}


document.getElementById("btn-logout").addEventListener("click", logout);
function logout() {
    localStorage.removeItem("token");
    localStorage.removeItem("user");
    window.location.href = "/Login.html";
    return false;
}

function jaEstaLogado(){
    const token = localStorage.getItem("token");
    const paginaAtual = window.location.pathname;
    
    if(token && paginaAtual.includes('Login.html')){
        window.location.href = "/Dashboard.html";
    }
}