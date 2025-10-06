
document.getElementById("btn-logout").addEventListener("click", function() {
    localStorage.removeItem('token');
    localStorage.removeItem('userEmail');
    window.location.href = "/Login.html";
})

function user(){
    const userArea = document.getElementById("user-area");
    const name = localStorage.getItem('user')
    
    userArea.innerHTML = name;
}

user();


