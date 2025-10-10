
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

const modalTarefa = document.getElementById('modal-tarefa');
const btnNovaTarefa = document.getElementById('btn-add-tarefa');
const closeModal = document.getElementById('close-tarefa');
btnNovaTarefa.addEventListener('click', async () => {
    const materias = await getAllMaterias();
    const materiaSelect = document.getElementById('materia-select');
    materiaSelect.innerHTML = "";

    materias.forEach(m => {
        const option = document.createElement("option");
        option.value = m.id;
        option.textContent = m.nome;
        materiaSelect.appendChild(option);
    });

    modalTarefa.style.display = "block";
});

closeModal.addEventListener('click', () => {
    modalTarefa.style.display = "none";
});

window.addEventListener('click', (event) => {
    if (event.target === modalTarefa) {
        modalTarefa.style.display = "none";
    }
});

document.getElementById('btn-salvar').addEventListener('click', async function addTarefa() {
    try{
        const token = localStorage.getItem('token');
        const titulo = document.getElementById('nome-tarefa').value;
        const descricao = document.getElementById('descricao-tarefa').value;
        const prioridade = parseInt(document.getElementById('prioridade-tarefa').value);
        const dataInicio = document.getElementById('tarefa-data-inicio').value;
        const dataFim = document.getElementById('tarefa-data-fim').value;
        const materiaId = document.getElementById('materia-select').value;
        const response = await fetch(`https://localhost:7118/api/Tarefa/AddTarefas?materiaId=${materiaId}`, {
            method: 'POST',
            headers: {'Content-Type': 'application/json', 'Authorization': `Bearer ${token}`},
            body: JSON.stringify({titulo, descricao, prioridade, dataInicio, dataFim})
        });

        if (!response.ok) {
            console.log(response);
        }

        const modal = document.getElementById('modal-tarefa');
        modal.style.display = "none";
        ListaTarefas();

    }catch(err){
        console.log(err);
    }
});

document.addEventListener('click', async function deleteMateria(e) {
    if(e.target.classList.contains('btn-delete')) {
        const token = localStorage.getItem('token');
        const id = e.target.getAttribute('data-id');

        if(!confirm("Are you sure?")){
            return;
        }
        try{
            const response = await fetch(`https://localhost:7118/api/Tarefa/DeleteTarefa/${id}`, {
                method: "DELETE",
                headers: {'Content-Type': 'application/json','Authorization': `Bearer ${token}`},
            })

            if(!response.ok) {
                console.log(response);
                return;
            }

            console.log("deletado com sucesso")
            ListaTarefas();
        }catch(err){
            console.log(err);
        }

    }

})



async function getAllTarefas(){
    try{
        const token = localStorage.getItem('token');
        const response = await fetch("https://localhost:7118/api/Tarefa/GetTarefas", {
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

async function ListaTarefas(){
    const tarefasContainer = document.getElementById('tarefas-container');
    const tarefas = await getAllTarefas();

    let html = "<ul>";
    tarefas.forEach(t => {
        html += `<li>${t.titulo}<button class="btn-delete" data-id="${t.id}">Deletar</button></li>`;
    });
    html += "</ul>";

    tarefasContainer.innerHTML = html;
}

ListaTarefas();
getAllMaterias();