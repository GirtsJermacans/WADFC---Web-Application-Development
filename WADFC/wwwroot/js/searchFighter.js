
(function () {

    /*========================================================================================================================================*/

    /*FORM VALIDATION*/
    const errorEvent = document.getElementById('errorForForm');
    const form = document.getElementById('formDesktopFighters2');
    const searchString = form.childNodes[1];

    /*Event Listener that checks form validity before sending to database*/
    form.addEventListener('submit', function (ev) {
        let errorMessage = [];
        console.log(searchString.value)
        if (searchString.value === '' || searchString.value === null) {
            errorMessage.push("Fighter Name is Required");
        }

        if (errorMessage.length > 0) {
            ev.preventDefault();
            errorEvent.innerHTML = errorMessage.join(', ');
        }

    });

    /*========================================================================================================================================*/

    $('#unknownSearch').attr('action', 'SearchFighter');

    /*========================================================================================================================================*/

    /*FORM VALIDATION FOR MOBILE SEARCH BOX*/
    if (window.innerWidth < 720) {
        const formMobile = document.getElementById('unknownSearch');
        const searchString = formMobile.childNodes[1];


        /*Event Listener that checks form validity before sending to database*/
        formMobile.addEventListener('submit', function (ev) {
            let errorMessage = [];
            if (searchString.value === '' || searchString.value === null) {
                console.log("Do I Get Here?");
                console.dir(formMobile);
                errorMessage.push("Fighter Name is Required");
            }

            if (errorMessage.length > 0) {
                ev.preventDefault();
                errorEvent.innerHTML = errorMessage.join(', ');
            }
        });
    }


    /*========================================================================================================================================*/

})();