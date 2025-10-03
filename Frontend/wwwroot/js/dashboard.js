document.addEventListener("DOMContentLoaded", () => {
    const token = localStorage.getItem('token');
    if (!token) {
        window.location.href = "/Login.html";
        return; 
    }
})
document.getElementById("btn-logout").addEventListener("click", function() {
    localStorage.removeItem('token');
    localStorage.removeItem('userEmail');
    window.location.href = "/Login.html";
})


const modalTarefa = document.getElementById('modalTarefa');
const btnNovaTarefa = document.getElementById('btn-add-tarefa');
const closeModal = document.getElementById('closeModal');
const formTarefa = document.getElementById('formTarefa');
btnNovaTarefa.addEventListener('click', () => {
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
formTarefa.addEventListener('submit', async function addTarefa() {


    const nome = document.getElementById('nomeTarefa').value;
    const numero = document.getElementById('numeroTarefa').value;
    const descricao = document.getElementById('descricaoTarefa').value;

    console.log("Tarefa criada:", { nome, numero, descricao });
    
    modalTarefa.style.display = "none";
    formTarefa.reset();
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
        const materias = document.getElementById('loading-materia');
        const token = localStorage.getItem('token');
        const response = await fetch("https://localhost:7118/Materia/GetMaterias", {
            method: "GET",
            headers: {'Content-Type': 'application/json', 'Authorization': `Bearer ${token}`},
        })
        
        if(!response.ok) {
            console.log("deu ruim");
        }
        else{
            const data = await response.json();
            console.log(data);
            let html = "<ul>";
            data.forEach(element => {
                html += `<li>${element.nome}</li>`;
            })
            html += `</ul>`
            materias.innerHTML = html;
            
        }
    }catch(err){
        console.log(err);
    }
}

getAllMaterias();


