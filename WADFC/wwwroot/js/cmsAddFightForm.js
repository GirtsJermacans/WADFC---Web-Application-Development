
(function () {

    /*========================================================================================================================================*/

    /*FORM VALIDATION*/
    const errorEvent = document.getElementById('errorForForm');
    const form = document.getElementById('formAddFight');
    const division = document.getElementById('NewFightForm_Division');
    const winner = document.getElementById('NewFightForm_Winner');
    const method = document.getElementById('NewFightForm_Method');

    /*Event Listener that checks form validity before sending to database*/
    form.addEventListener('submit', function (ev) {
        let errorMessage = [];
        if (division.value === '' || division.value === null) {
            errorMessage.push("Division is Required");
        }
        if (winner.value === '' || winner.value === null) {
            errorMessage.push("Winner | No Contest | Draw is Required");
        }
        if (method.value === '' || method.value === null) {
            errorMessage.push("Method is Required");
        }

        if (errorMessage.length > 0) {
            ev.preventDefault();
            errorEvent.innerHTML = errorMessage.join(', ');
        }
        
    });

    /*========================================================================================================================================*/

})();