const popup = document.getElementById('popup');
const overlay = document.getElementById('overlay');
const loginForm = document.getElementById('login-form');
const signupForm = document.getElementById('signup-form');
const popupTitle = document.getElementById('popup-title');
const toggleLink = document.getElementById('toggle-link');

function openPopup(defaultPage) {
    popup.classList.add('active');
    overlay.classList.add('active');

    if (defaultPage === 'login') {
        showLogin();
    } else {
        showSignUp();
    }
}

function closePopup() {
    popup.classList.remove('active');
    overlay.classList.remove('active');
}

function toggleForm() {
    if (loginForm.style.display === 'none') {
        showLogin();
    } else {
        showSignUp();
    }
}

function showLogin() {
    loginForm.style.display = 'flex';
    signupForm.style.display = 'none';
    popupTitle.textContent = 'Login';
    toggleLink.innerHTML = "Don't have an account? <a onclick='toggleForm()'>Sign Up</a>";
}

function showSignUp() {
    loginForm.style.display = 'none';
    signupForm.style.display = 'flex';
    popupTitle.textContent = 'Sign Up';
    toggleLink.innerHTML = "Already have an account? <a onclick='toggleForm()'>Login</a>";
}

overlay.addEventListener('click', closePopup);
