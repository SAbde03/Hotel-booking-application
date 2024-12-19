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
let count = 0;
localStorage.setItem('counter', count);
function Increment() {
    count = parseInt(localStorage.getItem('counter')) || 0;
    count++;
    localStorage.setItem('counter', count);
    document.getElementById("counter").value = count;
    updatePrice();
}
function Decrement() {
    count = parseInt(localStorage.getItem('counter')) || 0;
    count--;
    localStorage.setItem('counter', count);
    document.getElementById("counter").value = count;
    
    updatePrice();
}
function updatePrice() {
    let price = parseInt(document.getElementById("price").value) || 0; // Get the price value
    let count = parseInt(document.getElementById("counter").value) || 0; // Get the counter value
    let total = price * count; // Calculate the total price
    document.getElementById("price").innerHTML = total; // Update the price display
    localStorage.setItem('price', total); // Optionally store the total price in localStorage
}

overlay.addEventListener('click', closePopup);
