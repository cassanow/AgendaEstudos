function user(){
    const userArea = document.getElementById("user-area");
    const name = localStorage.getItem('user')

    userArea.innerHTML = name;
}

document.addEventListener("DOMContentLoaded", () => {
    const menuItems = document.querySelectorAll(".menu-item");
    const currentPage = window.location.pathname.split("/").pop();

    menuItems.forEach(item => {
        // Remove a classe ativa de todos
        item.classList.remove("active");

        // Compara o href com o nome do arquivo atual
        const href = item.getAttribute("href");
        if (href === currentPage) {
            item.classList.add("active");
        }

        // Também adiciona evento de clique para destacar enquanto navega
        item.addEventListener("click", () => {
            menuItems.forEach(i => i.classList.remove("active"));
            item.classList.add("active");
        });
    });
});

async function getAllMaterias(){
    try{
        const token = localStorage.getItem('token');
        const response = await fetch("https://localhost:7118/Materia/GetMaterias", {
            method: "GET",
            headers: {'Content-Type': 'application/json', 'Authorization': `Bearer ${token}`},
        })

        if(!response.ok) {
            console.log("deu ruim");
            return [];
        }
        else{
            return await response.json();
        }
    }catch(err){
        console.log(err);
        return [];
    }

}

const modalMateria = document.getElementById('modal-materia');
const btnNovaMateria = document.getElementById('btn-add-materia');
const closeModalMateria = document.getElementById('close-materia');
btnNovaMateria.addEventListener('click', () => {
    modalMateria.style.display = "block";
});

closeModalMateria.addEventListener('click', () => {
    modalMateria.style.display = "none";
});

window.addEventListener('click', (event) => {
    if (event.target === modalMateria) {
        modalMateria.style.display = "none";
    }
});
document.getElementById('btn-salvar').addEventListener('click', async function addMateria() {
    try{
        const token = localStorage.getItem('token');
        const nome = document.getElementById('nome-materia').value;
        const prioridade = parseInt(document.getElementById('prioridade-materia').value);
        const response = await fetch("https://localhost:7118/Materia/AddMateria", {
            method: "POST",
            headers: {'Content-Type': 'application/json','Authorization': `Bearer ${token}`},
            body: JSON.stringify({nome, prioridade}),
        })
        if(!response.ok) {
            console.log(response);
        }

        const modal = document.getElementById('modal-materia');
        modal.style.display = "none";
        
        ListaMaterias();
    }
    catch(err){
        console.log(err);
    }

});

async function ListaMaterias() {
    const materiasContainer = document.getElementById('materias-container');
    const materias = await getAllMaterias();

    let html = "<ul>";
    materias.forEach(m => {
        html += `<li>${m.nome}</li>`;
    });
    html += "</ul>";

    materiasContainer.innerHTML = html;
}


getAllMaterias();
ListaMaterias();
user();