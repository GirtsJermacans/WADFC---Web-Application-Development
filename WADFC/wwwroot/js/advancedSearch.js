
(function () {

    /*=============================================================================================================================================*/

    /*FORM VALIDATION*/
    const selection1 = document.getElementById('targetSelection');
    const selection2 = document.getElementById('targetSelection2')
    const selection3 = document.getElementById('targetSelection3')
    const form = document.getElementById('myForm');
    const errorEvent = document.getElementById('errorForJSForm');

    selection1.addEventListener('change', function () {
        errorEvent.innerHTML = '';
    });

    selection2.addEventListener('change', function () {
        errorEvent.innerHTML = '';
    });

    selection3.addEventListener('change', function () {
        errorEvent.innerHTML = '';
    });


    console.log(selection1);

    /*Used for finding Event based on Event Title or Fighter Name*/
    const eventTitleString = document.getElementById('EventTitleString');
    const fighterNameString = document.getElementById('FighterNameString');

    /*Used for finding Fighter(s) based on Fighter Name or Fighter Record*/
    const fighterNameStringFighter = document.getElementById('FighterNameStringFighter');
    const win = document.getElementById('Win');
    const loss = document.getElementById('Loss');
    const draw = document.getElementById('Draw');
    const noContest = document.getElementById('NoContest');

    /*Event Listener that checks form validity before sending to database*/
    form.addEventListener('submit', function (ev) {
        let errorMessages = [];
        if (selection1.value === "Fighter") {
            if (selection3.value === "Fighter Name") {
                if (fighterNameStringFighter.value === '' || fighterNameStringFighter.value === null) {
                    errorMessages.push("Fighter Name is Required");
                }
            }
            else if (selection3.value === "Record") {
                if (win.value === '' && loss.value === '' && draw.value === '' && noContest.value === '') {
                    errorMessages.push("Min One Category is Required");
                }
            }
        }
        else if (selection1.value === "Event") {
            if (selection2.value === "Event Title") {;
                if (eventTitleString.value === '' || eventTitleString.value === null) {
                    errorMessages.push("Event Title is Required");
                }
            }
            else if (selection2.value === "Fighter Name") {
                if (fighterNameString.value === '' || fighterNameString.value === null) {
                    errorMessages.push("Fighter Name is Required");
                }
            }
        }

        if (errorMessages.length > 0) {
            ev.preventDefault();
            errorEvent.innerHTML = errorMessages.join(', ');
        }

    });
        
    /*=============================================================================================================================================*/

    /*VISUAL LOGIC*/
    $(document).ready(function () {
        $("#searchFighterByRecord").hide();
    });

    // Section 1: A
    $("#targetSelection").change(function () {
        if ($("#targetSelection option:selected").text() == "Event") {                          // When we search for event
            $('.eventSearchingOptions option:contains("Event Title")').prop('selected', true);
            $("#eventSearchingOptions").show();
            $("#searchEventByEventTitle").show();
            $("#searchEventByFighterName").hide();
            $("#fighterSearchingOptions").hide();
            $("#searchFighterByFighterName").hide();
            $("#searchFighterByRecord").hide();
        } // Section 2: A
        else if ($("#targetSelection option:selected").text() == "Fighter") {                   // When we search for Fighter
            $('#targetSelection3 option:contains("Fighter Name")').prop('selected', true);
            $("#eventSearchingOptions").hide();
            $("#fighterSearchingOptions").show();
            $("#searchFighterByFighterName").show();
            $("#searchFighterByRecord").hide();
        }
    });

    // Section 1: B
    $("#targetSelection2").change(function () {
        if ($("#targetSelection2 option:selected").text() == "Event Title") {                   // When we search for event using Event Title
            $("#searchEventByEventTitle").show();
            $("#searchEventByFighterName").hide();
        }
        else if ($("#targetSelection2 option:selected").text() == "Fighter Name") {              // When we search for event using Fighter Name
            $("#searchEventByFighterName").show();
            $("#searchEventByEventTitle").hide();
        }
    });

    // Section 2: B
    $("#targetSelection3").change(function () {
        if ($("#targetSelection3 option:selected").text() == "Fighter Name") {
            $("#searchFighterByFighterName").show();
            $("#searchFighterByRecord").hide();
        }
        else if ($("#targetSelection3 option:selected").text() == "Record") {
            $("#searchFighterByFighterName").hide();
            $("#searchFighterByRecord").show();
        }
    });

    /*=============================================================================================================================================*/

})();