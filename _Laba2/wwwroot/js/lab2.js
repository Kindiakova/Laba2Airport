const uri = 'api/Directions';
let directions = [];

function getDirections() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayDirections(data))
        .catch (error => console.error("Unable to get directions"));
}

function addDirection() {
    const addCountryFromTextbox = document.getElementById('add-countryFrom');
    const addCountryToTextbox = document.getElementById('add-countryTo');
    const addCityFromTextbox = document.getElementById('add-cityFrom');
    const addCityToTextbox = document.getElementById('add-cityTo');
    const addTerminalFromTextbox = document.getElementById('add-terminalFrom');
    const addTerminalToTextbox = document.getElementById('add-terminalTo');
    const addRunwayFromTextbox = document.getElementById('add-runwayFrom');
    const addRunwayToTextbox = document.getElementById('add-runwayTo');

    const direction = {
        countryFrom: addCountryFromTextbox.value.trim(),
        countryTo: addCountryToTextbox.value.trim(),
        cityFrom: addCityFromTextbox.value.trim(),
        cityTo: addCityToTextbox.value.trim(),
        terminalFrom: addTerminalFromTextbox.value.trim(),
        terminalTo: addTerminalToTextbox.value.trim(),
        runwayFrom: addRunwayFromTextbox.value.trim(),
        runwayTo: addRunwayToTextbox.value.trim(),
    };
    console.log(JSON.stringify(direction));
    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(direction)
    })
        .then(response => {
            console.log(response);
            return response.json();
        })
        .then(() => {
            getDirections(); 
            addCountryFromTextbox.value = ''; 
            addCountryToTextbox.value = '';
            addCityFromTextbox.value = '';
            addCityToTextbox.value = '';
            addTerminalFromTextbox.value = '';
            addTerminalToTextbox.value = '';
            addRunwayFromTextbox.value = '';
            addRunwayToTextbox.value = '';
        })
        .catch(error => console.error('Unable to add directions.', error));
}
function deleteDirection(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getDirections())
        .catch(error => console.error('Unable to delete direction.', error));
}
function displayEditForm(id) {
    const direction = directions.find(direction => direction.id == id);
    document.getElementById('edit-id').value = direction.id;
    document.getElementById('edit-countryFrom').value = direction.countryFrom;
    document.getElementById('edit-countryTo').value = direction.countryTo;
    document.getElementById('edit-cityFrom').value = direction.cityFrom;
    document.getElementById('edit-cityTo').value = direction.cityTo;
    document.getElementById('edit-terminalFrom').value = direction.terminalFrom;
    document.getElementById('edit-terminalTo').value = direction.terminalTo;
    document.getElementById('edit-runwayFrom').value = direction.runwayFrom;
    document.getElementById('edit-runwayTo').value = direction.runwayTo;
    document.getElementById('editForm').style.display = 'block';
}
function updateDirection() {
    const directionId = document.getElementById('edit-id').value;
    const direction = {
        //id: parseInt(directionId, 10),
        countryFrom: document.getElementById('edit-countryFrom').value.trim(),
        countryTo: document.getElementById('edit-countryTo').value.trim(),
        cityFrom: document.getElementById('edit-cityFrom').value.trim(),
        cityTo: document.getElementById('edit-cityTo').value.trim(),
        terminalFrom: document.getElementById('edit-terminalFrom').value.trim(),
        terminalTo: document.getElementById('edit-terminalTo').value.trim(),
        runwayFrom: document.getElementById('edit-runwayFrom').value.trim(),
        runwayTo: document.getElementById('edit-runwayTo').value.trim()  
    };
    console.log(JSON.stringify(direction));
    fetch(`${uri}/${directionId}`, { 
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(direction)
    })
        .then(() => getDirections())
        .catch(error => console.error('Unable to update direction.', error));
    closeInput();
    return false;
}
function closeInput() {
    document.getElementById('editForm').style.display = 'none'; 
   
}

function _displayDirections(data) {
    const tBody = document.getElementById('directions');

    tBody.innerHTML = '';
    const button = document.createElement('button');
    data.forEach(direction => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Редагувати';
        editButton.setAttribute('onclick', `displayEditForm(${direction.id})`);
        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Видалити';
        deleteButton.setAttribute('onclick', `deleteDirection(${direction.id})`);
        let tr = tBody.insertRow();
        let td1 = tr.insertCell(0);
        let textNodeFrom = document.createTextNode
            (direction.countryFrom + ' ' + direction.cityFrom + ' ' + direction.terminalFrom + ' ' + direction.runwayFrom);
        td1.appendChild(textNodeFrom);
        let td2 = tr.insertCell(1);
        let textNodeTo = document.createTextNode
            (direction.countryTo + ' ' + direction.cityTo + ' ' + direction.terminalTo + ' ' + direction.runwayTo);
        td2.appendChild(textNodeTo);
        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);
        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });
    directions = data;
}