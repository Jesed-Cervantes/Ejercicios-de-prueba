async function getFibonacci() {
    const n = document.getElementById('inputN').value;
    const url = `/api/Fibonacci/${n}`;

    try {
        const response = await fetch(url);
        const data = await response.json();
        document.getElementById('result').innerText = JSON.stringify(data);
    } catch (error) {
        console.error('Error:', error);
        document.getElementById('result').innerText = 'Error al obtener la secuencia.';
    }
}

const uri_1 = 'api/TodoUsers';
const uri_2 = 'api/TodoMusicas';

let todoUsers = [];
let todoMusic = [];

function getUsers() {
    fetch(uri_1)
        .then(response => response.json())
        .then(data => {
            todoUsers = data;
            _displayUsers(data)
        })
        .catch(error => console.error('error al traer los items', error));
}
function addUsers() {
    const addNameTexbox = document.getElementById('add-name');
    const addEmailTexbox = document.getElementById('add-email');
    const item = {
        name: addNameTexbox.value.trim(),
        email: addEmailTexbox.value.trim()
    }
    fetch(uri_1, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            getUsers();
            addEmailTexbox.value = "";
            addNameTexbox.value = "";
        })
        .catch(error => console.error('no es posible actualizar el usuario', error));
}
function deleteUsers(id) {
    fetch(`${uri_1}/${id}`, {
        method:'DELETE'
    })
        .then(() => getUsers())
        .catch(error => console.error('no es posible eliminar el usuario', error));
}
function updateUsers(id) {
    const item = todoUsers.find(item => item.id === id);

    const itemRow = document.getElementById(`row-${id}`);
    const nameInput = itemRow.querySelector('.edit-name');
    const emailInput = itemRow.querySelector('.edit-email');

    item.name = nameInput.value.trim();
    item.email = emailInput.value.trim();

    fetch(`${uri_1}/${id}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(() => getUsers())
        .catch(error => console.error('no es posible actualizar el usuario', error));
}
function _displayCount(UsersCount) {
    const name = (UsersCount === 1) ? 'un usuario' : 'usuarios';
    document.getElementById('counter').innerText = `${UsersCount} ${name}`;
}
function _displayUsers(data) {
    const tbody = document.getElementById('todosUsers')
    tbody.innerHTML = ""

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        let tr = tbody.insertRow();
        tr.setAttribute('id', `row-${item.id}`);

        let td1 = tr.insertCell(0);
        let nameInput = document.createElement('input');
        nameInput.type = 'text';
        nameInput.className = 'edit-name';
        nameInput.value = item.name;
        td1.appendChild(nameInput);

        let td2 = tr.insertCell(1);
        let emailInput = document.createElement('input');
        emailInput.type = 'email';
        nameInput.className = 'edit-email';
        emailInput.value = item.email;
        td2.appendChild(emailInput);

        let td3 = tr.insertCell(2);
        let saveButton = button.cloneNode(false);
        saveButton.innerText = 'actualizar';
        saveButton.setAttribute('onclick', `updateUsers(${item.id})`);
        td3.appendChild(saveButton);

        let td4 = tr.insertCell(3);
        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'eliminar';
        deleteButton.setAttribute('onclick', `deleteUsers(${item.id})`);
        td4.appendChild(deleteButton);

    })


}


function getMusic() { }
function addMusic() { }
function deleteMusic() { }
function updateMusic() { }
function _displayMusic(data) { }


