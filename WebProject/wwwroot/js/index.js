const onInputDelete = (modelName) => {
    const userInput = document.getElementById('userInput').value;
    const deleteBtn = document.getElementById('deleteBtn');

    if (userInput === modelName) {
        deleteBtn.disabled = false;
    }
    else {
        deleteBtn.disabled = true;
    }
};