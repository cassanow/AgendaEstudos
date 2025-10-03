
document.getElementById("btn-logout").addEventListener("click", function() {
    localStorage.removeItem('token');
    localStorage.removeItem('userEmail');
    window.location.href = "/Login.html";
})


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

document.getElementById('btn-salvar-tarefa').addEventListener('click', async function addTarefa() {
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
        
        const data = await response.json();
        console.log(data);
        
    }catch(err){
        console.log(err);
    }
});



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
        
        const data = await response.json();
        console.log(data);
    }
    catch(err){
        console.log(err);
    }
    
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

async function ListaTarefas(){
    const tarefasContainer = document.getElementById('tarefas-container');
    const tarefas = await getAllTarefas();

    let html = "<ul>";
    tarefas.forEach(t => {
        html += `<li>${t.titulo}</li>`;
    });
    html += "</ul>";

    tarefasContainer.innerHTML = html;
}

ListaMaterias();
ListaTarefas();
getAllMaterias();


