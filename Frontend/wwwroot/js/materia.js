

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
        const response = await fetch("https://localhost:7118/api/Materia/AddMateria", {
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


document.addEventListener('click', async function deleteMateria(e) {
    if(e.target.classList.contains('btn-delete')) {
        const token = localStorage.getItem('token');
        const id = e.target.getAttribute('data-id');
        
        if(!confirm("Are you sure?")){
            return;
        }
        try{
            const response = await fetch(`https://localhost:7118/api/Materia/DeleteMateria/${id}`, {
                method: "DELETE",
                headers: {'Content-Type': 'application/json','Authorization': `Bearer ${token}`},
            })

            if(!response.ok) {
                console.log(response);
                return;
            }

            console.log("deletado com sucesso")
            ListaMaterias();
        }catch(err){
            console.log(err);
        }
        
    }
    
})

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

async function ListaMaterias() {
    const materiasContainer = document.getElementById('materias-container');
    const materias = await getAllMaterias();

    let html = "<ul>";
    materias.forEach(m => {
        html += `
        <li>
            ${m.nome}
            <div class="botoes">
                <button class="btn-edit" data-id="${m.id}">Editar</button>
                <button class="btn-delete" data-id="${m.id}">Deletar</button>
            </div>
        </li>
    `;
    });
    html += "</ul>";
    materiasContainer.innerHTML = html;
}


getAllMaterias();
ListaMaterias();