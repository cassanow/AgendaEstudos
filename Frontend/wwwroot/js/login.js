async function login() {
    try{
        const email = document.getElementById("login-email").value;
        const password = document.getElementById("login-password").value;
        const response = await fetch("https://localhost:7118/api/Auth/Login", {
            method: "POST",
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify({email, password})
        });

        const data = await response.json();
        localStorage.setItem('token', JSON.stringify(data.token));
        console.log("logado com sucesso")
    }catch(err){
        console.log(err)        
    }
    
}