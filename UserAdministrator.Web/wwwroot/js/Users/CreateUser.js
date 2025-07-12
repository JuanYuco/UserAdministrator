import { UserService } from './UserService.js';

window.onload = () => {
    const createUserForm = document.querySelector("#formCreateUser");
    createUserForm.addEventListener('submit', createUser);
};

const createUser = async (event) => {
    event.preventDefault();

    const labelMessage = document.querySelector("#laMessage");
    const nameInput = document.querySelector("#txtName");
    const birthDateInput = document.querySelector("#txtBirthDate");
    const genderInput = document.querySelector("#slGender");

    const { value: name } = nameInput;
    const { value: birthDate } = birthDateInput;
    const { value: gender } = genderInput;

    let errorMessage = "";
    if (!name) {
        errorMessage = "Debe ingresar un nombre.";
    }

    if (!birthDate) {
        errorMessage = `${errorMessage} Debe agregar una fecha de nacimiento.`;
    }

    if (!gender || (gender != "M" && gender != "F")) {
        errorMessage = `${errorMessage} Debe seleccionar un género valido.`;
    }

    if (errorMessage) {
        labelMessage.innerText = errorMessage;
        labelMessage.className = "text-danger";
        return;
    }

    const user = { Name: name, BirthDate: birthDate, Gender: gender };
    const result = await UserService.create(user);
    if (!result.ok && result.status != 404) {
        labelMessage.innerHTML = "Ocurrió un error llamando al servidor";
        labelMessage.className = "text-danger";
        return;
    }

    const body = await result.json();
    let className = "text-success";
    if (!body.successful) {
        className = "text-danger";
    } else {
        nameInput.value = "";
        birthDateInput.value = "";
        genderInput.value = "M";
    }

    labelMessage.innerHTML = body.userMessage;
    labelMessage.className = className;
};