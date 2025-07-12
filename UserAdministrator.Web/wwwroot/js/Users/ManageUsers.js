import { UserService } from './UserService.js';

window.onload = () => {
    loadTable();
}

const loadTable = async () => {
    const userCollection = await loadUserCollection();
    if (userCollection.lenght == 0) {
        return;
    }

    generateTable(userCollection);
}

const loadUserCollection = async () => {
    const response = await UserService.getAll();
    if (!response.ok) {
        return [];
    }

    const body = await response.json();
    if (!body.successful) {
        return [];
    }

    return body.entityCollection;
}

const generateTable = (userCollection) => {
    const tbody = document.querySelector("#tbyUsers");
    tbody.innerHTML = "";
    userCollection.forEach(({ id, name, birthDate, gender }) => {
        const nameLabel = document.createElement('label');
        nameLabel.innerText = name;
        nameLabel.setAttribute('id', `lbName${id}`);

        const nameInput = document.createElement('input');
        nameInput.type = 'text';
        nameInput.id = `txtName${id}`;
        nameInput.value = name;
        nameInput.setAttribute('data-originalValue', name);
        nameInput.className = 'form-control hide-element';

        const birthDateLabel = document.createElement('label');
        const date = new Date(birthDate);
        birthDateLabel.innerText = date.toLocaleDateString();
        birthDateLabel.setAttribute('id', `lbBirthDate${id}`);

        const dateText = birthDate.split('T')[0];
        const birthDateInput = document.createElement('input');
        birthDateInput.type = 'date';
        birthDateInput.id = `txtBirthDate${id}`;
        birthDateInput.value = dateText;
        birthDateInput.setAttribute('data-originalValue', dateText);
        birthDateInput.className = 'form-control hide-element';

        const genderLabel = document.createElement('label');
        genderLabel.innerText = gender;
        genderLabel.setAttribute('id', `lbGender${id}`);

        const genderSelect = document.createElement('select');
        genderSelect.id = `selGender${id}`;
        genderSelect.setAttribute('data-originalValue', gender);
        genderSelect.className = 'form-control hide-element';

        const mOption = document.createElement('option');
        mOption.innerText = 'Masculino';
        mOption.value = 'M';

        const fOption = document.createElement('option');
        fOption.innerText = 'Femenino';
        fOption.value = 'F';

        const editBtn = document.createElement('button');
        editBtn.id = `btnEdit${id}`;
        editBtn.className = 'btn btn-primary m-1';
        editBtn.textContent = 'Editar';
        editBtn.setAttribute('data-userId', id);
        editBtn.addEventListener('click', onEdit);

        const saveBtn = document.createElement('button');
        saveBtn.id = `btnSave${id}`;
        saveBtn.className = 'btn btn-primary m-1 hide-element';
        saveBtn.textContent = 'Guardar';
        saveBtn.setAttribute('data-userId', id);
        saveBtn.addEventListener('click', onSave);

        const cancelBtn = document.createElement('button');
        cancelBtn.id = `btnCancel${id}`;
        cancelBtn.className = 'btn btn-warning m-1 hide-element';
        cancelBtn.textContent = 'Cancelar';
        cancelBtn.setAttribute('data-userId', id);
        cancelBtn.addEventListener('click', onCancel);

        const deleteBtn = document.createElement('button');
        deleteBtn.className = 'btn btn-danger m-1';
        deleteBtn.textContent = 'Eliminar';
        deleteBtn.setAttribute('data-userId', id);
        deleteBtn.addEventListener('click', onDeleteUser);

        const nameTd = document.createElement('td');
        const birthDateTd = document.createElement('td');
        const genderTd = document.createElement('td');
        const actionTd = document.createElement('td');

        const tr = document.createElement('tr');
        tr.setAttribute('id', `tr${id}`);

        genderSelect.appendChild(mOption);
        genderSelect.appendChild(fOption);
        genderSelect.value = gender;

        nameTd.appendChild(nameLabel);
        nameTd.appendChild(nameInput);
        birthDateTd.appendChild(birthDateLabel);
        birthDateTd.appendChild(birthDateInput);
        genderTd.appendChild(genderLabel);
        genderTd.appendChild(genderSelect);
        actionTd.appendChild(editBtn);
        actionTd.appendChild(saveBtn);
        actionTd.appendChild(cancelBtn);
        actionTd.appendChild(deleteBtn);

        tr.appendChild(nameTd);
        tr.appendChild(birthDateTd);
        tr.appendChild(genderTd);
        tr.appendChild(actionTd);

        tbody.appendChild(tr);
    });
}

const onEdit = (event) => {
    const btnEdit = event.target; 
    const userId = btnEdit.getAttribute('data-userId');

    const nameLabel = document.querySelector(`#lbName${userId}`);
    const nameInput = document.querySelector(`#txtName${userId}`);

    const birthDateLabel = document.querySelector(`#lbBirthDate${userId}`);
    const birthDateInput = document.querySelector(`#txtBirthDate${userId}`);

    const genderLabel = document.querySelector(`#lbGender${userId}`);
    const genderInput = document.querySelector(`#selGender${userId}`);

    const btnSave = document.querySelector(`#btnSave${userId}`);
    const btnCancel = document.querySelector(`#btnCancel${userId}`);

    nameLabel.classList.add('hide-element');
    nameInput.classList.remove('hide-element');

    birthDateLabel.classList.add('hide-element');
    birthDateInput.classList.remove('hide-element');

    genderLabel.classList.add('hide-element');
    genderInput.classList.remove('hide-element');

    btnEdit.classList.add('hide-element');
    btnSave.classList.remove('hide-element');
    btnCancel.classList.remove('hide-element');
};

const onCancel = (event) => {
    const btnCancel = event.target;
    const userId = btnCancel.getAttribute('data-userId');

    const nameLabel = document.querySelector(`#lbName${userId}`);
    const nameInput = document.querySelector(`#txtName${userId}`);

    const birthDateLabel = document.querySelector(`#lbBirthDate${userId}`);
    const birthDateInput = document.querySelector(`#txtBirthDate${userId}`);

    const genderLabel = document.querySelector(`#lbGender${userId}`);
    const genderInput = document.querySelector(`#selGender${userId}`);

    const btnSave = document.querySelector(`#btnSave${userId}`);
    const btnEdit = document.querySelector(`#btnEdit${userId}`);

    nameLabel.classList.remove('hide-element');
    nameInput.classList.add('hide-element');
    nameInput.value = nameInput.getAttribute('data-originalValue');

    birthDateLabel.classList.remove('hide-element');
    birthDateInput.classList.add('hide-element');
    birthDateInput.value = birthDateInput.getAttribute('data-originalValue');

    genderLabel.classList.remove('hide-element');
    genderInput.classList.add('hide-element');
    genderInput.value = genderInput.getAttribute('data-originalValue');

    btnEdit.classList.remove('hide-element');
    btnSave.classList.add('hide-element');
    btnCancel.classList.add('hide-element');
};

const onSave = async (event) => {
    const labelMessage = document.querySelector("#lbMessage");
    const userId = event.target.getAttribute('data-userId');
    const { value: name } = document.querySelector(`#txtName${userId}`);
    const { value: birthDate } = document.querySelector(`#txtBirthDate${userId}`);
    const { value: gender } = document.querySelector(`#selGender${userId}`);

    const response = await UserService.update({ Id: userId, Name: name, BirthDate: birthDate, Gender: gender });
    if (!response.ok && response.status != 404) {
        labelMessage.innerHTML = 'Ocurrió un error al comunicarse con el servidor';
        labelMessage.className = 'text-danger';
        return;
    }

    const body = await response.json();
    let className = 'text-success';
    if (!body.successful) {
        className = 'text-danger';
    } else {
        loadTable();
    }

    labelMessage.innerHTML = body.userMessage;
    labelMessage.className = className;
};

const onDeleteUser = async (event) => {
    const labelMessage = document.querySelector("#lbMessage");
    const userId = event.target.getAttribute('data-userId');
    const deleteResponse = await deleteUser(userId);

    let className = 'text-success';
    if (!deleteResponse.ok) {
        className = 'text-danger';
    }

    labelMessage.className = className;
    labelMessage.innerHTML = deleteResponse.msg;
    removeRow(userId);
};

const deleteUser = async (userId) => {
    const response = await UserService.delete(userId);
    if (!response.ok) {
        return { ok: false, msg: 'Ocurrió un error al comunicarse con el servidor' };
    }

    const body = await response.json();
    return { ok: body.successful, msg: body.userMessage };
};

const removeRow = (userId) => {
    const trToDelete = document.querySelector(`#tr${userId}`);
    trToDelete.remove();
};
