
(function () {

    /*========================================================================================================================================*/

    /*FORM VALIDATION*/
    const errorEvent = document.getElementById('errorForForm');
    const form = document.getElementById('formUpdateEvent');
    const eventTitle = document.getElementById('EventTitle');
    const location = document.getElementById('Location');
    const date = document.getElementById('Date');

    console.log(date.value);


    /*Event Listener that checks form validity before sending to database*/
    form.addEventListener('submit', function (ev) {
        console.log(date.value);
        let errorMessage = [];
        if (eventTitle.value === '' || eventTitle.value === null) {
            errorMessage.push("Event Title is Required");
        }
        if (location.value === '' || location.value === null) {
            errorMessage.push("Location is Required");
        }
        if (date.value === '' || date.value === null) {
            errorMessage.push("Event Date is Required");
        }

        if (errorMessage.length > 0) {
            ev.preventDefault();
            errorEvent.innerHTML = errorMessage.join(', ');
        }
        
    });

    /*========================================================================================================================================*/

})();