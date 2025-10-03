document.getElementById("btn-login").addEventListener("click", async function login() {
    try {
        const email = document.getElementById("login-email").value;
        const password = document.getElementById("login-password").value;
        
        if (!email || !password) {
            const error = document.getElementById("login-error")
            error.innerText = "Preencha todos os campos!";
            error.style.display = "block";
            setTimeout(() => {
                error.style.display = "none";
            }, 5000);
            return; 
        }
        
        const response = await fetch("https://localhost:7118/api/Auth/Login", {
            method: "POST",
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify({email, password})
        })
        if (!response.ok) {
            const error = document.getElementById("login-error")
            error.style.display = "block";

            setTimeout(() => {
                error.style.display = "none";
            }, 5000);

        } else {
            const data = await response.json();
            localStorage.setItem('token', data.token);
            localStorage.setItem('user', data.user);
            window.location.href = 'Dashboard.html';
        }

    } catch (err) {
        console.log(err)
    }
})
