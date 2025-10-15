let modoEdicao = false;
let idTarefa = null;

async function getAllMaterias(){
    try{
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
    document.getElementById("modal-titulo").textContent = "Nova Tarefa";
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
        const url = modoEdicao
            ? `https://localhost:7118/api/Tarefa/UpdateTarefa/${idTarefa}`
            : `https://localhost:7118/api/Tarefa/AddTarefas`;
        
        const metodo = modoEdicao ? 'PUT' : 'POST';
        
        const token = localStorage.getItem('token');
        const titulo = document.getElementById('nome-tarefa').value;
        const descricao = document.getElementById('descricao-tarefa').value;
        const prioridade = parseInt(document.getElementById('prioridade-tarefa').value);
        let dataInicio = document.getElementById('tarefa-data-inicio').value;
        let dataFim = document.getElementById('tarefa-data-fim').value;
        const materiaId = document.getElementById('materia-select').value;

        if (!dataInicio.includes('T')) {
            dataInicio = dataInicio + 'T00:00:00';
        }
        if (!dataFim.includes('T')) {
            dataFim = dataFim + 'T00:00:00';
        }
        
        
        const response = await fetch(url, {
            method: metodo,
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

document.addEventListener("click", async function (e) {
    if(e.target.classList.contains("btn-edit")){
        modoEdicao = true;
        idTarefa = e.target.getAttribute("data-id");
        const token = localStorage.getItem('token');
        modalTarefa.style.display = "block";
        document.getElementById("modal-titulo").textContent = "Editar Tarefa";

        const materias = await getAllMaterias();
        const materiaSelect = document.getElementById('materia-select');
        materiaSelect.innerHTML = "";

        materias.forEach(m => {
            const option = document.createElement("option");
            option.value = m.id;
            option.textContent = m.nome;
            materiaSelect.appendChild(option);
        });
        
        const response = await fetch(`https://localhost:7118/api/Tarefa/GetTarefa/${idTarefa}`, {
            method: 'GET',
            headers: {'Content-Type': 'application/json', 'Authorization': `Bearer ${token}`},
            })
        
        const tarefa = await response.json();
        
        const titulo = document.getElementById('nome-tarefa').value = tarefa.titulo;
        const descricao = document.getElementById('descricao-tarefa').value = tarefa.descricao;
        const prioridade = parseInt(document.getElementById('prioridade-tarefa').value = tarefa.prioridade);
        let dataInicio = document.getElementById('tarefa-data-inicio').value = tarefa.dataInicio ? tarefa.dataInicio.split('T')[0] : '';
        let dataFim = document.getElementById('tarefa-data-fim').value =  tarefa.dataFim ? tarefa.dataFim.split('T')[0] : '';
        const materia = document.getElementById('materia-select').value = tarefa.materiaId
    }
})

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
        html += `
        <li>
            ${t.titulo}
            <div class="botoes">
                <button class="btn-edit" data-id="${t.id}">Editar</button>
                <button class="btn-delete" data-id="${t.id}">Deletar</button>
            </div>
        </li>
    `;
    });
    html += "</ul>";

    tarefasContainer.innerHTML = html;
}

ListaTarefas();
getAllMaterias();