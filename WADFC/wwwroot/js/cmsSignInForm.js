
(function () {

    /*========================================================================================================================================*/

    /*FORM VALIDATION*/
    const errorEvent = document.getElementById('errorForForm');
    const form = document.getElementById('formSignIn');
    const userName = document.getElementById('UserName');
    const password = document.getElementById('Password');

    /*Event Listener that checks form validity before sending to database*/
    form.addEventListener('submit', function (ev) {
        let errorMessage = [];
        if (userName.value === '' || userName.value === null) {
            errorMessage.push("User Name is Required");
        }
        if (password.value === '' || password.value === null) {
            errorMessage.push("Password is Required");
        }

        if (errorMessage.length > 0) {
            ev.preventDefault();
            errorEvent.innerHTML = errorMessage.join(', ');
        }
        
    });

    /*========================================================================================================================================*/

})();