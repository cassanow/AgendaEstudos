
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

async function getAllMaterias(){
        const token = localStorage.getItem('token');
        const response = await fetch("https://localhost:7118/api/Materia/GetMaterias", {
            method: "GET",
            headers: {'Content-Type': 'application/json', 'Authorization': `Bearer ${token}`},
        })
    
        if(!response.ok) {
            console.log("deu ruim");
            return [];
        }
        else{
            const materias = await response.json();
            console.log(materias);
            materias.sort((a, b) => {
                const qtdA = Array.isArray(a.tarefas) ? a.tarefas.length : 0;
                const qtdB = Array.isArray(b.tarefas) ? b.tarefas.length : 0;
                return qtdB - qtdA;
            });
            const container = document.getElementById("materias-container");
            container.innerHTML = "";
            
            const top3 = materias.slice(0,3);
            
            top3.forEach(m => {
                const qtd = Array.isArray(m.tarefas) ? m.tarefas.length : 0;
                container.innerHTML += `<div>
                <h3>${m.nome}<br></h3>
                <p>${qtd} tarefas pendentes</p>
                </div>
                `;
            })
            
        }
}

getAllMaterias();
user();


