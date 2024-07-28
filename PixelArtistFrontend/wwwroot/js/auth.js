document.addEventListener('DOMContentLoaded', function() {
    const registerBtn = document.getElementById('registerBtn');
    const loginBtn = document.getElementById('loginBtn');

    registerBtn.addEventListener('click', function() {
        handleAuth('register');
    });

    loginBtn.addEventListener('click', function() {
        handleAuth('login');
    });

    function handleAuth(action) {
        const username = document.getElementById('username').value;
        const password = document.getElementById('password').value;

        if (username && password) {
            fetch(`/api/auth/${action}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ username, password })
            })
            .then(response => response.json())
            .then(data => {
                console.log(`${action} response:`, data);
                alert(`${action.charAt(0).toUpperCase() + action.slice(1)} successful!`);
            })
            .catch(error => {
                console.error(`${action} error:`, error);
                alert(`${action.charAt(0).toUpperCase() + action.slice(1)} failed!`);
            });
        } else {
            alert('Please enter both username and password.');
        }
    }
});
