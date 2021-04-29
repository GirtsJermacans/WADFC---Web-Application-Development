
(function () {

    /*========================================================================================================================================*/

    /*FORM VALIDATION*/
    const errorEvent = document.getElementById('errorForForm');
    const form = document.getElementById('formEditFighter');
    const firstName = document.getElementById('FirstName');
    const surname = document.getElementById('Surname');
    const height = document.getElementById('Height');
    const weight = document.getElementById('Weight');
    const reach = document.getElementById('Reach');
    const stance = document.getElementById('Stance');
    const win = document.getElementById('Win');
    const loss = document.getElementById('Loss');
    const draw = document.getElementById('Draw');
    const noContest = document.getElementById('NoContest');
    const doB = document.getElementById('DOB');

    /*Event Listener that checks form validity before sending to database*/
    form.addEventListener('submit', function (ev) {
        let errorMessage = [];
        if (firstName.value === '' || firstName.value === null) {
            errorMessage.push("First Name is Required");
        }
        if (surname.value === '' || surname.value === null) {
            errorMessage.push("Surname is Required");
        }
        if (height.value === '' || height.value === null) {
            errorMessage.push("Height is Required");
        }
        if (weight.value === '' || weight.value === null) {
            errorMessage.push("Weight is Required");
        }
        if (reach.value === '' || reach.value === null) {
            errorMessage.push("Reach is Required");
        }
        if (stance.value === '' || stance.value === null) {
            errorMessage.push("Stance is Required");
        }
        if (win.value === '' || win.value === null) {
            errorMessage.push("Win stat is Required");
        }
        if (loss.value === '' || loss.value === null) {
            errorMessage.push("Loss stat is Required");
        }
        if (draw.value === '' || draw.value === null) {
            errorMessage.push("Draw stat is Required");
        }
        if (noContest.value === '' || noContest.value === null) {
            errorMessage.push("No Contest stat is Required");
        }
        if (doB.value === '' || doB.value === null) {
            errorMessage.push("Date of Birth is Required");
        }

        if (errorMessage.length > 0) {
            ev.preventDefault();
            errorEvent.innerHTML = errorMessage.join(', ');
        }

    });

    /*========================================================================================================================================*/

})();