
(function () {

    /*========================================================================================================================================*/

    /*FORM VALIDATION*/
    const errorEvent = document.getElementById('errorForForm');
    const form = document.getElementById('formRegister');
    const userName = document.getElementById('UserName');
    const password = document.getElementById('Password');
    const confirmPassword = document.getElementById('ConfirmPassword');
    const email = document.getElementById('Email');
    const fullName = document.getElementById('FullName');
    const doB = document.getElementById('BirthDate');

    /*Event Listener that checks form validity before sending to database*/
    form.addEventListener('submit', function (ev) {
        let errorMessage = [];
        if (userName.value === '' || userName.value === null) {
            errorMessage.push("User Name is Required");
        }
        if (password.value === '' || password.value === null) {
            errorMessage.push("Password is Required");
        }
        if (confirmPassword.value === '' || confirmPassword.value === null) {
            errorMessage.push("Need to Confirm Password");
        }
        if (password.value != confirmPassword.value) {
            errorMessage.push("Failed to Confirm Password");
        }
        if (email.value === '' || email.value === null) {
            errorMessage.push("Email is Required");
        }
        if (fullName.value === '' || fullName.value === null) {
            errorMessage.push("Full Name is Required");
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