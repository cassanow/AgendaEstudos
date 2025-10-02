async function register(){
    try{
        const name = document.getElementById('register-name').value;
        const email = document.getElementById('register-email').value;
        const password = document.getElementById('register-password').value;

        const response = await fetch("https://localhost:7118/api/Auth/Register", {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify({name, email, password  })
        })

        if(!response.ok){
            const error = document.getElementById("register-error")
            error.style.display = "block";

            setTimeout(() => {
                error.style.display = "none";
            }, 5000);

        }else{
            window.location.href = 'Login.html';
        }
        
    }catch(err){
        console.log(err)    
    }
       
}