
(function () {

    /*========================================================================================================================================*/

    /*FORM VALIDATION*/
    const errorEvent = document.getElementById('errorForForm');
    const form = document.getElementById('formUpdateFight');
    const fighterAID = document.getElementById('NewFightForm_FighterAID');
    const fighterBID = document.getElementById('NewFightForm_FighterBID');
    const division = document.getElementById('NewFightForm_Division');
    const winner = document.getElementById('NewFightForm_Winner');
    const method = document.getElementById('NewFightForm_Method');

    /*Event Listener that checks form validity before sending to database*/
    form.addEventListener('submit', function (ev) {
        const fighterA = fighterAID.childNodes[fighterAID.value];
        const fighterB = fighterBID.childNodes[fighterBID.value];
        let errorMessage = [];
        if (fighterA.innerText === '' || fighterA.innerText === null) {
            errorMessage.push("Fighter A is Required");
        }
        if (fighterB.innerText === '' || fighterB.innerText === null) {
            errorMessage.push("Fighter B is Required");
        }
        if (division.value === '' || division.value === null) {
            errorMessage.push("Division is Required");
        }
        if (winner.value === '' || winner.value === null) {
            errorMessage.push("Winner | No Contest | Draw is Required");
        }
        else if (winner.value != '' || winner.value != null) {
            if (winner.value !== fighterA.innerText) {
                if (winner.value !== fighterB.innerText) {
                    if (winner.value !== "No Contest") {
                        console.log("Step Before Last");
                        if (winner.value !== "Draw") {
                            errorMessage.push("Winner | No Contest | Draw is Required");
                        }
                    }
                }
            }
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